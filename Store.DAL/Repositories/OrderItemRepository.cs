using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class OrderItemRepository : Repository, IRepository<OrderItem>
    {
        public OrderItemRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return db.OrderItems.ToList();
        }

        public IEnumerable<OrderItem> GetAllWithOrder()
        {
            return db.OrderItems.Where(oi=>oi.Order!=null).ToList();
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
            db.Entry(orderItem).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}