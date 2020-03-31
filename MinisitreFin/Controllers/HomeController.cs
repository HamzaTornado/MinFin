using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisitreFin.Models;
using PagedList.Mvc;
using PagedList;

namespace MinisitreFin.Controllers
{
    public class HomeController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        public ActionResult Index()
        {
            var article = db.Articles.Where(a => a.statu == true);
            return View(article.ToList());
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ArticleList()
        {
            var activites = db.Activites.Include(a => a.Type_Activite);
            return View(activites.ToList());
            
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }
        public ActionResult EvenementsPublic(string chercher, int? page)
        {
            if (chercher != null)
            {
                var evenements = db.Evenements.Where(e => e.Statut == true&&e.Titre_even.Contains(chercher));
                return View(evenements.ToList().ToPagedList(page ?? 1, 3));
            }
            else {
                var evenements = db.Evenements.Where(e => e.Statut == true);
                return View(evenements.ToList().ToPagedList(page ?? 1, 3));
            }
            

        }

        //public ActionResult chercherEvenements(string chercher, int? page)
        //{

        //    var evenements = db.Evenements.Where(e => e.Statut == true && e.Titre_even.Contains(chercher));
            
        //    return RedirectToAction("chercherEvenements","Index", evenements.ToList().ToPagedList(page ?? 1, 3));
        //}
        public ActionResult DetailsEvent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenements evenements = db.Evenements.Find(id);
            if (evenements == null)
            {
                return HttpNotFound();
            }
            return View(evenements);
        }
        public ActionResult ArticlesPublic(int? page)
        {

            var articles = db.Articles.Where(a => a.statu == true);
            return View(articles.ToList().ToPagedList(page ?? 1, 3));
        }
        public ActionResult DetailsArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }
    }
}