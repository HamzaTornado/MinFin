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
    public class ProgrammesController : Controller
    {
        private MinistreFinEntities db = new MinistreFinEntities();

        // GET: Programmes
        public ActionResult Index()
        {
            var programmes = db.Programmes.Include(p => p.Utilisateur);
            return View(programmes.ToList());
        }

        // GET: Programmes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programme programme = db.Programmes.Find(id);
            if (programme == null)
            {
                return HttpNotFound();
            }
            return View(programme);
        }

        // GET: Programmes/Create
        public ActionResult Create()
        {
            ViewBag.UtilisateurID = new SelectList(db.Utilisateurs, "ID", "Nom");
            return View();
        }

        // POST: Programmes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UtilisateurID,Nom_prog,Objectif_prog,Date_debut,Date_fin,Donateur,budget")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                db.Programmes.Add(programme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UtilisateurID = new SelectList(db.Utilisateurs, "ID", "Nom", programme.UtilisateurID);
            return View(programme);
        }

        // GET: Programmes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programme programme = db.Programmes.Find(id);
            if (programme == null)
            {
                return HttpNotFound();
            }
            ViewBag.UtilisateurID = new SelectList(db.Utilisateurs, "ID", "Nom", programme.UtilisateurID);
            return View(programme);
        }

        // POST: Programmes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UtilisateurID,Nom_prog,Objectif_prog,Date_debut,Date_fin,Donateur,budget")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UtilisateurID = new SelectList(db.Utilisateurs, "ID", "Nom", programme.UtilisateurID);
            return View(programme);
        }

        // GET: Programmes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programme programme = db.Programmes.Find(id);
            if (programme == null)
            {
                return HttpNotFound();
            }
            return View(programme);
        }

        // POST: Programmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Programme programme = db.Programmes.Find(id);
            db.Programmes.Remove(programme);
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
