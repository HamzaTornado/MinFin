using System;
using System.Collections.Generic;
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
    public class DashboardController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        
        // GET: Dashboard
        
        public ActionResult Index()
        {
            var currentId = User.Identity.GetUserId();
            var x = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId);
            if (x.Statu==false)
            {
                Session.Clear();
                Session[currentId] = null;
                Session.Abandon();
                ViewBag.MessageAC = "Vous n'avez pas encore activé";
                return RedirectToAction("Login", "Account"); 
            }
            else
            {
                var initiative = db.Initiatives.ToList().Count();
                var evenement = db.Evenements.ToList().Count();
                var projet = db.Projet.ToList().Count();
                ViewData["initiative"] = initiative;
                ViewData["evenement"] = evenement;
                ViewData["projet"] = projet;
                return View();
                
            }
        }

    }
}