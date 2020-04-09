using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MinisitreFin.Models;

namespace MinisitreFin.Controllers
{
    [Authorize]
    public class ActivitesController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: Activites
        public ActionResult Index()
        {
            var activites = db.Activites.Include(a => a.Type_Activite).Include(a => a.Agenda);
            return View(activites.ToList());
        }
        public ActionResult AgendaActivites(int AgID)
        {

            var activites = db.Activites.Where(a => a.AgendaID == AgID).Include(a=>a.Agenda).ToList();
            ViewData["Agenda"] = db.Agenda.FirstOrDefault(a => a.ID == AgID).Nom_agenda;
            return View(activites);
        }
        public ActionResult GroupeActivites(int? idgroupe,string IDCreate )
        {
            var AgID = db.Agenda.FirstOrDefault(ag => ag.GroupId == idgroupe).ID;
            var activites = db.Activites.Where(a => a.AgendaID == AgID).Include(a => a.Agenda).ToList();
            ViewData["Agenda"] = db.Agenda.FirstOrDefault(a => a.ID == AgID).Nom_agenda;
            ViewData["idgroupe"] = idgroupe;
            ViewData["IDCreate"] = IDCreate;
            return View(activites);
        }
        // GET: Activites/Details/5
        public ActionResult Details(int? id, int? idgroupe, string IDCreate)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activites activites = db.Activites.Find(id);
            if (activites == null)
            {
                return HttpNotFound();
            }
            ViewData["idgroupe"] = idgroupe;
            ViewData["IDCreate"] = IDCreate;
            return View(activites);
        }

        // GET: Activites/Create
        public ActionResult Create()
        {
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type");
            ViewBag.AgendaID = new SelectList(db.Agenda, "ID", "Nom_agenda");
            
            return View();
        }

        // POST: Activites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActivite(Activites activites,int? id,int? idgroupe )
        {
            if (ModelState.IsValid)
            {
                activites.statu = false;
                
                db.Activites.Add(activites);
                db.SaveChanges();
                //// Send Password in Gmail/////////// 
                ///
                //var members  = db.Membre_group.Where(mg=>mg.GroupId==idgroupe);
                //foreach (var mem in members)
                //{
                //    string recipient = mem.Utilisateur.Email;
                //    string subject = "MEF Espace Nouvelle Activite";
                //    string body = "Nouvelle Activite Danse Le groupe";
                //    WebMail.SmtpServer = "smtp.gmail.com";
                //    WebMail.SmtpPort = 587;
                //    WebMail.SmtpUseDefaultCredentials = false;
                //    WebMail.EnableSsl = true;
                //    WebMail.UserName = "meftestmail@gmail.com";
                //    WebMail.Password = "MinFin1234";
                //    WebMail.Send(to: recipient, subject: subject, body: body, isBodyHtml: true);
                //}
                
                ///////////////////////////////
                return RedirectToAction("TestApi", "Agenda", new { id });
            }

            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            ViewBag.AgendaID = new SelectList(db.Agenda, "ID", "Nom_agenda", activites.AgendaID);
            return View(activites);
        }

        // GET: Activites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activites activites = db.Activites.Find(id);
            if (activites == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            ViewBag.AgendaID = new SelectList(db.Agenda, "ID", "Nom_agenda", activites.AgendaID);
            return View(activites);
        }

        // POST: Activites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type_ActiviteID,Nom_activ,Objectif_activ,Date,statu,AgendaID")] Activites activites)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activites).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            ViewBag.AgendaID = new SelectList(db.Agenda, "ID", "Nom_agenda", activites.AgendaID);
            return View(activites);
        }
        public ActionResult UpdateStatu(int id, int? idgroupe, string IDCreate)
        {
            Activites ac = db.Activites.Find(id);
            ac.statu = !ac.statu.Value;
            db.Activites.Attach(ac);
            db.Entry(ac).State = EntityState.Modified;
            db.SaveChanges();
            ViewData["idgroupe"] = idgroupe;
            ViewData["IDCreate"] = IDCreate;
            return RedirectToAction("GroupeActivites", "Activites",new { idgroupe, IDCreate });
        }
        // GET: Activites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activites activites = db.Activites.Find(id);
            if (activites == null)
            {
                return HttpNotFound();
            }
            return View(activites);
        }

        // POST: Activites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activites activites = db.Activites.Find(id);
            db.Activites.Remove(activites);
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
