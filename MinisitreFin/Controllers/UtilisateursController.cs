using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MinisitreFin.Models;

namespace MinisitreFin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UtilisateursController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        
        // GET: Utilisateurs
        public ActionResult Index()
        {
            var currentId = User.Identity.GetUserId();
            var utilisateurs = db.Utilisateur.Include(u => u.AspNetUsers);

            var x = db.Utilisateur.Where(p => p.UserId != currentId);
            return View(x.ToList());
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
        public ActionResult AddCM()
        {
            return View();
        }
        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,Nom,Prenom,Email,Telephone,Statu")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Utilisateur.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", utilisateur.UserId);
            return View(utilisateur);
        }
        
        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", utilisateur.UserId);
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(utilisateur).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", utilisateur.UserId);
            return View(utilisateur);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult UpdateStatu(int id)
        {
            Utilisateur ut = db.Utilisateur.Find(id);
            ut.Statu = !ut.Statu.Value;
            db.Utilisateur.Attach(ut);
            db.Entry(ut).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateur.Find(id);
            db.Utilisateur.Remove(utilisateur);
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
