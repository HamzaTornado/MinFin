using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
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
            //// Send Password in Gmail/////////// 
            string recipient = ut.Email;
            string body="";
            string subject = "MEF Espace";
            if (ut.Statu.Value)
            {
                body = "Bonjour,<br>Merci pour l'intérêt que vous témoignez envers l'espace MEF Maroc.<br> <strong> Votre Compte Est Activé</strong>.Vous pourrez accéder à votre espace en tant que Bailleurs de Fonds .Pour cela, vous devrez utiliser votre email : " + ut.Email + " comme login Votre compte vous donne accès aux fonctionnalités réservées aux participants à  l'espace MEF<br> Vous pourrez à tout moment modifier votre mot de passe  à partir de votre espace personnel.<br> Pour tout besoin,<br> vous pouvez nous contacter via l'email suivant: MEF@contact.com";
            }
            else
            {
                body = "Votre Compte est Désactivé";
            }
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
 
            WebMail.SmtpUseDefaultCredentials = false;
            WebMail.EnableSsl = true;
            WebMail.UserName = "meftestmail@gmail.com";
            WebMail.Password = "MinFin1234";
            WebMail.Send(to: recipient, subject: subject, body: body, isBodyHtml: true);
            ///////////////////////////////
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
