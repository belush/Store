using System;
using System.Collections.Generic;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IRepository<Order> _repository;

        public OrderLogic(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Order> GetAll()
        {
            return _repository.GetAll();
        }

        public void ProcessOrder(Cart cart, Delivery delivery)
        {
            var order = new Order();
            order.OrderItems = cart.Lines;
            order.DateCreation = DateTime.Now;
            order.DateSale = DateTime.Now;
            //order.User;
            //order.Status;

            _repository.Add(order);
        }

        public Order Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            return _repository.Get(id.Value);
        }

        public void Add(Order order)
        {
            _repository.Add(order);
        }

        public void Edit(Order order)
        {
            _repository.Edit(order);
        }
    }
}