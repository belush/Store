using System.Collections.Generic;
using Store.BLL.DTO;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IOrderLogic
    {
        IEnumerable<OrderDTO> GetAll();
        void ProcessOrder(Cart cart, DeliveryDTO delivery);
        void ProcessOrder(Cart cart, DeliveryDTO delivery, UserDTO client);
        OrderDTO Get(int? id);
        void Add(OrderDTO good);
        void Edit(OrderDTO good);
    }
}