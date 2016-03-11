using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class OrderItemRepository : Repository, IOrderItemRepository
    {
        public OrderItemRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return db.OrderItems.ToList();
        }

        public OrderItem Get(int id)
        {
            return db.OrderItems.Find(id);
        }

        public IEnumerable<OrderItem> Find(Func<OrderItem, bool> predicate)
        {
            return db.OrderItems.Where(predicate);
        }

        public void Add(OrderItem entity)
        {
            entity.Order = db.Orders.Find(entity.Order.Id);
            entity.Good = db.Goods.Find(entity.Good.Id);
            db.OrderItems.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var orderItem = db.OrderItems.Find(id);
            if (orderItem != null)
            {
                db.OrderItems.Remove(orderItem);
                db.SaveChanges();
            }
        }

        public void Edit(OrderItem orderItem)
        {
            //db.Entry(orderItem).State = EntityState.Modified;

            var item = db.OrderItems.FirstOrDefault(i => i.Id == orderItem.Id);

            if (item != null)
            {
                if (orderItem.Order != null)
                {
                    item.Order = orderItem.Order;
                }

                item.Good = orderItem.Good;
                item.Number = orderItem.Number;
                item.PriceSale = orderItem.PriceSale;
            }

            db.SaveChanges();
        }

        public IEnumerable<OrderItem> GetByOrderNull()
        {
            var orderItems = db.OrderItems.Where(o => o.Order == null).ToList();
            return orderItems;
        }

        public IEnumerable<OrderItem> GetItemsOfOrder(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            var orderItems = db.OrderItems.ToList();

            //var orderItems2 = orderItems.Where(o => o.Order.Id == id).ToList();

            return orderItems.Where(item => item.Order != null && item.Order.Id == id).ToList();
        }

        public IEnumerable<OrderItem> GetAllWithOrder()
        {
            return db.OrderItems.Where(oi => oi.Order != null).ToList();
        }
    }
}