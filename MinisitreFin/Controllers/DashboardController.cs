using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
namespace MinisitreFin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private MinistreFinEntities db = new MinistreFinEntities();
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {

            var initiative = db.Initiatives.ToList().Count();
            var evenement = db.Evenements.ToList().Count();
            var activete = db.Activites.ToList().Count();
            var programme = db.Programmes.ToList().Count();
            ViewData["initiative"] = initiative;
            ViewData["evenement"] = evenement;
            ViewData["programme"] = programme;
            return View();
        }

    }
}