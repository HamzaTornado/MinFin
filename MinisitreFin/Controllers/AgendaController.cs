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
    [Authorize]
    public class AgendaController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        public ActionResult TestApi(int? id,int? idgroupe)
        {
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type");
            ViewData["idagenda"] = id;
            ViewData["idgroupe"] = idgroupe;
            return View();
        }
        public ActionResult TestApi2(int? id)
        {
            ViewData["idagenda"] = id;
            return View();
        }
        public ActionResult AllEvents(int id)
        {

            return Json(db.Activites.Where(ac=>ac.AgendaID == id).AsEnumerable().Select(a => new {
                id = a.ID,
                title = a.Nom_activ + "- Objectif :"+ a.Objectif_activ,
                start = a.Date.Value.Date.ToString("yyyy-MM-dd"),
                

            }).ToList(),JsonRequestBehavior.AllowGet);
        }

        // GET: Agenda
        public ActionResult Index()
        {
            var agenda = db.Agenda.Include(a => a.Groupe_thematiqe);
            return View(agenda.ToList());
        }

        // GET: Agenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe");
            return View();
        }
        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAgenda([Bind(Include = "ID,GroupId,Nom_agenda,Date_creation")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                agenda.Date_creation = DateTime.Now;
                db.Agenda.Add(agenda);
                db.SaveChanges();
                return RedirectToAction("Consulte", "Groupe", new { id = agenda.GroupId });
            }

            ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe", agenda.GroupId);
            return View(agenda);
        }
        // GET: Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe", agenda.GroupId);
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                agenda.Date_creation = agenda.Date_creation;
                agenda.GroupId = agenda.GroupId;
                
                db.Entry(agenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Groupe",null);
            }
            ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe", agenda.GroupId);
            return View(agenda);
        }

        // GET: Agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agenda agenda = db.Agenda.Find(id);
            db.Agenda.Remove(agenda);
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
