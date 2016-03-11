using System.Net;
using System.Web.Mvc;
using Store.BLL.DTO;
using Store.BLL.Interfaces;

namespace Store.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class ColorController : Controller
    {
        private readonly IColorLogic _colorLogic;

        public ColorController(IColorLogic colorLogic)
        {
            _colorLogic = colorLogic;
        }

        public ActionResult Index()
        {
            return View(_colorLogic.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ColorDTO colorDto)
        {
            if (ModelState.IsValid)
            {
                _colorLogic.Add(colorDto);
                return RedirectToAction("Index");
            }

            return View(colorDto);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var color = _colorLogic.Get(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ColorDTO colorDto)
        {
            if (ModelState.IsValid)
            {
                _colorLogic.Edit(colorDto);
                return RedirectToAction("Index");
            }
            return View(colorDto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var color = _colorLogic.Get(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _colorLogic.Delete(id);
            return RedirectToAction("Index");
        }
    }
}