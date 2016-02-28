using System.Collections.Generic;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IOrderLogic
    {
        IEnumerable<Order> GetAll();
        void ProcessOrder(Cart cart, Delivery delivery);
        void ProcessOrder(Cart cart, Delivery delivery, ClientProfile client);
        Order Get(int? id);
        //IEnumerable<Order> Find(Func<Order, bool> predicate);
        void Add(Order good);
        //void Delete(int id);
        void Edit(Order good);
    }
}