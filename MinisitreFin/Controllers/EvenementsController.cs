using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MinisitreFin.Models;
using MinisitreFin.ViewModels;

namespace MinisitreFin.Controllers
{
    [ValidateInput(false)]
    [Authorize(Roles = "Admin,CM")]
    public class EvenementsController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: Evenements
        public ActionResult Index()
        {
            return View(db.Evenements.ToList());
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenements evenement1 = db.Evenements.Find(id);
            if (evenement1 == null)
            {
                return HttpNotFound();
            }
            return View(evenement1);
        }

        // GET: Evenements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evenements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvenementsViewModel evenementViewModel,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null)
                    {
                        var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                        Image.SaveAs(path);
                        evenementViewModel.Image = Image.FileName;
                    }
                    else
                    {
                        evenementViewModel.Image = "logo-MF.jpg";
                    }
                    evenementViewModel.Statut = false;
                    Evenements evenement = new Evenements()
                    {
                        Titre_even = evenementViewModel.Titre_even,
                        Description = evenementViewModel.Description,
                        Image = evenementViewModel.Image,
                        Date_debut = evenementViewModel.Date_debut,
                        Date_fin = evenementViewModel.Date_fin,
                        Statut = evenementViewModel.Statut
                    };

                    db.Evenements.Add(evenement);
                    db.SaveChanges();
                }
                catch (Exception DbExc)
                {
                    TempData["Message"] = DbExc.Message;
                    return View(evenementViewModel);
                }
                //file.SaveAs(path);
                return RedirectToAction("Index");
            }

            return View(evenementViewModel);
        }

        // GET: Evenements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenements evenement1 = db.Evenements.Find(id);
            if (evenement1 == null)
            {
                return HttpNotFound();
            }
            EvenementsViewModel evenementViewModel = new EvenementsViewModel()
            {
                ID = evenement1.ID,
                Titre_even = evenement1.Titre_even,
                Description = evenement1.Description,
                Image = evenement1.Image,
                Date_debut = evenement1.Date_debut.Value,
                Date_fin = evenement1.Date_fin.Value,
                Statut = evenement1.Statut.Value
            };
            
            return View(evenementViewModel);
        }

        // POST: Evenements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvenementsViewModel evenementViewModel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string oldPath = Path.Combine(Server.MapPath("~/AppImg"), evenementViewModel.Image);
                    if (Image != null)
                    {
                        System.IO.File.Delete(oldPath);
                        var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                        Image.SaveAs(path);
                        evenementViewModel.Image = Image.FileName;
                    }

                    evenementViewModel.Statut = evenementViewModel.Statut;
                    Evenements evenement = new Evenements()
                    {
                        ID = evenementViewModel.ID,
                        Titre_even = evenementViewModel.Titre_even,
                        Description = evenementViewModel.Description,
                        Image = evenementViewModel.Image,
                        Date_debut = evenementViewModel.Date_debut,
                        Date_fin = evenementViewModel.Date_fin,
                        Statut = evenementViewModel.Statut
                    };
                    db.Entry(evenement).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception error)
                {
                    TempData["Message"] = error.Message;
                    return View(evenementViewModel);
                }
                return RedirectToAction("Index");
            }
            return View(evenementViewModel);
        }

        //[HttpPost]
        //public ActionResult Createimg()
        //{
        //    Evenements eve = new Evenements();
            
        //    return View();
        //}
        // GET: Evenements/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenements evenement1 = db.Evenements.Find(id);
            if (evenement1 == null)
            {
                return HttpNotFound();
            }
            return View(evenement1);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateStatu(int id)
        {
            Evenements ev = db.Evenements.Find(id);
            ev.Statut = !ev.Statut.Value;
            db.Evenements.Attach(ev);
            db.Entry(ev).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Evenements evenement1 = db.Evenements.Find(id);
            db.Evenements.Remove(evenement1);
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
