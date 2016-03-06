using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class CategoryRepository : Repository, IRepository<Category>
    {
        public CategoryRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories.Where(c => c.IsDeleted == false);
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return db.Categories.Where(predicate);
        }

        public void Add(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category != null)
            {
                category.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void Edit(Category entity)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == entity.Id);
            category.Id = entity.Id;
            category.Name = entity.Name;
            db.SaveChanges();
        }
    }
}