using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisitreFin.Models;
using MinisitreFin.ViewModels;

namespace MinisitreFin.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class ArticlesController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: Articles
        public ActionResult Index()
        {
            
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
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

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticlesViewModel articlesViewModel,HttpPostedFileBase Image)
        {
            
            if (ModelState.IsValid)
            {
                try{
                    if (Image != null)
                    {
                        var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                        Image.SaveAs(path);
                        articlesViewModel.Image = Image.FileName;
                    }
                    else
                    {
                        articlesViewModel.Image = "logo-MF.jpg";
                    }


                    articlesViewModel.statu = false;
                    Articles articles = new Articles()
                    {
                        Titre_article = articlesViewModel.Titre_article,
                        Description = articlesViewModel.Description,
                        Contenu_article = articlesViewModel.Contenu_article,
                        Image = articlesViewModel.Image,
                        Url_video = articlesViewModel.Url_video,
                        Date_creation = articlesViewModel.Date_creation,
                        statu = articlesViewModel.statu
                    };

                    db.Articles.Add(articles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception DbExc)
                {
                    //DbEntityValidationException
                    //string error = "";
                    //foreach (var er in DbExc.EntityValidationErrors)
                    //{
                    //    foreach (var ve in er.ValidationErrors)
                    //    {
                    //        error += " - " + ve.ErrorMessage;
                    //    }
                    //}
                    TempData["Message"] = DbExc.Message;
                    return View(articlesViewModel);
                }
            }
            return View(articlesViewModel);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
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
            ArticlesViewModel articlesviewmodel = new ArticlesViewModel()
            {
                ID = articles.ID,
                Titre_article = articles.Titre_article,
                Description = articles.Description,
                Contenu_article = articles.Contenu_article,
                Image = articles.Image,
                Url_video = articles.Url_video,
                Date_creation = articles.Date_creation.Value,
                statu = articles.statu
            };
            return View(articlesviewmodel);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticlesViewModel articlesviewmodel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                try {
                    string oldPath = Path.Combine(Server.MapPath("~/AppImg"), articlesviewmodel.Image);
                    if (Image != null)
                    {
                        System.IO.File.Delete(oldPath);
                        var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                        Image.SaveAs(path);
                        articlesviewmodel.Image = Image.FileName;
                    }

                    articlesviewmodel.statu = articlesviewmodel.statu;

                    Articles articles = new Articles()
                    {
                        ID = articlesviewmodel.ID,
                        Titre_article = articlesviewmodel.Titre_article,
                        Description = articlesviewmodel.Description,
                        Contenu_article = articlesviewmodel.Contenu_article,
                        Image = articlesviewmodel.Image,
                        Url_video = articlesviewmodel.Url_video,
                        Date_creation = articlesviewmodel.Date_creation,
                        statu = articlesviewmodel.statu
                    };

                    db.Entry(articles).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception DbExc)
                {
                    //string error = "";
                    //foreach (var er in DbExc.EntityValidationErrors)
                    //{
                    //    foreach (var ve in er.ValidationErrors)
                    //    {
                    //        error += " - " + ve.ErrorMessage;
                    //    }
                    //}
                    TempData["Message"] = DbExc.Message;
                    return View(articlesviewmodel);
                }

            }
            return View(articlesviewmodel);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
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

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Articles articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateStatu(int id)
        {
            try
            {
                Articles ar = db.Articles.Find(id);
                ar.statu = !ar.statu;
                db.Articles.Attach(ar);
                db.Entry(ar).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception) { }
          

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
