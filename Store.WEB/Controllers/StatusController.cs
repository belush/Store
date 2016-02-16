using System.Linq;
using System.Net;
using System.Web.Mvc;
using Store.BLL.Interfaces;
using Store.BLL.Logic;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Repositories;

namespace Store.WEB.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusLogic _statusLogic;

        public StatusController()
        {
            var context = new StoreContext();

            _statusLogic = new StatusLogic(new StatusRepository(context));
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

        //}
        //    base.Dispose(disposing);
        //    }
        //        db.Dispose();
        //    {
        //    if (disposing)
        //{

        //protected override void Dispose(bool disposing)
    }
}