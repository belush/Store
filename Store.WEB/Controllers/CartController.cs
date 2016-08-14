using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Store.BLL;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.WEB.Models;
//using Store.DAL.Entities;

namespace Store.WEB.Controllers
{
    public class CartController : Controller
    {
        private readonly ICategoryLogic _categoryLogic;
        private readonly IClientLogic _clientLogic;
        private readonly IColorLogic _colorLogic;
        private readonly IGoodLogic _goodLogic;
        private readonly IOrderItemLogic _orderItemLogic;
        private readonly IOrderLogic _orderLogic;

        public CartController(ICategoryLogic categoryLogic, IColorLogic colorLogic,
            IGoodLogic goodLogic, IOrderLogic orderLogic, IClientLogic clientLogic, IOrderItemLogic orderItemLogic)
        {
            _goodLogic = goodLogic;
            _categoryLogic = categoryLogic;
            _colorLogic = colorLogic;
            _orderLogic = orderLogic;
            _clientLogic = clientLogic;
            _orderItemLogic = orderItemLogic;
        }

        public ActionResult Index(Cart cart, string returnUrl)
        {
            var cartIndexViewModel = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };

            var client = _clientLogic.Get(User.Identity.GetUserId());
            if (client != null)
            {
                ViewBag.userBlocked = client.IsBlocked;
            }


            return View(cartIndexViewModel);
        }

        [Authorize]
        public ActionResult Checkout(Cart cart)
        {
            var userId = User.Identity.GetUserId();
            var client = _clientLogic.Get(userId);

            if (client.IsBlocked)
            {
                return RedirectToAction("Index", "Goods");
            }

            return View(new DeliveryDTO());
        }

        [Authorize]
        [HttpPost]
        public ViewResult Checkout(Cart cart, DeliveryDTO deliveryDto)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                //todo: refactor
                var userId = User.Identity.GetUserId();
                var client = _clientLogic.Get(userId);

                foreach (var item in cart.Lines)
                {
                    var good = _goodLogic.Get(item.Good.Id);
                    if (good.Count >= item.Number)
                    {
                        good.Count -= item.Number;
                        good.OrderItems = null;
                        _goodLogic.Edit(good);
                    }
                    else
                    {
                        return View("GoodIsOver");
                    }
                }

                _orderLogic.ProcessOrder(cart, deliveryDto, client);
                cart.Clear();

                return View("Completed");
            }
            return View(deliveryDto);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int goodId, string returnUrl)
        {
            var good = _goodLogic.GetAll()
                .FirstOrDefault(g => g.Id == goodId);

            if (good != null)
            {
                cart.AddItem(good, 1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }


        public RedirectToRouteResult RemoveFromCart(Cart cart, int goodId, string returnUrl)
        {
            var good = _goodLogic.Get(goodId);

            if (good != null)
            {
                cart.RemoveLine(good);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}