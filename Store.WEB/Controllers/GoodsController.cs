using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.WEB.Helpers;
using Store.WEB.Models;
using Store.WEB.Models.GoodViewModels;
using PagedList.Mvc;
using PagedList;

namespace Store.WEB.Controllers
{
    public class GoodsController : Controller
    {
        private readonly ICategoryLogic _categoryLogic;
        private readonly IClientLogic _clientLogic;
        private readonly IColorLogic _colorLogic;
        private readonly GoodHelper _goodHelper;
        private readonly IGoodLogic _goodLogic;
        public int pageSize = 5;

        public GoodsController(IGoodLogic goodLogic, IColorLogic colorLogic, ICategoryLogic categoryLogic,
            IClientLogic clientLogic)
        {
            _goodLogic = goodLogic;
            _categoryLogic = categoryLogic;
            _colorLogic = colorLogic;
            _clientLogic = clientLogic;
            _goodHelper = new GoodHelper(colorLogic, categoryLogic);
        }

        public ActionResult Index()
        {
            ViewBag.Categories = _goodHelper.GetCategories();
            ViewBag.Colors = _goodHelper.GetColors();

            var goods = _goodLogic.GetAll();
            var goodViews = Mapper.Map<IEnumerable<GoodDTO>, IEnumerable<GoodViewModel>>(goods);

            return View(goodViews);
        }

        public ActionResult GoodsSearch(string search, FilterModelDTO filterDto, int page = 1)
        {
            var goods = _goodLogic.Search(search, filterDto);

            var goodViews = Mapper.Map<IEnumerable<GoodDTO>, IEnumerable<GoodViewModel>>(goods);

            var id = User.Identity.GetUserId();

            var user = _clientLogic.Get(id);

            if (user!=null)
            {
                foreach (var goodView in goodViews)
                {

                    var dis = (100 - user.Discount) / 100;
                    goodView.PriceWithDiscount = goodView.PriceSale * (decimal)dis;
                }
            }
        

            //todo: refactor -take goods grop _goodLogic
            int pageSize = 5; // количество объектов на страницу
            IEnumerable<GoodViewModel> goodssPerPages = goodViews.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = goodViews.Count() };
            GoodListViewModel ivm = new GoodListViewModel { PageInfo = pageInfo, Goods = goodssPerPages };

            if (ivm.Goods.Any())
            {
                return PartialView(goodViews.ToPagedList(page, pageSize));
            }

            return PartialView("SearchNull");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var good = _goodLogic.Get(id);

            if (good == null)
            {
                return HttpNotFound();
            }
            return View(good);
        }

        public FileContentResult GetImage(int id)
        {
            var good = _goodLogic.Get(id);

            if (good != null)
            {
                return File(good.Image, good.ImageType);
            }
            return null;
        }
    }
}