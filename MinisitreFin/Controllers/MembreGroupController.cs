using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MinisitreFin.Models;

namespace MinisitreFin.Controllers
{
    [Authorize]
    public class MembreGroupController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: MembreGroup
        public ActionResult Index()
        {
            var membre_group = db.Membre_group.Include(m => m.Groupe_thematiqe).Include(m => m.Utilisateur.ID);
            return View(membre_group.ToList());
        }

        // GET: MembreGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membre_group membre_group = db.Membre_group.Find(id);
            if (membre_group == null)
            {
                return HttpNotFound();
            }
            return View(membre_group);
        }
        // GET: MembreGroup/Create
        public ActionResult Create()
        {
            var currentId = User.Identity.GetUserId();
            var utilisateurs = db.Utilisateur.Include(u => u.AspNetUsers);
            var idAdmin = "4dcc3cda-6ee1-4a7c-9fd4-33b3a565ab80";
            
            ViewBag.MembreId = new SelectList(db.Utilisateur.Where(p => p.UserId != currentId), "ID", "Email");
            
            return View();
        }

        // POST: MembreGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        
        public ActionResult Create(Membre_group membre_group)
        {   
            string currentUser= User.Identity.GetUserId();
            if (db.Utilisateur.FirstOrDefault(p => p.UserId == currentUser) != null)
            {
                int IdeUser = db.Utilisateur.FirstOrDefault(p => p.UserId == currentUser).ID;
                if (db.Groupe_thematiqe.FirstOrDefault(p => p.CreatedById == IdeUser) != null)
                {
                    int currentGroupe = db.Groupe_thematiqe.FirstOrDefault(p => p.CreatedById == IdeUser).ID;
                    if (db.Utilisateur.FirstOrDefault(p => p.ID == membre_group.MembreId) != null)
                    {
                        int IdChildren = db.Utilisateur.FirstOrDefault(p => p.ID == membre_group.MembreId).ID;
                        membre_group.MembreId = IdChildren;
                        membre_group.GroupId = currentGroupe;

                        db.Membre_group.Add(membre_group);
                        db.SaveChanges();
                        return RedirectToAction("Consulte","Groupe", new { id=currentGroupe });
                    }
                }
            }
            //ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe", membre_group.GroupId);
            //ViewBag.MembreId = new SelectList(db.Utilisateur, "ID", "UserId", membre_group.MembreId);
            return View(membre_group);
        }
        [HttpPost]
        public ActionResult add(Membre_group membre_group)
        {
            var membreexi = db.Membre_group.FirstOrDefault(m => m.MembreId == membre_group.MembreId);
           
                
                db.Membre_group.Add(membre_group);
                db.SaveChanges();
            

            return RedirectToAction("Consulte", "Groupe",new {id= membre_group.GroupId });
        }
        // GET: MembreGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membre_group membre_group = db.Membre_group.Find(id);
            if (membre_group == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe", membre_group.GroupId);
            ViewBag.MembreId = new SelectList(db.Utilisateur, "ID", "UserId", membre_group.MembreId);
            return View(membre_group);
        }

        // POST: MembreGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupId,MembreId")] Membre_group membre_group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membre_group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe", membre_group.GroupId);
            ViewBag.MembreId = new SelectList(db.Utilisateur, "ID", "UserId", membre_group.MembreId);
            return View(membre_group);
        }

        // GET: MembreGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membre_group membre_group = db.Membre_group.Find(id);
            if (membre_group == null)
            {
                return HttpNotFound();
            }
            return View(membre_group);
        }

        // POST: MembreGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Membre_group membre_group = db.Membre_group.Find(id);
            db.Membre_group.Remove(membre_group);
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
