﻿using MinisitreFin.Models;
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
    [ValidateInput(false)]
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
        public ActionResult Create([Bind(Include = "ID,UtilisateurID,Nom_init,Statu_init,Date_debu,Date_fin,Objectifs_generaux,Obgectifs_specifiques,Description_court,Description_detaillee,Budget,Approbateur,Cofinancement,Regions")] Initiatives initiative)
        {
            if (ModelState.IsValid)
            {
                try {
                    initiative.Statu_init = false;
                    db.Initiatives.Add(initiative);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }catch (DbEntityValidationException DbExc)
                {
                    string error = "";
                    foreach (var er in DbExc.EntityValidationErrors)
                    {
                        foreach (var ve in er.ValidationErrors)
                        {
                            error += " - " + ve.ErrorMessage;
                        }
                    }
                    TempData["Message"] = error;
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
            if (initiative == null)
            {
                return HttpNotFound();
            }
            ViewBag.UtilisateurID = new SelectList(db.Utilisateur, "ID", "UserId", initiative.UtilisateurID);
            return View(initiative);
        }

        // POST: Initiatives1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UtilisateurID,Nom_init,Statu_init,Date_debu,Date_fin,Objectifs_generaux,Obgectifs_specifiques,Description_court,Description_detaillee,Budget,Approbateur,Cofinancement,Regions")] Initiatives initiative)
        {
            if (ModelState.IsValid)
            {
                try {
                    initiative.Statu_init = initiative.Statu_init;
                    db.Entry(initiative).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException DbExc)
                {
                    string error = "";
                    foreach (var er in DbExc.EntityValidationErrors)
                    {
                        foreach (var ve in er.ValidationErrors)
                        {
                            error += " - " + ve.ErrorMessage;
                        }
                    }
                    TempData["Message"] = error;
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
