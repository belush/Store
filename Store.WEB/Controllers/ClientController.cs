using System.Web.Mvc;
using Store.BLL.DTO;
using Store.BLL.Interfaces;

namespace Store.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class ClientController : Controller
    {
        private readonly IClientLogic _clientLogic;

        public ClientController(IClientLogic clientLogic)
        {
            _clientLogic = clientLogic;
        }

        public ActionResult Index()
        {
            var clients = _clientLogic.GetAll();
            return View(clients);
        }

        [HttpPost]
        public ActionResult Index(UserDTO userDto)
        {
            if (userDto.Id!=null)
            {

                var user = _clientLogic.Get(userDto.Id);

                if (userDto.Discount>=0 && userDto.Discount<=100)
                {
                    user.Discount = userDto.Discount;
                }

                _clientLogic.Edit(user);
            }           

            return RedirectToAction("Index");
        }

        public ActionResult ChangeStatus(string id)
        {
            var client = _clientLogic.Get(id);

            if (client != null)
            {
                client.IsBlocked = client.IsBlocked == false;
                _clientLogic.Edit(client);
            }

            return PartialView(_clientLogic.GetAll());
        }

        public ActionResult ChangeDiscount(UserDTO userDto, string id, double discount)
            {
            var client = _clientLogic.Get(id);
            client.Discount = discount;

            _clientLogic.Edit(client);

            return PartialView("ChangeStatus",_clientLogic.GetAll());
        }
    }
}