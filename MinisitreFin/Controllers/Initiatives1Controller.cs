using MinisitreFin.Models;
using MinisitreFin.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MinisitreFin.Controllers
{
    [Authorize(Roles = "Admin,BDF")]
    public class Initiatives1Controller : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: Initiatives1
        //public ActionResult Index()
        //{
        //    var initiatives = db.Initiatives.Include(i => i.Utilisateur);
        //    return View(initiatives.ToList());
        //}
        public ActionResult Index(string chercher, int? page)
        {
            if (chercher != null)
            {
                var initiatives = db.Initiatives;
                return View(initiatives.ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                var initiatives = db.Initiatives;
                return View(initiatives.ToList().ToPagedList(page ?? 1, 3));
            }
        }
        // GET: Initiatives1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Initiatives initiative = db.Initiatives.Find(id);
            if (initiative == null)
            {
                return HttpNotFound();
            }
            return View(initiative);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateStatu(int id)
        {
            try {
                Initiatives ini = db.Initiatives.Find(id);
                ini.Statu_init = !ini.Statu_init.Value;
                db.Initiatives.Attach(ini);
                db.Entry(ini).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception) { }

            return RedirectToAction("Index");
        }
        // GET: Initiatives1/Create
        public ActionResult Create()
        {
            ViewBag.UtilisateurID = new SelectList(db.Utilisateur, "ID", "UserId");
            return View();
        }

        // POST: Initiatives1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InitiativesViewModel initiative)
        {
            if (ModelState.IsValid)
            {
                try {
                    initiative.Statu_init = false;
                    Initiatives init = new Initiatives()
                    {
                        UtilisateurID = initiative.UtilisateurID,
                        Nom_init = initiative.Nom_init,
                        Statu_init = initiative.Statu_init,
                        Date_debu = initiative.Date_debu,
                        Date_fin = initiative.Date_fin,
                        Objectifs_generaux = initiative.Objectifs_generaux,
                        Obgectifs_specifiques = initiative.Obgectifs_specifiques,
                        Description_court = initiative.Description_court,
                        Description_detaillee = initiative.Description_detaillee,
                        Budget = initiative.Budget,
                        Approbateur = initiative.Approbateur,
                        Cofinancement = initiative.Cofinancement,
                        Regions = initiative.Regions,
                    };
                    db.Initiatives.Add(init);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }catch (Exception DbExc)
                {
                    
                    TempData["Message"] = DbExc;
                    ViewBag.UtilisateurID = new SelectList(db.Utilisateur, "ID", "UserId", initiative.UtilisateurID);
                    return View(initiative);
                }

            }

            ViewBag.UtilisateurID = new SelectList(db.Utilisateur, "ID", "UserId", initiative.UtilisateurID);
            return View(initiative);
        }

        // GET: Initiatives1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Initiatives initiative = db.Initiatives.Find(id);
            InitiativesViewModel init = new InitiativesViewModel()
            {
                UtilisateurID = initiative.UtilisateurID,
                Nom_init = initiative.Nom_init,
                Statu_init = initiative.Statu_init.Value,
                Date_debu = initiative.Date_debu.Value,
                Date_fin = initiative.Date_fin.Value,
                Objectifs_generaux = initiative.Objectifs_generaux,
                Obgectifs_specifiques = initiative.Obgectifs_specifiques,
                Description_court = initiative.Description_court,
                Description_detaillee = initiative.Description_detaillee,
                Budget = initiative.Budget.Value,
                Approbateur = initiative.Approbateur,
                Cofinancement = initiative.Cofinancement,
                Regions = initiative.Regions,
            };
            if (initiative == null)
            {
                return HttpNotFound();
            }
            ViewBag.UtilisateurID = new SelectList(db.Utilisateur, "ID", "UserId", initiative.UtilisateurID);
            return View(init);
        }

        // POST: Initiatives1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InitiativesViewModel initiative)
        {
            if (ModelState.IsValid)
            {
                try {
                    //initiative.Statu_init = initiative.Statu_init;
                    Initiatives init = new Initiatives()
                    {
                        ID=initiative.ID,
                        UtilisateurID = initiative.UtilisateurID,
                        Nom_init = initiative.Nom_init,
                        Statu_init = initiative.Statu_init,
                        Date_debu = initiative.Date_debu,
                        Date_fin = initiative.Date_fin,
                        Objectifs_generaux = initiative.Objectifs_generaux,
                        Obgectifs_specifiques = initiative.Obgectifs_specifiques,
                        Description_court = initiative.Description_court,
                        Description_detaillee = initiative.Description_detaillee,
                        Budget = initiative.Budget,
                        Approbateur = initiative.Approbateur,
                        Cofinancement = initiative.Cofinancement,
                        Regions = initiative.Regions,
                    };
                    db.Entry(init).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception DbExc)
                {
                    //DbEntityValidationException
                    //string error = "";
                    //foreach (var er in DbExc.EntityValidationErrors)
                    //{
                    //    foreach (var ve in er.ValidationErrors)
                    //    {
                    //        error += " - " + ve.ErrorMessage;
                    //    }
                    //}
                    TempData["Message"] = DbExc.Message;
                    ViewBag.UtilisateurID = new SelectList(db.Utilisateur, "ID", "UserId", initiative.UtilisateurID);
                    return View(initiative);
                }
            }
            ViewBag.UtilisateurID = new SelectList(db.Utilisateur, "ID", "UserId", initiative.UtilisateurID);
            return View(initiative);
        }
        // GET: Initiatives1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Initiatives initiative = db.Initiatives.Find(id);
            if (initiative == null)
            {
                return HttpNotFound();
            }
            return View(initiative);
        }

        // POST: Initiatives1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Initiatives initiative = db.Initiatives.Find(id);
            db.Initiatives.Remove(initiative);
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
