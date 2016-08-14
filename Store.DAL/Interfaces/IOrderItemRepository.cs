using System.Collections.Generic;
using Store.DAL.Entities;

namespace Store.DAL.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        IEnumerable<OrderItem> GetByOrderNull();
        IEnumerable<OrderItem> GetItemsOfOrder(int? id);
    }
}