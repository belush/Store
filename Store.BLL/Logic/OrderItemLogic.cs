using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.BLL.Logic
{
    public class OrderItemLogic : IOrderItemLogic
    {
        private readonly IOrderItemRepository _repository;

        public OrderItemLogic(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrderItemDTO> GetAll()
        {
            var orderItems = _repository.GetAll().ToList();
            var orderItemsDto = Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems);

            return orderItemsDto;
        }

        public IEnumerable<OrderItemDTO> GetByOrderNull()
        {
            var orderItems = _repository.GetByOrderNull();
            var orderItemsDto = Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems);
            return orderItemsDto;
        }

        public OrderItemDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }
            var orderItem = _repository.Get(id.Value);
            var orderItemDto = Mapper.Map<OrderItem, OrderItemDTO>(orderItem);

            return orderItemDto;
        }

        public void Add(OrderItemDTO orderItemDto)
        {
            var orderItem = Mapper.Map<OrderItemDTO, OrderItem>(orderItemDto);
            _repository.Add(orderItem);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Edit(OrderItemDTO orderItemDto)
        {
            var orderItem = Mapper.Map<OrderItemDTO, OrderItem>(orderItemDto);
            _repository.Edit(orderItem);
        }

        public IEnumerable<OrderItemDTO> GetItemsOfOrder(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var orderItems = _repository.GetItemsOfOrder(id);

            var orderItemsDto = Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems);
            return orderItemsDto;
        }
    }
}