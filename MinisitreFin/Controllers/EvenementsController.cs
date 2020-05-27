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

namespace MinisitreFin.Controllers
{
    [ValidateInput(false)]
    [Authorize]
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
        public ActionResult Create(Evenements evenement1,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                    Image.SaveAs(path);
                    evenement1.Image = Image.FileName;
                }
                else
                {
                    evenement1.Image = "logo-MF.jpg";
                }
                
                evenement1.Statut = false;
                db.Evenements.Add(evenement1);
                db.SaveChanges();
                //file.SaveAs(path);
                return RedirectToAction("Index");
            }

            return View(evenement1);
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
            return View(evenement1);
        }

        // POST: Evenements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Evenements evenement1, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string oldPath =Path.Combine(Server.MapPath("~/AppImg"), evenement1.Image);
                if (Image != null)
                {
                    System.IO.File.Delete(oldPath);
                    var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                    Image.SaveAs(path);
                    evenement1.Image = Image.FileName;
                }
                
                evenement1.Statut = evenement1.Statut;
                db.Entry(evenement1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evenement1);
        }
        [HttpPost]
        public ActionResult Createimg()
        {
            Evenements eve = new Evenements();
            
            return View();
        }
        // GET: Evenements/Delete/5
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
