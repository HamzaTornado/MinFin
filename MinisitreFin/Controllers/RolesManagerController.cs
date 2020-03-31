using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisitreFin.Models;

namespace MinisitreFin.Controllers
{
    public class RolesManagerController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: RolesManager
        public ActionResult Index()
        {
            var aspNetUserRoles = db.AspNetUserRoles.Include(a => a.AspNetRoles).Include(a => a.AspNetUsers).Where(a=>a.RoleId !="1" ).Distinct();
            return View(aspNetUserRoles.ToList());
        }

        // GET: RolesManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRoles);
        }

        // GET: RolesManager/Create
        public ActionResult Create()
        {
            var listroles = db.AspNetRoles.Where(r => r.Name != "Admin");
            ViewBag.RoleId = new SelectList(listroles, "Id", "Name");
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: RolesManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUserRoles.Add(aspNetUserRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRoles.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // GET: RolesManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRoles.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // POST: RolesManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRoles.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // GET: RolesManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRoles);
        }

        // POST: RolesManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id);
            db.AspNetUserRoles.Remove(aspNetUserRoles);
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
