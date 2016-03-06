using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.WEB.Models;
using Store.WEB.Models.OrderViewModels;

namespace Store.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly IOrderItemLogic _orderItemLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly IStatusLogic _statusLogic;

        public OrderController(IOrderItemLogic orderItemLogic, IOrderLogic orderLogic, IStatusLogic statusLogic)
        {
            _orderLogic = orderLogic;
            _orderItemLogic = orderItemLogic;
            _statusLogic = statusLogic;
        }

        public ActionResult Index()
        {
            var orders = _orderLogic.GetAll().OrderByDescending(o => o.Id).ToList();
            var orderViews = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            return View(orderViews);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartViewModel cartViewModel, List<OrderItem> orderItems)
        {
            var order = new Order();

            if (ModelState.IsValid)
            {
                order.OrderItems = cartViewModel.OrderItems;
                order.DateCreation = cartViewModel.DateCreation;
                order.DateSale = cartViewModel.DateSale;
                order.Id = cartViewModel.Id;
                order.Status = cartViewModel.Status;
                //TODO: check
                //order.User = cartViewModel.User;
                order.Sum = cartViewModel.Sum;

                _orderLogic.Add(order);
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

            var statuses = _statusLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            ViewBag.Statuses = statuses;

            //todo: refactor
            var items = _orderItemLogic.GetAll().Where(o => o.Order.Id == id);
            ViewBag.CartInfo = items;

            var order = _orderLogic.Get(id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        public ActionResult ChangeStatus(int? id, string status)
        {
            //TODO: refactor use modelview  
            //TODO: узнать стоит ли перенести этот код в BLL (using MVC in BLL)
            var statuses = _statusLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            ViewBag.Statuses = statuses;

            var order = _orderLogic.Get(id);

            if (status != null)
            {
                var statusId = int.Parse(status);
                order.Status = _statusLogic.Get(statusId);

                _orderLogic.Edit(order);
            }

            if (order == null)
            {
                return HttpNotFound();
            }
            //return View(order);
            return PartialView(order);
        }
    }
}