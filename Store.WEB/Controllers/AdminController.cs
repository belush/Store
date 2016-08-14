using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.WEB.Helpers;
using Store.WEB.Models;
using Store.WEB.Models.GoodViewModels;
using Store.WEB.Models.ReportViewModels;

namespace Store.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AdminHelper _adminHelper;
        private readonly ICategoryLogic _categoryLogic;
        private readonly IColorLogic _colorLogic;
        private readonly IGoodLogic _goodLogic;
        private readonly IOrderItemLogic _orderItemLogic;

        public AdminController(IGoodLogic goodLogic, IColorLogic colorLogic, ICategoryLogic categoryLogic,
            IOrderItemLogic orderItemLogic)
        {
            _goodLogic = goodLogic;
            _categoryLogic = categoryLogic;
            _colorLogic = colorLogic;
            _orderItemLogic = orderItemLogic;
            _adminHelper = new AdminHelper(_colorLogic, _categoryLogic, _orderItemLogic);
        }

        public ActionResult Index()
        {
            var goods = _goodLogic.GetAll();
            var goodViews = _adminHelper.GoodDtoListToGoodAdminViewList(goods);

            return View(goodViews);
        }

        public ViewResult Edit(int id)
        {
            var good = _goodLogic.Get(id);

            var goodEditModel = Mapper.Map<GoodDTO, GoodEditModel>(good);

            goodEditModel.Categories = _adminHelper.GetCategories();
            goodEditModel.Colors = _adminHelper.GetColors();

            return View(goodEditModel);
        }

        [HttpPost]
        public ActionResult Edit(GoodEditModel goodEditModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                goodEditModel.Date = DateTime.Now;

                Mapper.CreateMap<GoodEditModel, GoodDTO>()
                    .ForMember("Category",
                        opt => opt.MapFrom(v => _categoryLogic.Get(v.CategoryId)))
                    .ForMember("Color",
                        opt => opt.MapFrom(v => _colorLogic.Get(v.ColorId)));

                var good = Mapper.Map<GoodEditModel, GoodDTO>(goodEditModel);
                good.OrderItems = _goodLogic.Get(goodEditModel.Id).OrderItems;
                if (upload != null && upload.ContentLength > 0)
                {
                    good.ImageType = upload.ContentType;

                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        good.Image = reader.ReadBytes(upload.ContentLength);
                    }
                }

                _goodLogic.Edit(good);

                return RedirectToAction("Index");
            }
            return View(goodEditModel);
        }

        public ActionResult Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _goodLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ViewResult Create()
        {
            ViewBag.categories = _adminHelper.GetCategories();
            ViewBag.colors = _adminHelper.GetColors();

            var goodCreateModel = _adminHelper.CreateGoodCreateModel();

            return View(goodCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GoodCreateModel goodCreateModel, HttpPostedFileBase upload)
        {
            goodCreateModel.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                var good = _adminHelper.GoodCreateModelToGood(goodCreateModel, upload);

                _goodLogic.Add(good);
                return RedirectToAction("Index");
            }

            return View(goodCreateModel);
        }

        public ActionResult GoodsSearch(string search, FilterModelDTO filterDto)
        {
            var goods = _goodLogic.Search(search, filterDto);

            var goodViews = _adminHelper.GoodDtoListToGoodAdminViewList(goods);

            if (goodViews.Any())
            {
                return PartialView(goodViews);
            }
            return PartialView("SearchNull");
        }

        public ActionResult Report()
        {
            var orderItems = _orderItemLogic.GetAll().Where(i => i.Good != null && i.Order != null).ToList();


            //todo: to finish REPORT
            var report = new List<ReportViewModel>();
            var reportSecond = new List<ReportIndexModel>();

            foreach (var item in orderItems)
            {
                reportSecond.Add(new ReportIndexModel()
                {
                    User = item.Order.User.Name,
                    Good = item.Good.Name,
                    DateSale = item.Order.DateSale,
                    Cost = (double)item.PriceSale
                });
            }

            reportSecond = reportSecond.OrderBy(r => r.User).ToList();

            var newRepot = from item in reportSecond
                           group item by item.User;

            

            return View(newRepot);
            return View(reportSecond);
        }
    }
}