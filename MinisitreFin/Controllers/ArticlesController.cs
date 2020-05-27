using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisitreFin.Models;

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
        public ActionResult Create(Articles articles,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
               if(Image != null)
                {
                    var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                    Image.SaveAs(path);
                    articles.Image = Image.FileName;
                }
                else
                {
                    articles.Image = "logo-MF.jpg";
                }
                
                
                articles.statu = false;
                db.Articles.Add(articles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articles);
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
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Articles articles, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
              
              
                string oldPath = Path.Combine(Server.MapPath("~/AppImg"), articles.Image);
                if (Image != null)
                {
                    System.IO.File.Delete(oldPath);
                    var path = Path.Combine(Server.MapPath("~/AppImg"), Image.FileName);
                    Image.SaveAs(path);
                    articles.Image = Image.FileName;
                }

                
                articles.statu = articles.statu;
                db.Entry(articles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articles);
        }

        // GET: Articles/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Articles articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateStatu(int id)
        {
            Articles ar = db.Articles.Find(id);
            ar.statu = !ar.statu;
            db.Articles.Attach(ar);
            db.Entry(ar).State = EntityState.Modified;
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
