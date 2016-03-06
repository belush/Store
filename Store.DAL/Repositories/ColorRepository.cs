using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class ColorRepository : Repository, IRepository<Color>
    {
        public ColorRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<Color> GetAll()
        {
            return db.Colors.Where(c => c.IsDeleted == false);
        }

        public Color Get(int id)
        {
            return db.Colors.Find(id);
        }

        public IEnumerable<Color> Find(Func<Color, bool> predicate)
        {
            return db.Colors.Where(predicate);
        }

        public void Add(Color entity)
        {
            db.Colors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var color = db.Colors.Find(id);
            if (color != null)
            {
                color.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void Edit(Color entity)
        {
            //db.Entry(entity).State = EntityState.Modified;
            var color = db.Colors.FirstOrDefault(c => c.Id == entity.Id);
            color.Id = entity.Id;
            color.Name = entity.Name;
            db.SaveChanges();
        }
    }
}