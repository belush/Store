using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class GoodRepository : Repository, IRepository<Good>
    {
        public GoodRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<Good> GetAll()
        {
            return db.Goods;
        }

        public Good Get(int id)
        {
            return db.Goods.Find(id);
        }

        public IEnumerable<Good> Find(Func<Good, bool> predicate)
        {
            return db.Goods.Where(predicate);
        }

        public void Add(Good entity)
        {
            db.Goods.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var good = db.Goods.Find(id);
            if (good != null)
            {
                db.Goods.Remove(good);
                db.SaveChanges();
            }
        }

        public void Edit(Good entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}