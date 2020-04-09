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
    [ValidateInput(false)]
    [Authorize]
    public class CompteRenduController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: CompteRendu
        public ActionResult Index()
        {
            var compte_rendu = db.compte_rendu.Include(c => c.Activites);
            return View(compte_rendu.ToList());
        }
        public ActionResult ActiviteCRS(int? idAct ,int idgroupe, string IDCreate)
        {
            var compte_rendu = db.compte_rendu.Where(c => c.Activites.ID== idAct);
            ViewData["idgroupe"] = idgroupe;
            ViewData["IDCreate"] = IDCreate;
            return View(compte_rendu.ToList());
        }
        // GET: CompteRendu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte_rendu compte_rendu = db.compte_rendu.Find(id);
            if (compte_rendu == null)
            {
                return HttpNotFound();
            }
            return View(compte_rendu);
        }
        public ActionResult DetailsCR(int? id,int? idgroupe)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte_rendu compte_rendu = db.compte_rendu.Find(id);
            if (compte_rendu == null)
            {
                return HttpNotFound();
            }
            ViewData["idgroupe"] = idgroupe;
            return View(compte_rendu);
        }

        // GET: CompteRendu/Create
        public ActionResult Create(int? id,int? idgroupe, string IDCreate)
        {
            ViewBag.ActivitesID = new SelectList(db.Activites.Where(a=>a.ID==id), "ID", "Nom_activ");
            ViewData["idActivite"] = id;
            ViewData["idgroupe"] = idgroupe;
            ViewData["IDCreate"] = IDCreate;
            return View();
        }

        // POST: CompteRendu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compte_rendu compte_rendu)
        {
            if (ModelState.IsValid)
            {
                compte_rendu.Statut = false;
                db.compte_rendu.Add(compte_rendu);
                db.SaveChanges();
                return RedirectToAction("index","Groupe",null);
            }

            ViewBag.ActivitesID = new SelectList(db.Activites, "ID", "Nom_activ", compte_rendu.ActivitesID);
            return View(compte_rendu);
        }

        // GET: CompteRendu/Edit/5
        public ActionResult Edit(int? id, int idgroupe,int? idAct)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte_rendu compte_rendu = db.compte_rendu.Find(id);
            if (compte_rendu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivitesID = new SelectList(db.Activites, "ID", "Nom_activ", compte_rendu.ActivitesID);
            ViewData["idgroupe"] = idgroupe;
            ViewData["idActivite"] = idAct;
            return View(compte_rendu);
        }

        // POST: CompteRendu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( compte_rendu compte_rendu, int? idgroupe, string IDCreate)
        {
            if (ModelState.IsValid)
            {
                compte_rendu.ActivitesID = compte_rendu.ActivitesID;
                compte_rendu.Statut = compte_rendu.Statut;
                db.Entry(compte_rendu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GroupeActivites", "Activites", new { idgroupe, IDCreate });
            }
            ViewBag.ActivitesID = new SelectList(db.Activites, "ID", "Nom_activ", compte_rendu.ActivitesID);
            return View(compte_rendu);
        }
        public ActionResult UpdateStatu(int id,int? idAct, int? idgroupe, string IDCreate)
        {
            compte_rendu cr = db.compte_rendu.Find(id);
            cr.Statut = !cr.Statut.Value;
            db.compte_rendu.Attach(cr);
            db.Entry(cr).State = EntityState.Modified;
            db.SaveChanges();
            ViewData["idgroupe"] = idgroupe;
            ViewData["IDCreate"] = IDCreate;
            return RedirectToAction("ActiviteCRS", "CompteRendu", new { idAct ,idgroupe, IDCreate });
        }
        // GET: CompteRendu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte_rendu compte_rendu = db.compte_rendu.Find(id);
            if (compte_rendu == null)
            {
                return HttpNotFound();
            }
            return View(compte_rendu);
        }

        // POST: CompteRendu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            compte_rendu compte_rendu = db.compte_rendu.Find(id);
            db.compte_rendu.Remove(compte_rendu);
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
