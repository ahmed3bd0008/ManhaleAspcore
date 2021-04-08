using ManhaleAspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ManahelContext _context;
        private readonly DbSet<T>_entity;

        public Repository(ManahelContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        public T Add(T Item)
        {
            _entity.Add(Item);
            _context.SaveChanges();
            return Item;
        }

        public T Delete(int id)
        {
            var item = _entity.Find(id);
            if (item!=null)
            {
                _entity.Remove(item);
                _context.SaveChanges();
            }
            return item;
        }

        public List<T> GetAll()
        {
            return _entity.ToList();
        }

        public T GetById(int id)
        {
            return _entity.Find(id);
        }

        public T Update(T item)
        {
            if (item==null)
            {
                throw new ArgumentNullException("entity");
            }

            _entity.Update(item);
            _context.SaveChanges();
            return item;

        }
    }
}
