using System;
using System.Collections.Generic;
using Store.BLL.DTO;

namespace Store.BLL.Interfaces
{
    public interface IOrderItemLogic
    {
        IEnumerable<OrderItemDTO> GetAll();
        OrderItemDTO Get(int? id);
        void Add(OrderItemDTO goodDto);
        void Delete(int id);
        void Edit(OrderItemDTO goodDto);
        IEnumerable<OrderItemDTO> GetItemsOfOrder(int? id);
        IEnumerable<OrderItemDTO> GetByOrderNull();
    }
}