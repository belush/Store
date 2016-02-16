using System;
using System.Collections.Generic;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly IRepository<Category> _repository;

        public CategoryLogic(IRepository<Category> repository)
        {
            _repository = repository;
        }


        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }
            return _repository.Get(id.Value);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return _repository.Find(predicate);
        }

        public void Add(Category category)
        {
            _repository.Add(category);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(Category category)
        {
            _repository.Edit(category);
        }
    }
}