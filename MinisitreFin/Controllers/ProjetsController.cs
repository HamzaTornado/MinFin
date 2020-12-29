using MinisitreFin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisitreFin.ViewModels;


namespace MinisitreFin.Controllers
{
    [ValidateInput(false)]
    [Authorize]
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
        public ActionResult Create(ProjectsViewModel projetsviewmodel)
        {
            if (ModelState.IsValid)
            {
                
                try {
                    Projet projet = new Projet() {
                        ID_Initiative = projetsviewmodel.ID_Initiative,
                        Nom_projet = projetsviewmodel.Nom_projet,
                        Description = projetsviewmodel.Description,
                        Objectif_projet = projetsviewmodel.Objectif_projet,
                        Date_debut = projetsviewmodel.Date_debut,
                        Date_fin = projetsviewmodel.Date_fin
                    };

                    db.Projet.Add(projet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception DbExc)
                {
                    //string error = "";
                    //foreach (var er in DbExc.EntityValidationErrors)
                    //{
                    //    foreach (var ve in er.ValidationErrors)
                    //    {
                    //        error += " - " + ve.ErrorMessage;
                    //    }
                    //}
                    TempData["Message"] = DbExc;
                    ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projetsviewmodel.ID_Initiative);
                    return View(projetsviewmodel);
                }

            }

            ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projetsviewmodel.ID_Initiative);
            return View(projetsviewmodel);
        }
        
        // GET: Projets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projet projet = db.Projet.Find(id);
            ProjectsViewModel projetviewmodel = new ProjectsViewModel()
            {
                ID = projet.ID,
                ID_Initiative = projet.ID_Initiative,
                Nom_projet = projet.Nom_projet,
                Description = projet.Description,
                Objectif_projet = projet.Objectif_projet,
                Date_debut = projet.Date_debut.Value,
                Date_fin = projet.Date_fin.Value
            };
            if (projet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projetviewmodel.ID_Initiative);
            return View(projetviewmodel);
        }
        

        // POST: Projets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectsViewModel projetviewmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Projet projet = new Projet()
                    {
                        ID = projetviewmodel.ID,
                        ID_Initiative = projetviewmodel.ID_Initiative,
                        Nom_projet = projetviewmodel.Nom_projet,
                        Description = projetviewmodel.Description,
                        Objectif_projet = projetviewmodel.Objectif_projet,
                        Date_debut = projetviewmodel.Date_debut,
                        Date_fin = projetviewmodel.Date_fin
                    };
                    db.Entry(projet).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception DbExc)
                {
                    //string error = "";
                    //foreach (var er in DbExc.EntityValidationErrors)
                    //{
                    //    foreach (var ve in er.ValidationErrors)
                    //    {
                    //        error += " - " + ve.ErrorMessage;
                    //    }
                    //}
                    TempData["Message"] = DbExc;
                    ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projetviewmodel.ID_Initiative);
                    return View(projetviewmodel);
                }

            }
            ViewBag.ID_Initiative = new SelectList(db.Initiatives, "ID", "Nom_init", projetviewmodel.ID_Initiative);
            return View(projetviewmodel);
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
