using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.BLL.Interfaces;

namespace Store.WEB.Helpers
{
    public class GoodHelper
    {

        private readonly ICategoryLogic _categoryLogic;
        private readonly IColorLogic _colorLogic;

        public GoodHelper(IColorLogic colorLogic, ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
            _colorLogic = colorLogic;
        }

        public List<SelectListItem> GetColors()
        {
            var colors = _colorLogic.GetAll().
               Select(s => new SelectListItem
               {
                   Text = s.Name,
                   Value = s.Id.ToString()
               }).ToList();
            colors.Add(new SelectListItem { Value = "0", Text = "Любой", Selected = true });

            return colors;
        }

        public List<SelectListItem> GetCategories()
        {
            var categories = _categoryLogic.GetAll().
              Select(s => new SelectListItem
              {
                  Text = s.Name,
                  Value = s.Id.ToString()
              }).ToList();
            categories.Add(new SelectListItem { Value = "0", Text = "Любая", Selected = true });

            return categories;
        }
      
    }
}