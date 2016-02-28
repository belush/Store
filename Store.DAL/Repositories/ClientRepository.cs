using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class ClientRepository : Repository, IClientRepository
    {
        public ClientRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            return db.ClientProfiles;
        }

        public ClientProfile Get(int id)
        {
            return db.ClientProfiles.Find(id);
        }

        public ClientProfile Get(string id)
        {
            return db.ClientProfiles.Find(id);
        }

        public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate)
        {
            return db.ClientProfiles.Where(predicate);
        }

        public void Add(ClientProfile entity)
        {
            db.ClientProfiles.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var client = db.ClientProfiles.Find(id);
            if (client != null)
            {
                db.ClientProfiles.Remove(client);
                db.SaveChanges();
            }
        }

        public void Edit(ClientProfile entity)
        {
            var clientProfile = db.ClientProfiles.FirstOrDefault(c => c.Id == entity.Id);
            clientProfile.Id = entity.Id;
            clientProfile.Name = entity.Name;
            clientProfile.Address = entity.Address;
            clientProfile.ApplicationUser = entity.ApplicationUser;
            clientProfile.Orders = entity.Orders;
            db.SaveChanges();
        }
    }
}