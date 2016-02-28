﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Store.BLL.Interfaces;
using Store.BLL.Logic;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Repositories;
using Store.WEB.Models;

namespace Store.WEB.Controllers
{
    public class GoodsController : Controller
    {
        private readonly ICategoryLogic _categoryLogic;
        private readonly IColorLogic _colorLogic;
        private readonly IGoodLogic _goodLogic;

        public GoodsController(IGoodLogic goodLogic, IColorLogic colorLogic, ICategoryLogic categoryLogic)
        {
            _goodLogic = goodLogic;
            _categoryLogic = categoryLogic;
            _colorLogic = colorLogic;
        }

        public ActionResult Index()
        {
            //TODO: remove to BLL
            var categories = _categoryLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            categories.Add(new SelectListItem {Value = "0", Text = "Любой", Selected = true});

            //TODO: remove to BLL
            var colors = _colorLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            colors.Add(new SelectListItem {Value = "0", Text = "Любая", Selected = true});

            ViewBag.Categories = categories;
            ViewBag.Colors = colors;

            var goods = _goodLogic.GetAll();
            var goodViews = Mapper.Map<IEnumerable<Good>, IEnumerable<GoodViewModel>>(goods);

            return View(goodViews);
        }

        public ActionResult GoodsSearch(string search, FilterModel filter)
        {
            var goods = _goodLogic.GetAll().ToList();
            IEnumerable<Good> goodsResult = goods;
            if (filter.CategoryId == 0)
            {
                IEnumerable<Good> goodsTemp = _goodLogic.GetAll().ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }
            else if (filter.CategoryId > 0)
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.Category.Id == filter.CategoryId).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.ColorId == 0)
            {
                IEnumerable<Good> goodsTemp = _goodLogic.GetAll().ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }
            else if (filter.ColorId > 0)
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.Color.Id == filter.ColorId).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (!string.IsNullOrEmpty(search))
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.Name.ToLower().StartsWith(search.ToLower())).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.PriceFrom != 0)
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.PriceSale > filter.PriceFrom).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.PriceTo != 0)
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.PriceSale < filter.PriceTo).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }


            if (filter.SizeD != 0)
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.SizeDepth == filter.SizeD).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.SizeH != 0)
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.SizeHeight == filter.SizeH).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.SizeW != 0)
            {
                IEnumerable<Good> goodsTemp =
                    _goodLogic.GetAll().ToList().Where(i => i.SizeWidth == filter.SizeW).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            var goodViews = goodsResult.Select(good => new GoodViewModel
            {
                Id = good.Id,
                Name = good.Name,
                Count = good.Count,
                Category = good.Category.Name,
                Color = good.Color.Name,
                PriceSale = good.PriceSale,
                Date = good.Date,
                SizeDepth = good.SizeDepth,
                SizeHeight = good.SizeHeight,
                SizeWidth = good.SizeWidth,
                Description = good.Description
                //Image = good.Image
            }).ToList();

            //return Json(goodViews, JsonRequestBehavior.AllowGet);
            if (goodViews.Count() > 0)
            {
                return PartialView(goodViews);
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

        public ActionResult Create()
        {
            //TODO: remove to BLL
            var categories = _categoryLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            //TODO: remove to BLL
            var colors = _colorLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            ViewBag.Categories = categories;
            ViewBag.Colors = colors;

            //TODO: use view model!!!
            //TODO: use helper in: goods out: selected list!!!


            return View();
        }

        //[Bind(Include = "Id,Name,Date,Description,Image,Count")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GoodCreateModel goodCreateModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                ///TODO: refactor and remove to certain class

                //Mapper.CreateMap<GoodCreateModel, Good>()
                //  .ForMember("Category",
                //      opt => opt.MapFrom(v => _categoryLogic.Get(v.CategoryId)))
                //  .ForMember("Color",
                //      opt => opt.MapFrom(v => _colorLogic.Get(v.ColorId)));


                //var good = Mapper.Map<GoodCreateModel, Good>(goodCreateModel);

                //TODO: use builder or Helper
                var good = new Good
                {
                    Category = _categoryLogic.Get(goodCreateModel.CategoryId),
                    Color = _colorLogic.Get(goodCreateModel.ColorId),
                    Date = goodCreateModel.Date,
                    Count = goodCreateModel.Count,
                    Name = goodCreateModel.Name,
                    Description = goodCreateModel.Description,
                    PriceIncome = goodCreateModel.PriceIncome,
                    PriceSale = goodCreateModel.PriceSale,
                    SizeDepth = goodCreateModel.SizeDepth,
                    SizeHeight = goodCreateModel.SizeHeight,
                    SizeWidth = goodCreateModel.SizeWidth
                };

                //TODO:use DTO layer

                if (upload != null && upload.ContentLength > 0)
                {
                    good.ImageType = upload.ContentType;

                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        good.Image = reader.ReadBytes(upload.ContentLength);
                    }
                }

                _goodLogic.Add(good);
                return RedirectToAction("Index");
            }

            return View(goodCreateModel);
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

        //TODO: refactor with good view model
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var good = _goodLogic.Get(id);

            //TODO: remove to BLL
            var categories = _categoryLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            //TODO: remove to BLL
            var colors = _colorLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            ViewBag.Categories = categories;
            ViewBag.Colors = colors;

            var goodCreateModel = Mapper.Map<Good, GoodCreateModel>(good);

            if (good == null)
            {
                return HttpNotFound();
            }

            return View(goodCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GoodCreateModel goodCreateModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<GoodCreateModel, Good>()
                    .ForMember("Category",
                        opt => opt.MapFrom(v => _categoryLogic.Get(v.CategoryId)))
                    .ForMember("Color",
                        opt => opt.MapFrom(v => _colorLogic.Get(v.ColorId)));

                var good = Mapper.Map<GoodCreateModel, Good>(goodCreateModel);

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
            return View(goodCreateModel);
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
    }
}