using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Identity;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly StoreContext db;
        private bool disposed;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new StoreContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            ClientManager = new ClientManager(db);
        }

        public ApplicationUserManager UserManager { get; }

        public IClientManager ClientManager { get; }

        public ApplicationRoleManager RoleManager { get; }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    ClientManager.Dispose();
                }
                disposed = true;
            }
        }
    }
}