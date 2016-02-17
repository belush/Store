using System.Collections.Generic;
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
        private readonly IGoodLogic _goodLogic;

        public GoodsController()
        {
            var context = new StoreContext();
            _goodLogic = new GoodLogic(new GoodRepository(context));
            _categoryLogic = new CategoryLogic(new CategoryRepository(context));
        }

        public ActionResult Index()
        {
            var goods = _goodLogic.GetAll();


            Mapper.CreateMap<Good, GoodViewModel>()
                .ForMember("PriceIncome",
                    opt => opt.MapFrom(v => v.PriceIncome.Value))
                .ForMember("PriceSale",
                    opt => opt.MapFrom(v => v.PriceSale.Value))
                .ForMember("Category",
                    opt => opt.MapFrom(v => v.Category.Name))
                .ForMember("Color",
                    opt => opt.MapFrom(v => v.Color.Name));

            var goodViews = Mapper.Map<IEnumerable<Good>, IEnumerable<GoodViewModel>>(goods);

            return View(goodViews);
        }

        public ActionResult GoodsSearch(string search, string sort)
        {
            var goods = _goodLogic.GetAll().ToList();

            if (sort == "name")
            {
                goods = goods.OrderBy(g => g.Name).ToList();
            }
            if (sort == "count")
            {
                goods = goods.OrderBy(g => g.Count).ToList();
            }

            var goodViews = goods.Select(good => new GoodCreateModel
            {
                Id = good.Id,
                Name = good.Name,
                Count = good.Count,
                //Category= good.Category.Name,
                //Color= good.Color.Name,
                //Price= good.Price.Value,
                Date = good.Date,
                Description = good.Description,
                Image = good.Image
            }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                goodViews = goodViews.Where(i => i.Name.ToLower().StartsWith(search.ToLower())).ToList();
            }

            //if (!string.IsNullOrEmpty(search))
            //{
            //    goods = goods.Where(i => i.Name.ToLower().StartsWith(search.ToLower())).ToList();
            //}

            return Json(goodViews, JsonRequestBehavior.AllowGet);
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
            var categories = _categoryLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            ViewBag.Categories = categories;

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
                Mapper.CreateMap<GoodCreateModel, Good>()
                    .ForMember("PriceIncome",
                        opt => opt.MapFrom(v => new Price {Value = v.PriceIncome}))
                    .ForMember("PriceSale",
                        opt => opt.MapFrom(v => new Price {Value = v.PriceSale}))
                    .ForMember("Category",
                        opt => opt.MapFrom(v => _categoryLogic.Get(v.CategoryId)))
                    .ForMember("Color",
                        opt => opt.MapFrom(v => new Color {Name = v.ColorId.ToString()}));

                var good = Mapper.Map<GoodCreateModel, Good>(goodCreateModel);


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

        public ActionResult Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,Description,Image,Count")] Good good)
        {
            if (ModelState.IsValid)
            {
                _goodLogic.Edit(good);
                return RedirectToAction("Index");
            }
            return View(good);
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.Dispose();
        //    }
        //    base.Dispose(disposing);

        //}
    }
}