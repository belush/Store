using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.WEB.Models;

namespace Store.WEB.Helpers
{
    public class AdminHelper
    {
        private readonly ICategoryLogic _categoryLogic;
        private readonly IColorLogic _colorLogic;
        private readonly IOrderItemLogic _orderItemLogic;

        public AdminHelper(IColorLogic colorLogic, ICategoryLogic categoryLogic, IOrderItemLogic orderItemLogic)
        {
            _categoryLogic = categoryLogic;
            _colorLogic = colorLogic;
            _orderItemLogic = orderItemLogic;
        }

        public AdminHelper()
        {
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            var categories = _categoryLogic.GetAll().ToList().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            return categories;
        }

        public IEnumerable<SelectListItem> GetColors()
        {
            var colors = _colorLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            return colors;
        }

        public IEnumerable<GoodAdminView> GoodDtoListToGoodAdminViewList(IEnumerable<GoodDTO> goods)
        {
            var categories = _categoryLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            categories.Add(new SelectListItem {Value = "0", Text = "Любая", Selected = true});

            var colors = _colorLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            colors.Add(new SelectListItem {Value = "0", Text = "Любой", Selected = true});

            var goodViews = Mapper.Map<IEnumerable<GoodDTO>, IEnumerable<GoodAdminView>>(goods);

            if (goodViews.Count() > 0)
            {
                goodViews.FirstOrDefault().Categories = categories;
                goodViews.FirstOrDefault().Colors = colors;
            }

            return goodViews;
        }

        public GoodCreateModel GoodDtoToGoodCreateModel(GoodDTO goodDto)
        {
            var goodCreateModel = Mapper.Map<GoodDTO, GoodCreateModel>(goodDto);

            var categories = _categoryLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            var colors = _colorLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            goodCreateModel.Categories = categories;
            goodCreateModel.Colors = colors;

            return goodCreateModel;
        }

        public GoodDTO GoodCreateModelToGood(GoodCreateModel goodCreateModel, HttpPostedFileBase upload)
        {
            goodCreateModel.Date = DateTime.Now;

            Mapper.CreateMap<GoodCreateModel, GoodDTO>()
                  .ForMember("Category",
                      opt => opt.MapFrom(v => _categoryLogic.Get(v.CategoryId)))
                  .ForMember("Color",
                      opt => opt.MapFrom(v => _colorLogic.Get(v.ColorId)));
                  

            var good = Mapper.Map<GoodCreateModel, GoodDTO>(goodCreateModel);

            if (upload != null && upload.ContentLength > 0)
            {
                good.ImageType = upload.ContentType;

                using (var reader = new BinaryReader(upload.InputStream))
                {
                    good.Image = reader.ReadBytes(upload.ContentLength);
                }
            }

            return good;
        }

        public GoodCreateModel CreateGoodCreateModel()
        {
            GoodCreateModel goodCreateModel = new GoodCreateModel();

            var categories = _categoryLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            var colors = _colorLogic.GetAll().
                Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();

            goodCreateModel.Categories = categories;
            goodCreateModel.Colors = colors;

            return goodCreateModel;
        }
    }
}