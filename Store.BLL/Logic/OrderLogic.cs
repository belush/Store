using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using Store.DAL.Repositories;

namespace Store.BLL.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderItemLogic _orderItemLogic;
        private readonly IRepository<Order> _repository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IStatusLogic _statusLogic;
        private readonly IGoodLogic _goodLogic;
        private readonly IClientRepository _clientRepository;

        public OrderLogic(IRepository<Order> repository, IStatusLogic statusLogic, IOrderItemLogic orderItemLogic,
            IClientRepository clientRepository, IOrderItemRepository orderItemRepository, IGoodLogic goodLogic)
        {
            _repository = repository;
            _statusLogic = statusLogic;
            _orderItemLogic = orderItemLogic;
            _clientRepository = clientRepository;
            _orderItemRepository = orderItemRepository;
            _goodLogic = goodLogic;
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            var orders = _repository.GetAll();
            var ordersDto = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);

            return ordersDto;
        }

        public void ProcessOrder(Cart cart, DeliveryDTO deliveryDto)
        {
            var orderItemsDto = cart.Lines;
            var orderItems = Mapper.Map<IEnumerable<OrderItemDTO>, IEnumerable<OrderItem>>(orderItemsDto);

            var order = new Order();
            order.OrderItems = orderItems;
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

            OrderDTO orderDto = new OrderDTO();

            orderDto.Status = _statusLogic.Get(1);

            orderDto.DateCreation = DateTime.Now;
            orderDto.DateSale = DateTime.Now;
            orderDto.User = userDto;
            orderDto.Sum = cart.Lines.Sum(x => x.PriceSale * x.Number);
            orderDto.Delivery = deliveryDto;

            Order order = new Order();
            order.Id = orderDto.Id;
            order.User = _clientRepository.Get(userDto.Id);
            order.DateCreation = DateTime.Now;
            order.DateSale = DateTime.Now;
            order.Sum = orderDto.Sum;

            var delivery = Mapper.Map<DeliveryDTO, Delivery>(deliveryDto);
            var status = Mapper.Map<StatusDTO, Status>(_statusLogic.Get(1));

            order.Status = status;
            order.Delivery = delivery;

            _repository.Add(order);

            var items = cart.Lines;

            foreach (OrderItemDTO itemDto in items)
            {
                itemDto.Good = _goodLogic.Get(itemDto.Good.Id);
                itemDto.Order = new OrderDTO { Id = order.Id };
                _orderItemLogic.Add(itemDto);
            }
        }

        public OrderDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("id null");
            }

            var order = _repository.Get(id.Value);
            var orderDto = Mapper.Map<Order, OrderDTO>(order);

            return orderDto;
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