﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class StatusRepository : Repository, IRepository<Status>
    {
        public StatusRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<Status> GetAll()
        {
            return db.Statuses;
        }

        public Status Get(int id)
        {
            return db.Statuses.Find(id);
        }

        public IEnumerable<Status> Find(Func<Status, bool> predicate)
        {
            return db.Statuses.Where(predicate);
        }

        public void Add(Status entity)
        {
            db.Statuses.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var status = db.Statuses.Find(id);
            if (status != null)
            {
                db.Statuses.Remove(status);
                db.SaveChanges();
            }
        }

        public void Edit(Status entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
