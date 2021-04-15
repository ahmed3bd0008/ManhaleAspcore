using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManhaleAspNetCore.Models;

namespace ManhaleAspNetCore.Controllers
{
    public class KhaliaController : Controller
    {
        private readonly ManahelContext _context;
        static private int ManhalID = 0;
        static private int QueueID = 0;
        public KhaliaController(ManahelContext context)
        {
            _context = context;
        }

        // GET: Khalia
        public async Task<IActionResult> Index(int id)
        {
            ManhalID = id;
            var manahelContext = _context.khaliases.Where(a => a.ManhalId == ManhalID)
                                                    .Include(k => k.Manhal)
                                                    .Include(c => c.Queues);
            ViewData["ManhalId"] = ManhalID;
            return View(await manahelContext.ToListAsync());
        }

        // GET: Khalia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khalias = await _context.khaliases
                .Include(k => k.Manhal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khalias == null)
            {
                return NotFound();
            }

            return View(khalias);
        }

        // GET: Khalia/Create
        public IActionResult Create()
        {
            //ViewData["ManhalId"] = new SelectList(_context.manahels, "Id", "Id");
            ViewData["ManhalId"] = ManhalID;
            ViewBagData();
            //WithoutQueue
            return View();
        }

        // POST: Khalia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Khalias khalias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khalias);
                await _context.SaveChangesAsync();
                ViewData["ManhalId"] = ManhalID;
                return RedirectToAction(nameof(Index), new { id = ManhalID }) ;
            }
            ViewData["ManhalId"] = ManhalID;
            ViewBagData();
            return View(khalias);
        }

        // GET: Khalia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khalias = await _context.khaliases.Where(a => a.Id == id).Include(k => k.Queues).FirstOrDefaultAsync();
            if (khalias == null)
            {
                return NotFound();
            }
            //ViewData["ManhalId"] = new SelectList(_context.manahels, "Id", "FlowerName", khalias.ManhalId);
            ViewData["ManhalId"] = ManhalID;
            ViewData["KhaliaId"] = khalias.Id;
            QueueID = khalias.Queues.Id;
            ViewData["QueueId"] = QueueID;
            ViewBagData();
            return View(khalias);
        }

        // POST: Khalia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        ///*[Bind("Id,Ssn,KhaliaLevel,KhaliaType,Wood,PraweezCount,Notes,ManhalId")]*/
        [HttpPost]
        [ValidateAntiForgeryToken]  //Edit Khalia Info
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Ssn,KhaliaLevel,KhaliaType,Wood,PraweezCount,Notes,ManhalId")] Khalias khalias)
        {
            if (id != khalias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khalias);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhaliasExists(khalias.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["ManhalId"] = ManhalID;
                ViewData["QueueId"] = QueueID;
                return RedirectToAction(nameof(Index), new { id = ManhalID });
            }
            ViewData["ManhalId"] = ManhalID;
            ViewData["KhaliaId"] = khalias.Id;
            ViewData["QueueId"] = QueueID;
            ViewBagData();
            return View(khalias);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  //Edit Queue Info  
        public async Task<IActionResult> EditQueue(int id, [Bind("Id,QueueStatus,DateFertilization,KhaliaId")] Queue queue)
        {
            if (id != queue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                ViewData["ManhalId"] = ManhalID;
                return RedirectToAction(nameof(Index), new { id = ManhalID });
            }
            ViewData["ManhalId"] = ManhalID;
            ViewBagData();
            return RedirectToAction(nameof(Edit), new { id = queue.KhaliaId });
        }
        // GET: Khalia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khalias = await _context.khaliases
                .Include(k => k.Manhal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khalias == null)
            {
                return NotFound();
            }

            return View(khalias);
        }

        // POST: Khalia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khalias = await _context.khaliases.FindAsync(id);
            _context.khaliases.Remove(khalias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { id = ManhalID });
        }

        private bool KhaliasExists(int id)
        {
            return _context.khaliases.Any(e => e.Id == id);
        }

        private void ViewBagData()
        {
            List<string> KhaliaLevel = new List<string>();
            KhaliaLevel.Add("Excellent"); KhaliaLevel.Add("Strong");
            KhaliaLevel.Add("Medium"); KhaliaLevel.Add("Weak");
            ViewData["KhaliaLevel"] = new SelectList(KhaliaLevel);

            List<string> KhaliaType = new List<string>();
            KhaliaType.Add("Complete"); KhaliaType.Add("Medium");
            KhaliaType.Add("Small");
            ViewData["KhaliaType"] = new SelectList(KhaliaType);

            List<string> woodType = new List<string>();
            woodType.Add("New"); woodType.Add("Old");
            ViewData["woodType"] = new SelectList(woodType);

            List<string> QueueStatus = new List<string>();
            QueueStatus.Add("Fertilized"); QueueStatus.Add("not Fertilized");
            QueueStatus.Add("Stacked"); QueueStatus.Add("without Queue");
            ViewData["QueueStatus"] = new SelectList(QueueStatus);
        }
    }
}
