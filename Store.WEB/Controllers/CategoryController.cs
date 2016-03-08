using System.Net;
using System.Web.Mvc;
using Store.BLL.DTO;
using Store.BLL.Interfaces;

namespace Store.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryLogic _categoryLogic;

        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        public ActionResult Index()
        {
            return View(_categoryLogic.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                _categoryLogic.Add(categoryDto);

                return RedirectToAction("Index");
            }

            return View(categoryDto);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryLogic.Get(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                _categoryLogic.Edit(categoryDto);

                return RedirectToAction("Index");
            }
            return View(categoryDto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryLogic.Get(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryLogic.Delete(id);
            return RedirectToAction("Index");
        }
    }
}