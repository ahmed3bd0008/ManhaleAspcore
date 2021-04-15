using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManhaleAspNetCore.Models;
using ManhaleAspNetCore.ModelView.Manahel;
using System.Text.Json;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ManhaleAspNetCore.Controllers
{
    public class ManhalController : Controller
    {
        private readonly ManahelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ManhalController(ManahelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Manhal
        public async Task<IActionResult> Index()
        {
            ViewData["Charts"] = ChartsValue.GetForAllManahel(_context);
            return View(await _context.manahels.Where(a => a.Account_ID == "1").ToListAsync());
        }

        // GET: Manhal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Charts"] = ChartsValue.GetForAllManahel(_context);
            ViewData["KhaliaCount"] = ChartsValue.GetKhaliaCount(_context);
            ViewData["QueueCount"] = ChartsValue.GetQueueCount(_context);
            ViewData["Average"] = "66%";
            ViewData["ManhalImage"] = new AddImageVM() { id = id, imageFile = null };

            if (id == null)
            {
                return NotFound();
            }

            var manahel = await _context.manahels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manahel == null)
            {
                return NotFound();
            }
            else
            {
                var imgs = await _context.Images.Where(a => a.TabelName == "manhal")
                                            .Where(b => b.TabelId == manahel.Id).ToListAsync();
                manahel.ImageManhals = imgs;
            }

            return View(manahel);
        }

        // GET: Manhal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manhal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ssn,NickName,LocationName,FlowerName,DateCreated,DateUpdated")] Manahel manahel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    manahel.Account_ID = "1";
                    manahel.DateUpdated = manahel.DateCreated;
                    _context.Add(manahel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch { }
            return View(manahel);
        }

        // GET: Manhal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manahel = await _context.manahels.FindAsync(id);
            if (manahel == null)
            {
                return NotFound();
            }
            return View(manahel);
        }

        // POST: Manhal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ssn,NickName,LocationName,FlowerName,DateCreated,DateUpdated,Account_ID")] Manahel manahel)
        {
            if (id != manahel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manahel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManahelExists(manahel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(manahel);
        }

        // GET: Manhal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manahel = await _context.manahels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manahel == null)
            {
                return NotFound();
            }

            return View(manahel);
        }

        // POST: Manhal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manahel = await _context.manahels.FindAsync(id);
            _context.manahels.Remove(manahel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManahelExists(int id)
        {
            return _context.manahels.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SearchManhal(string txtSearch)
        {
            ViewData["Charts"] = ChartsValue.GetForAllManahel(_context);

            var result = await _context.manahels.Where(a => a.Account_ID == "1")
                                            .Where(b => b.NickName.Contains(txtSearch)).ToListAsync();
            return View(result);
            //return Json(result/*,new JsonSerializerOptions(JsonSerializerDefaults.General)*/);
        }

        public async Task<IActionResult> AddImage(AddImageVM imageVM)
        {
            string uniqueFileName = null;

            if (imageVM.imageFile != null)
            {
                try
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + imageVM.imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageVM.imageFile.CopyTo(fileStream);
                    }
                    Images images = new Images();
                    images.ImagesString = uniqueFileName;
                    images.TabelId = imageVM.id.Value;
                    images.TabelName = "manhal";

                    await _context.Images.AddAsync(images);
                    await _context.SaveChangesAsync();
                }
                catch { }
            }
            return RedirectToAction("Details","Manhal", new { id = imageVM.id });
        }

        public async Task<IActionResult> DeleteImage(int idImg,int id)
        {
            Images image =await _context.Images.Where(a => a.Id == idImg).FirstAsync();
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Manhal", new { id = id });
        }
    }
}
