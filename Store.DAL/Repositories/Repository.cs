using Store.DAL.Context;

namespace Store.DAL.Repositories
{
    public class Repository
    {
        protected readonly StoreContext db;

        public Repository(StoreContext storeContext)
        {
            db = storeContext;
        }
    }
}