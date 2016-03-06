using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;

namespace Store.DAL.Repositories
{
    public class GoodRepository : Repository, IGoodRepository /*IRepository<Good>*/
    {
        public GoodRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public IEnumerable<Good> GetAll()
        {
            return db.Goods.Where(g => g.IsDeleted == false);
        }

        public Good Get(int id)
        {
            return db.Goods.Find(id);
        }

        public IEnumerable<Good> Find(Func<Good, bool> predicate)
        {
            return db.Goods.Where(predicate);
        }

        public void Add(Good entity)
        {
            entity.Date = DateTime.Now;
            db.Goods.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var good = db.Goods.Find(id);
            if (good != null)
            {
                good.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void Edit(Good entity)
        {
            //db.Entry(entity).State = EntityState.Modified;
            var good = db.Goods.FirstOrDefault(g => g.Id == entity.Id);
            good.Id = entity.Id;
            good.Name = entity.Name;
            good.Description = entity.Description;
            good.ImageType = entity.ImageType;
            good.OrderItems = entity.OrderItems;
            good.Color = entity.Color;
            good.Count = entity.Count;
            good.Date = entity.Date;
            good.Image = entity.Image;
            good.PriceIncome = entity.PriceIncome;
            good.PriceSale = entity.PriceSale;
            good.SizeDepth = entity.SizeDepth;
            good.SizeHeight = entity.SizeHeight;
            good.SizeWidth = entity.SizeWidth;
            good.OrderItems = entity.OrderItems;
            good.Category = entity.Category;
            db.SaveChanges();
        }

        public IEnumerable<Good> Search(string search, FilterModel filter)
        {
            var goods = GetAll().ToList();
            IEnumerable<Good> goodsResult = goods;

            if (filter.CategoryId == 0)
            {
                IEnumerable<Good> goodsTemp = GetAll().ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }
            else if (filter.CategoryId > 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.Category.Id == filter.CategoryId).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.ColorId == 0)
            {
                IEnumerable<Good> goodsTemp = GetAll().ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }
            else if (filter.ColorId > 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.Color.Id == filter.ColorId).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (!string.IsNullOrEmpty(search))
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.Name.ToLower().StartsWith(search.ToLower())).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.PriceFrom != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.PriceSale > filter.PriceFrom).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.PriceTo != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.PriceSale < filter.PriceTo).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }


            if (filter.SizeDFrom != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.SizeDepth >= filter.SizeDFrom).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.SizeHFrom != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.SizeHeight >= filter.SizeHFrom).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.SizeWFrom != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.SizeWidth >= filter.SizeWFrom).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            //
            if (filter.SizeDTo != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.SizeDepth <= filter.SizeDTo).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.SizeHTo != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.SizeHeight <= filter.SizeHTo).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            if (filter.SizeWTo != 0)
            {
                IEnumerable<Good> goodsTemp =
                    GetAll().ToList().Where(i => i.SizeWidth <= filter.SizeWTo).ToList();
                goodsResult = goodsResult.Intersect(goodsTemp);
            }

            return goodsResult;
        }
    }
}