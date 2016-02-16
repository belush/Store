using System;
using System.Collections.Generic;
using System.Linq;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class OrderItemLogic : IOrderItemLogic
    {
        private readonly IRepository<OrderItem> _repository;

        public OrderItemLogic(IRepository<OrderItem> repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<OrderItem> GetByOrderNull()
        {
            return _repository.GetAll().Where(o => o.Order == null).ToList();
        }

        public OrderItem Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }
            return _repository.Get(id.Value);
        }

        public IEnumerable<OrderItem> Find(Func<OrderItem, bool> predicate)
        {
            return _repository.Find(predicate);
        }

        public void Add(OrderItem orderItem)
        {
            _repository.Add(orderItem);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(OrderItem orderItem)
        {
            _repository.Edit(orderItem);
        }

        public IEnumerable<OrderItem> GetItemsOfOrder(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<OrderItem> orderItems = _repository.GetAll().ToList();
            IEnumerable<OrderItem> orderItems2 = orderItems.Where(o => o.Order.Id == id).ToList();
            return orderItems2;
        }
    }
}