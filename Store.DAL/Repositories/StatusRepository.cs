using System;
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
            return db.Statuses.Where(s => s.IsDeleted == false);
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
                status.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void Edit(Status entity)
        {
            var status = db.Statuses.FirstOrDefault(s => s.Id == entity.Id);
            status.Id = entity.Id;
            status.Name = entity.Name;
            db.SaveChanges();
        }
    }
}
