using System;
using System.Collections.Generic;
using System.Linq;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IRepository<Order> _orderLogic;
        private readonly IStatusLogic _statusLogic;
        private readonly IOrderItemLogic _orderItemLogic;

        public OrderLogic(IRepository<Order> orderLogic, IStatusLogic statusLogic, IOrderItemLogic orderItemLogic)
        {
            _orderLogic = orderLogic;
            _statusLogic = statusLogic;
            _orderItemLogic = orderItemLogic;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderLogic.GetAll();
        }

        public void ProcessOrder(Cart cart, Delivery delivery)
        {
            var order = new Order();
            order.OrderItems = cart.Lines;
            order.DateCreation = DateTime.Now;
            order.DateSale = DateTime.Now;
            //order.User
            //order.Status;

            _orderLogic.Add(order);
        }

        public void ProcessOrder(Cart cart, Delivery delivery, ClientProfile client)
        {
            foreach (var item in cart.Lines)
            {
                item.PriceSale = item.Good.PriceSale;
            }

            var items = cart.Lines;
            foreach (var item in items)
            {
                _orderItemLogic.Add(item);
            }

            var order = new Order();
            order.Status = _statusLogic.Get(1);
           
            order.DateCreation = DateTime.Now;
            order.DateSale = DateTime.Now;
            order.User = client;
            order.Sum = cart.Lines.Sum(x => x.PriceSale*x.Number);
            order.Delivery = delivery;

            _orderLogic.Add(order);

            //todo: refactor
            foreach (var item in items)
            {
                item.Order = order;
                _orderItemLogic.Edit(item);
            }
            order.OrderItems = items;

            _orderLogic.Edit(order);
            
        }

        public Order Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            return _orderLogic.Get(id.Value);
        }

        public void Add(Order order)
        {
            _orderLogic.Add(order);
        }

        public void Edit(Order order)
        {
            _orderLogic.Edit(order);
        }
    }
}