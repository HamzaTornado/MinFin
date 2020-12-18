using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinisitreFin.Models;
using MinisitreFin.ViewModels;

namespace MinisitreFin.Controllers
{
    [Authorize(Roles = "Admin,CM")]
    public class GalerieController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        // GET: Galerie
        public ActionResult Index()
        {
            
            return View(db.Galerie.ToList());
        }

        // GET: Galerie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Galerie/Create
        public ActionResult AddMultipleImages()
        {
            return View();
        }

        // POST: Galerie/Create
        [HttpPost]
        public ActionResult AddMultipleImages(HttpPostedFileBase[] files)
        {
            MultiImages d = new MultiImages();
            string nonvalid = "";
            string extension = "";
            int countf = 0;
            try
            {
                    Galerie galerie = new Galerie();
                    foreach (HttpPostedFileBase file in files)
                    {
                        extension = "";
                        extension = Path.GetExtension(file.FileName);
                        
                        string[] exten = {".jpg", ".jpeg", ".bmp", ".gif", ".png" };
                        //Checking file is available to save.  
                        if (file != null && Array.Exists(exten, e => e == extension) )
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Img"), file.FileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            galerie.NomImage = file.FileName;
                            galerie.Alt = file.FileName+" " + file.ContentType;

                        ViewBag.UploadStatus = (files.Count() - countf).ToString() + " files uploaded successfully.";
                        //assigning file uploaded status to ViewBag for showing message to user.   
                    }
                        else
                        {
                            countf += 1;
                            nonvalid = extension + " " + nonvalid;
                            TempData["UploadErrors"] = nonvalid+ "Extension non autorisé";
                        d.files = files;
                        return View(d);
                    }
                    ViewBag.UploadStatus = (files.Count() - countf).ToString() + " files uploaded successfully.";
                    db.Galerie.Add(galerie);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            catch(Exception e)
            {
                ViewBag.ServerError = e.Message;
                d.files = files;
                return View(d);
            }
            
        }

        // GET: Galerie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Galerie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Galerie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Galerie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
