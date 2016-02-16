using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Store.BLL.Interfaces;
using Store.BLL.Logic;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Repositories;

namespace Store.WEB.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly IGoodLogic _goodLogic;
        private readonly IOrderItemLogic _orderItemLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly IStatusLogic _statusLogic;

        public OrderItemsController()
        {
            var context = new StoreContext();
            _orderItemLogic = new OrderItemLogic(new OrderItemRepository(context));
            _orderLogic = new OrderLogic(new OrderRepository(context));
            _goodLogic = new GoodLogic(new GoodRepository(context));
            _statusLogic = new StatusLogic(new StatusRepository(context));
        }

        public ActionResult Index()
        {
            IEnumerable<OrderItem> items = _orderItemLogic.GetByOrderNull();
            return View(items);
        }

        [HttpPost]
        public ActionResult Index(int? a)
        {
            

            Order order = new Order();
            ///TODO: refactor 1
            Status status = _statusLogic.Get(1);
            ///TODO: refactor LINQ
            IEnumerable<OrderItem> orderItems = _orderItemLogic.GetByOrderNull();

            order.OrderItems = orderItems.ToList();
            order.DateCreation = DateTime.Now;
            order.DateSale = DateTime.Now;
            order.Status = status;

            _orderLogic.Add(order);

            return RedirectToAction("Index", "Order");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orderItem = _orderItemLogic.Get(id);

            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        public ActionResult Create(int? id)
        {
            var good = _goodLogic.Get(id);
            var orderItem = new OrderItem {Good = good};

            return View(orderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PriceSale,Number")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                ///TODO: refactor 1
                Good good = _goodLogic.Get(1);
                orderItem.Good = good;
                _orderItemLogic.Add(orderItem);

                return RedirectToAction("Index");
            }

            return View(orderItem);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderItem = _orderItemLogic.Get(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PriceSale,Number")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _orderItemLogic.Edit(orderItem);
                return RedirectToAction("Index");
            }
            return View(orderItem);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderItem = _orderItemLogic.Get(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderItemLogic.Delete(id);

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}