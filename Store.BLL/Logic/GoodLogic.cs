using System;
using System.Collections.Generic;
using System.Linq;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class GoodLogic : IGoodLogic
    {
        //private readonly IRepository<Good> _repository;
        private readonly IGoodRepository _repository;

        //public GoodLogic(IRepository<Good> repository)
        public GoodLogic(IGoodRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Good> GetAll()
        {
            return _repository.GetAll();
        }

        public Good Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }
            return _repository.Get(id.Value);
        }

        public IEnumerable<Good> Find(Func<Good, bool> predicate)
        {
            return _repository.Find(predicate);
        }

        public void Add(Good good)
        {
            _repository.Add(good);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(Good good)
        {
            _repository.Edit(good);
        }

        public IEnumerable<Good> Search(string search, FilterModel filter)
        {
            return _repository.Search(search, filter);
        }
    }
}