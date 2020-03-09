using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;

namespace MinisitreFin.Controllers
{
    public class ActivitesController : Controller
    {
        private MinistreFinEntities db = new MinistreFinEntities();

        // GET: Activites
        public ActionResult Index()
        {
            var activites = db.Activites.Include(a => a.Type_Activite);
            return View(activites.ToList());
        }

        // GET: Activites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite activites = db.Activites.Find(id);
            if (activites == null)
            {
                return HttpNotFound();
            }
            return View(activites);
        }

        // GET: Activites/Create
        public ActionResult Create()
        {
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type");
            return View();
        }

        // POST: Activites/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type_ActiviteID,Nom_activ,Objectif_activ,Date")] Activite activites)
        {
            if (ModelState.IsValid)
            {
                db.Activites.Add(activites);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            return View(activites);
        }

        // GET: Activites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite activites = db.Activites.Find(id);
            if (activites == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            return View(activites);
        }

        // POST: Activites/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type_ActiviteID,Nom_activ,Objectif_activ,Date")] Activite activites)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activites).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            return View(activites);
        }

        // GET: Activites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite activites = db.Activites.Find(id);
            if (activites == null)
            {
                return HttpNotFound();
            }
            return View(activites);
        }

        // POST: Activites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activite activites = db.Activites.Find(id);
            db.Activites.Remove(activites);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
