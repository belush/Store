using System;
using System.Collections;
using System.Collections.Generic;
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
    public class OrderController : Controller
    {
        private readonly IOrderItemLogic _orderItemLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly IStatusLogic _statusLogic;

        public OrderController()
        {
            var context = new StoreContext();

            _orderLogic = new OrderLogic(new OrderRepository(context));
            _orderItemLogic = new OrderItemLogic(new OrderItemRepository(context));
            _statusLogic = new StatusLogic(new StatusRepository(context));
        }

        public ActionResult Index()
        {
            return View(_orderLogic.GetAll().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Sum,DateCreation,DateSale")] Order order)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(order);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<SelectListItem> statuses = _statusLogic.GetAll().
                Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            ViewBag.Statuses = statuses;

            Order order = _orderLogic.Get(id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Details(int? id, string status)
        {
            //TODO: refactor use modelview  
            //TODO: узнать стоит ли перенести этот код в BLL (using MVC in BLL)
            List<SelectListItem> statuses = _statusLogic.GetAll().
                Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            ViewBag.Statuses = statuses;

            Order order = _orderLogic.Get(id);
            int statusId = int.Parse(status);
            order.Status = _statusLogic.Get(statusId);

            _orderLogic.Edit(order);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //    base.Dispose(disposing);
        //    }
        //        db.Dispose();
        //    {
        //    if (disposing)
        //{


        //protected override void Dispose(bool disposing)
        //}
    }
}