using System.Net;
using System.Web.Mvc;
using Store.BLL.Interfaces;
using Store.DAL.Entities;

namespace Store.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class StatusController : Controller
    {
        private readonly IStatusLogic _statusLogic;

        public StatusController(IStatusLogic statusLogic)
        {
            _statusLogic = statusLogic;
        }

        public ActionResult Index()
        {
            return View(_statusLogic.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                _statusLogic.Add(status);

                return RedirectToAction("Index");
            }

            return View(status);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var status = _statusLogic.Get(id);

            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                _statusLogic.Edit(status);
                return RedirectToAction("Index");
            }
            return View(status);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var status = _statusLogic.Get(id);

            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _statusLogic.Delete(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);

        //}
    }
}