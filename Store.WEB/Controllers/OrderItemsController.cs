using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Store.BLL.Interfaces;
using Store.BLL.Logic;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Repositories;
using Store.WEB.Models;

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
            //TODO: учесть еще и User.ID (User.ID==Current.User.ID)
            var orderItems = _orderItemLogic.GetByOrderNull();

            //TODO: refactor
            var cartViewModel = new CartViewModel();
            cartViewModel.OrderItems = orderItems;


            //return View(orderItems);
            return View(cartViewModel);
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? a)
        {
            var order = new Order();
            ///TODO: refactor 1
            var status = _statusLogic.Get(1);
            ///TODO: refactor LINQ
            var orderItems = _orderItemLogic.GetByOrderNull();

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
            //var orderItem = new OrderItem {Good = good};
            //orderItem.PriceSale = good.PriceSale.Value;

            var itemViewModel = new OrderItemViewModel();
            itemViewModel.Good = good;
            itemViewModel.GoodId = good.Id;

            //return View(orderItem);
            return View(itemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,PriceSale,Number")] OrderItemViewModel itemViewModel)
        public ActionResult Create(OrderItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                ///TODO: refactor 1
                //int goodId = orderItem.Good.Id;
                //var good = _goodLogic.Get(goodId);
                //orderItem.Good = good;
                //orderItem.Good = good;
                var orderItem = new OrderItem();
                orderItem.Good= _goodLogic.Get(itemViewModel.GoodId);
                orderItem.Number = itemViewModel.Number;

                _orderItemLogic.Add(orderItem);

                return RedirectToAction("Index");
            }

            //return View(orderItem);
            return View();
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

        //}
        //    base.Dispose(disposing);
        //    }
        //        //db.Dispose();
        //    {
        //    if (disposing)
        //{

        //protected override void Dispose(bool disposing)
    }
}