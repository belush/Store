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
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderItemLogic _orderItemLogic;
        private readonly IRepository<Order> _repository;
        private readonly IStatusLogic _statusLogic;

        public OrderLogic(IRepository<Order> repository, IStatusLogic statusLogic, IOrderItemLogic orderItemLogic)
        {
            _repository = repository;
            _statusLogic = statusLogic;
            _orderItemLogic = orderItemLogic;
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            var orders = _repository.GetAll();
            var ordersDTO = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);

            return ordersDTO;
        }

        public void ProcessOrder(Cart cart, DeliveryDTO deliveryDto)
        {
            var order = new Order();
            order.OrderItems = cart.Lines;
            order.DateCreation = DateTime.Now;
            order.DateSale = DateTime.Now;
            //order.User
            //order.Status;

            _repository.Add(order);
        }

        public void ProcessOrder(Cart cart, DeliveryDTO deliveryDto, UserDTO userDto)
        {
            foreach (var item in cart.Lines)
            {
                item.PriceSale = item.Good.PriceSale;
            }

            var items = cart.Lines;
            var orderItemsDto = Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(items);


            foreach (var item in orderItemsDto)
            {
                //todo: refactor DTO!!
                _orderItemLogic.Add(item);
            }

            var orderDto = new OrderDTO();

            orderDto.Status = _statusLogic.Get(1);

            orderDto.DateCreation = DateTime.Now;
            orderDto.DateSale = DateTime.Now;
            orderDto.User = userDto;
            orderDto.Sum = cart.Lines.Sum(x => x.PriceSale * x.Number);
            orderDto.Delivery = deliveryDto;

            var order = Mapper.Map<OrderDTO, Order>(orderDto);

            //todo: refactor DTO !!!!!!
            //var order = new Order();

            //order.Status = _statusLogic.Get(1);

            //order.DateCreation = DateTime.Now;
            //order.DateSale = DateTime.Now;
            //order.User = userDto;
            //order.Sum = cart.Lines.Sum(x => x.PriceSale * x.Number);
            //order.Delivery = deliveryDto;

            _repository.Add(order);

            ////todo: refactor
            foreach (var item in orderItemsDto)
            {
                item.Order = orderDto;
                _orderItemLogic.Edit(item);
            }
            order.OrderItems = items;

            _repository.Edit(order);
        }

        public OrderDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            var order = _repository.Get(id.Value);
            var orderDTO = Mapper.Map<Order, OrderDTO>(order);

            return orderDTO;
        }

        public void Add(OrderDTO orderDto)
        {
            var order = Mapper.Map<OrderDTO, Order>(orderDto);
            _repository.Add(order);
        }

        public void Edit(OrderDTO orderDto)
        {
            var order = Mapper.Map<OrderDTO, Order>(orderDto);
            _repository.Edit(order);
        }
    }
}