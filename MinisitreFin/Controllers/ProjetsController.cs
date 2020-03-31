using MinisitreFin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MinisitreFin.Controllers
{
    [ValidateInput(false)]
    public class ProjetsController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        
        // GET: Projets
        public ActionResult Index()
        {
            var projets = db.Projet.Include(p => p.Initiatives);
            return View(projets.ToList());
        }
       
        // GET: Projets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = db.Projet.Find(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            return View(projet);
        }
        
        // GET: Projets/Create
        public ActionResult Create()
        {
            ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init");
            return View();
        }

        // POST: Projets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_Initiative,Nom_projet,Description,Objectif_projet,Date_debut,Date_fin")] Projet projet)
        {
            if (ModelState.IsValid)
            {
                db.Projet.Add(projet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projet.ID_Initiative);
            return View(projet);
        }
        
        // GET: Projets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = db.Projet.Find(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projet.ID_Initiative);
            return View(projet);
        }
        

        // POST: Projets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Initiative,Nom_projet,Description,Objectif_projet,Date_debut,Date_fin")] Projet projet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projet.ID_Initiative);
            return View(projet);
        }
      

        // GET: Projets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = db.Projet.Find(id);
            if (projet == null)
            {
                return HttpNotFound();
            }
            return View(projet);
        }
     
        // POST: Projets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projet projet = db.Projet.Find(id);
            db.Projet.Remove(projet);
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
