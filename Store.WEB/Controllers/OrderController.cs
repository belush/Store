using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
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
                //TODO: check
                //order.User = cartViewModel.User;
                orderDto.Sum = cartViewModel.Sum;

                _orderLogic.Add(orderDto);
                return RedirectToAction("Index");
            }

            return View(orderDto);
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

            var items = _orderItemLogic.GetItemsOfOrder(id);
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

            var orderDetails = new OrderDetailsModel
            {
                Id = order.Id,
                DateCreation = order.DateCreation,
                DateSale = order.DateSale.ToShortDateString(),
                Status = order.Status.Name,
                User = order.User.Name,
                Sum = order.Sum
            };
            orderDetails.Statuses = statuses;

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
            return PartialView(orderDetails);
            return PartialView(order);
        }
    }
}