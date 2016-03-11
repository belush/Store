using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.WEB.Models;
using Store.WEB.Models.OrderViewModels;
using WebGrease.Css.Extensions;

namespace Store.WEB.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var orders = _orderLogic.GetAll().OrderByDescending(o => o.Id).ToList();
            var orderViews = Mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderViewModel>>(orders);
            return View(orderViews);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartViewModel cartViewModel, List<OrderItemDTO> orderItemsDto)
        {
            //var order = new Order();
            var orderDto = new OrderDTO();

            if (ModelState.IsValid)
            {
                //todo: refactor DTO
                orderDto.OrderItems = cartViewModel.OrderItems;
                orderDto.DateCreation = cartViewModel.DateCreation;
                orderDto.DateSale = cartViewModel.DateSale;
                orderDto.Id = cartViewModel.Id;
                orderDto.Status = cartViewModel.Status;
                orderDto.Sum = cartViewModel.Sum;

                _orderLogic.Add(orderDto);
                return RedirectToAction("Index");
            }

            return View(orderDto);
        }

        [Authorize(Roles = "admin")]
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

            var items = _orderItemLogic.GetItemsOfOrder(id);
            ViewBag.CartInfo = items;

            var order = _orderLogic.Get(id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [Authorize(Roles = "user, admin")]
        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = _orderLogic.Get(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            if (order.User.Id != User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Account");
            }

            var items = _orderItemLogic.GetItemsOfOrder(id);
            ViewBag.CartInfo = items;

            return View(order);
        }

        [Authorize(Roles = "admin")]
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

            var orderDetails = new OrderDetailsModel
            {
                Id = order.Id,
                DateCreation = order.DateCreation,
                DateSale = order.DateSale.ToShortDateString(),
                Status = order.Status.Name,
                Sum = order.Sum
            };

            if (order.User != null)
            {
                orderDetails.User = order.User.Name;
            }

            orderDetails.Statuses = statuses;

            return PartialView(orderDetails);
        }
    }
}