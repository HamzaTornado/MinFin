using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MinisitreFin.Models;

namespace MinisitreFin.Controllers
{
   
    [Authorize]
    public class AgendaController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Espace MEF 2020";
        
        public ActionResult TestApi(int? id,int? idgroupe)
        {

            //UserCredential credential;
            //using (var stream =
            //    new FileStream(Path.Combine(Server.MapPath("~/Credentials"), "credentials-MinFin.json"), FileMode.Open, FileAccess.Read))
            //{
            //    // The file token.json stores the user's access and refresh tokens, and is created
            //    // automatically when the authorization flow completes for the first time.
            //    string credPath = Path.Combine(Server.MapPath("~/Credentials"), "token" + id + ".json");
            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        Scopes,
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore(credPath, true)).Result;

            //}

            //// Create Google Calendar API service.
            //var service = new CalendarService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = ApplicationName,
            //});

            //// Define parameters of request.
            ////EventsResource.ListRequest request = service.Events.List("primary");
            ////request.TimeMin = DateTime.Now;
            ////request.ShowDeleted = false;
            ////request.SingleEvents = true;
            ////request.MaxResults = 10;
            ////request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            //// List events.
            ////Events events = request.Execute();

            //var calendars = service.CalendarList.List().Execute().Items;
            //List<CalendarsModel> CML = new List<CalendarsModel>();
            //foreach (CalendarListEntry entry in calendars)
            //{
            //    CalendarsModel CM = new CalendarsModel();
            //    CM.ID = entry.Id;
            //    CM.Name = entry.Summary;
            //    CML.Add(CM);
            //    ViewData["colorid"] += "[" + entry.BackgroundColor + " ]<br>";
            //    //ViewData["calendarsList"] += entry.Summary + " ID: |" + entry.Id + "|<br>"; 
            //}

            //ViewBag.CalendarID = new SelectList(CML, "ID", "Name");
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
                start = a.DateStart.Value.Date.ToString("yyyy-MM-dd"),
                end = a.DateEnd.Value.Date.ToString("yyyy-MM-dd"),
            }).ToList(),JsonRequestBehavior.AllowGet);
        }

        // GET: Agenda
        [Authorize(Roles = "Admin")]
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
            try
            {
                if (ModelState.IsValid)
                {
                    agenda.Date_creation = DateTime.Now;
                    db.Agenda.Add(agenda);
                    db.SaveChanges();
                    return RedirectToAction("Consulte", "Groupe", new { id = agenda.GroupId });
                }
            }catch (DbEntityValidationException DbExc)
            {
                string error = "";
                foreach (var er in DbExc.EntityValidationErrors)
                {
                    foreach (var ve in er.ValidationErrors)
                    {
                        error += " - " + ve.ErrorMessage+"\n";
                    }
                }
                TempData["Message"] = error;
                return View(agenda);
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
            try
            {
                if (ModelState.IsValid)
                {
                    agenda.Date_creation = agenda.Date_creation;
                    agenda.GroupId = agenda.GroupId;

                    db.Entry(agenda).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Groupe", null);
                }
                ViewBag.GroupId = new SelectList(db.Groupe_thematiqe, "ID", "Nom_groupe", agenda.GroupId);
            }
            catch (DbEntityValidationException DbExc)
            {
                string error = "";
                foreach (var er in DbExc.EntityValidationErrors)
                {
                    foreach (var ve in er.ValidationErrors)
                    {
                        error += " - " + ve.ErrorMessage + "\n";
                    }
                }
                TempData["Message"] = error;
                return View(agenda);
            }

            return View(agenda);
        }

        // GET: Agenda/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
