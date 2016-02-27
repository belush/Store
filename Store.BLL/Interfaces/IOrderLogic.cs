using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IOrderLogic
    {
        IEnumerable<Order> GetAll();
        void ProcessOrder(Cart cart, Delivery delivery);
        Order Get(int? id);
        //IEnumerable<Order> Find(Func<Order, bool> predicate);
        void Add(Order good);
        //void Delete(int id);
        void Edit(Order good);
    }
}
