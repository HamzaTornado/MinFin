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
    public class InitiativesController : Controller
    {
        private MinistreFinEntities db = new MinistreFinEntities();
        [Authorize]
        // GET: Initiatives
        public ActionResult Index()
        {
            var initiatives = db.Initiatives.Include(i => i.Utilisateur);
            return View(initiatives.ToList());
        }

        // GET: Initiatives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Initiative initiatives = db.Initiatives.Find(id);
            if (initiatives == null)
            {
                return HttpNotFound();
            }
            return View(initiatives);
        }

        // GET: Initiatives/Create
        public ActionResult Create()
        {
            ViewBag.UtilisateurID = new SelectList(db.Utilisateurs, "ID", "Nom");
            return View();
        }

        // POST: Initiatives/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UtilisateurID,Nom_init,Statu_init,Date_debu,Date_fin,Objectifs_generaux,Obgectifs_specifiques,Description_court,Description_detaillee,Budget,Approbateur,Cofinancement,Regions")] Initiative initiatives)
        {
            if (ModelState.IsValid)
            {
                db.Initiatives.Add(initiatives);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(initiatives);
        }

        // GET: Initiatives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Initiative initiatives = db.Initiatives.Find(id);
            if (initiatives == null)
            {
                return HttpNotFound();
            }
            return View(initiatives);
        }

        // POST: Initiatives/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UtilisateurID,Nom_init,Statu_init,Date_debu,Date_fin,Objectifs_generaux,Obgectifs_specifiques,Description_court,Description_detaillee,Budget,Approbateur,Cofinancement,Regions")] Initiative initiatives)
        {
            if (ModelState.IsValid)
            {
                db.Entry(initiatives).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(initiatives);
        }

        // GET: Initiatives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Initiative initiatives = db.Initiatives.Find(id);
            if (initiatives == null)
            {
                return HttpNotFound();
            }
            return View(initiatives);
        }

        // POST: Initiatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Initiative initiatives = db.Initiatives.Find(id);
            db.Initiatives.Remove(initiatives);
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
