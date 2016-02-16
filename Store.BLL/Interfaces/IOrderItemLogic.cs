using System;
using System.Collections.Generic;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IOrderItemLogic
    {
        IEnumerable<OrderItem> GetAll();
        OrderItem Get(int? id);
        IEnumerable<OrderItem> Find(Func<OrderItem, bool> predicate);
        void Add(OrderItem good);
        void Delete(int id);
        void Edit(OrderItem good);
        IEnumerable<OrderItem> GetItemsOfOrder(int? id);
        IEnumerable<OrderItem> GetByOrderNull();
    }
}