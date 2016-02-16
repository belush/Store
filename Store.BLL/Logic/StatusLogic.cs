using System;
using System.Collections.Generic;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class StatusLogic : IStatusLogic
    {
        private readonly IRepository<Status> _repository;

        public StatusLogic(IRepository<Status> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Status> GetAll()
        {
            return _repository.GetAll();
        }

        public void Add(Status status)
        {
            _repository.Add(status);
        }

        public Status Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            return _repository.Get(id.Value);
        }

        public void Edit(Status status)
        {
            _repository.Edit(status);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}