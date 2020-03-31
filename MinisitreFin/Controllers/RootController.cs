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
    public class RootController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        // GET: Root
        public ActionResult Index()
        {
            var activites = db.Activites.Include(a => a.Type_Activite);
            return View(activites.ToList());
           
        }
    }
}