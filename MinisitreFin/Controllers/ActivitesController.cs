using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using MinisitreFin.Models;
using MinisitreFin.services;

namespace MinisitreFin.Controllers
{
    [RequireHttps]
    [Authorize]
    public class ActivitesController : Controller
    {

        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Espace MEF 2020";

        private static string FromAddress;
        private static string strSmtpClient;
        private static string UserID;
        private static string Password;
        private static string SMTPPort;
        private static bool bEnableSSL;

        private static void GetMailData()
        {

            FromAddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromAddress");
            strSmtpClient = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient");
            UserID = System.Configuration.ConfigurationManager.AppSettings.Get("UserID");
            Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            //ReplyTo = System.Configuration.ConfigurationManager.AppSettings.Get("ReplyTo");
            SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort");
            if ((System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL") == null))
            {
            }
            else
            {
                if ((System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() == "YES"))
                {
                    bEnableSSL = true;
                }
                else
                {
                    bEnableSSL = false;
                }
            }
        }
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
            try
            {
                var AgID = db.Agenda.FirstOrDefault(ag => ag.GroupId == idgroupe).ID;
                var activites = db.Activites.Where(a => a.AgendaID == AgID).Include(a => a.Agenda).ToList();
                ViewData["Agenda"] = db.Agenda.FirstOrDefault(a => a.ID == AgID).Nom_agenda;
                ViewData["idgroupe"] = idgroupe;
                ViewData["IDCreate"] = IDCreate;
                return View(activites);
            }
            catch (Exception)
            {
                TempData["Message"] = "aucune activité trouvée";
                return RedirectToAction("Index", "Groupe");
            }
            
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

        public  ActionResult CreateActivite( int? id, int? idgroupe,bool GAC)
        {
            if (GAC)
            {
                UserCredential credential;

                using (var stream =
                    new FileStream(Path.Combine(Server.MapPath("~/Credentials"), "credentials-MinFin.json"), FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = Path.Combine(Server.MapPath("~/Credentials"), "token" + id + ".json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                var calendars = service.CalendarList.List().Execute().Items;
                List<CalendarsModel> CML = new List<CalendarsModel>();
                foreach (CalendarListEntry entry in calendars)
                {
                    CalendarsModel CM = new CalendarsModel();
                    CM.ID = entry.Id;
                    CM.Name = entry.Summary;
                    CML.Add(CM);
                    ViewData["colorid"] += "[" + entry.BackgroundColor + " ]<br>";
                    //ViewData["calendarsList"] += entry.Summary + " ID: |" + entry.Id + "|<br>"; 
                }

                ViewBag.GoogleCalendarID = new SelectList(CML, "ID", "Name");
            }
            ViewData["GAC"] = GAC;
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type");
            ViewData["idagenda"] = id;
            ViewData["idgroupe"] = idgroupe;
            return View();
        }
        // POST: Activites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateActivite(EventsModel eventsModel,int? id,int? idgroupe, bool GAC)
        {
            Activites activites = new Activites();
            
                activites.Nom_activ = eventsModel.Title;
                activites.Objectif_activ = eventsModel.Description;
                activites.Type_ActiviteID = eventsModel.Type_ActiviteID;
                activites.Emplacement = eventsModel.Location;
                activites.AgendaID = eventsModel.AgendaId;
                activites.DateStart =eventsModel.DateStart;
                activites.DateEnd = eventsModel.DateEnd;
                activites.statu = false;
                db.Activites.Add(activites);
                try
                {
                    await db.SaveChangesAsync();
                }catch(DbEntityValidationException DbExc)
                {
                string error = "";
                foreach (var er in DbExc.EntityValidationErrors)
                {
                    foreach (var ve in er.ValidationErrors)
                    {
                        error += " - " + ve.ErrorMessage;
                    }
                }
                TempData["Message"] = error;
            }
                
                IList<EventAttendee> AttList = new List<EventAttendee>();

                var MemL = db.Membre_group.Where(mg => mg.GroupId == idgroupe);
                Membre_group membre_Group = new Membre_group();
                foreach (var item in MemL)
                {
                    EventAttendee eventAttendee = new EventAttendee();
                    eventAttendee.Email = item.Utilisateur.Email;
                    AttList.Add(eventAttendee);
                    //new EventAttendee() { Email = item.Utilisateur.Email };
                }
                ///// Email Send //////////////////////////////
                GetMailData();
                EMail mail = new EMail();
                dynamic MailMessage = new MailMessage();
                MailMessage.From = new MailAddress(FromAddress);
                foreach (var item in MemL)
                {
                    MailMessage.To.Add(item.Utilisateur.Email);
                }
                MailMessage.Subject = "Espace MEFRA Notification";
                MailMessage.IsBodyHtml = true;
                MailMessage.Body = "Nouveau Activité";


                SmtpClient SmtpClient = new SmtpClient();
                SmtpClient.Host = strSmtpClient;
                SmtpClient.EnableSsl = bEnableSSL;
                SmtpClient.Port = Int32.Parse(SMTPPort);
                SmtpClient.Credentials = new System.Net.NetworkCredential(UserID, Password);

                try
                {
                    try
                    {
                        SmtpClient.Send(MailMessage);

                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i <= ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.StatusCode;
                        if ((status == SmtpStatusCode.MailboxBusy) | (status == SmtpStatusCode.MailboxUnavailable))
                        {
                            System.Threading.Thread.Sleep(5000);
                            SmtpClient.Send(MailMessage);
                        }
                    }
                }


                if (GAC)
                {
                    UserCredential credential;
                    using (var stream =
                   new FileStream(Path.Combine(Server.MapPath("~/Credentials"), "credentials-MinFin.json"), FileMode.Open, FileAccess.Read))
                    {
                        // The file token.json stores the user's access and refresh tokens, and is created
                        // automatically when the authorization flow completes for the first time.
                        string credPath = Path.Combine(Server.MapPath("~/Credentials"), "token" + id + ".json");
                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.Load(stream).Secrets,
                            Scopes,
                            "user",
                            CancellationToken.None,
                            new FileDataStore(credPath, true)).Result;
                    }

                    // Create Google Calendar API service.
                    var service = new CalendarService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = ApplicationName,

                    });
                    // Define parameters of request.
                    EventsResource.ListRequest request = service.Events.List("primary");
                    request.TimeMin = DateTime.Now;
                    request.ShowDeleted = false;
                    request.SingleEvents = true;
                    request.MaxResults = 10;
                    request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;



                    
                    Event newEvent = new Event()
                    {
                        Summary = eventsModel.Title,
                        Location = eventsModel.Location,
                        Description = eventsModel.Description,
                        Start = new EventDateTime()
                        {
                            DateTime = DateTime.Parse(string.Format("{0:s}", eventsModel.DateStart)),
                            TimeZone = "Africa/Casablanca",
                        },
                        End = new EventDateTime()
                        {
                            DateTime = DateTime.Parse(string.Format("{0:s}", eventsModel.DateEnd)),
                            TimeZone = "Africa/Casablanca",
                        },
                        Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                        Attendees = AttList,
                        Reminders = new Event.RemindersData()
                        {
                            UseDefault = false,
                            Overrides = new EventReminder[] {
                new EventReminder() { Method = "email", Minutes = 24 * 60 },
                new EventReminder() { Method = "sms", Minutes = 10 },
                }
                        }
                    };

                    EventsResource.InsertRequest request2 = service.Events.Insert(newEvent, eventsModel.GoogleCalendarID);

                    request2.Execute();
                }
               
                ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type");
                ViewData["idagenda"] = id;
                ViewData["idgroupe"] = idgroupe;
                ViewData["GAC"] = GAC;
                return RedirectToAction("TestApi", "Agenda", new { id });
           
        }

        // GET: Activites/Edit/5
        public ActionResult Edit(int? id,int? idgroupe, string IDCreate)
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
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            ViewBag.AgendaID = new SelectList(db.Agenda, "ID", "Nom_agenda", activites.AgendaID);
            return View(activites);
        }

        // POST: Activites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Activites activites  ,int? idgroupe, string IDCreate)
        {
            if (ModelState.IsValid)
            {

                db.Entry(activites).State = EntityState.Modified;
                try
                {
                    
                    db.SaveChanges();
                }catch (DbEntityValidationException DbExc)
                {
                    string error = "";
                    foreach (var er in DbExc.EntityValidationErrors)
                    {
                        foreach (var ve in er.ValidationErrors)
                        {
                            error += " - " + ve.ErrorMessage;
                        }
                    }
                    TempData["Message"] = error;
                }

                ViewData["idgroupe"] = idgroupe;
                ViewData["IDCreate"] = IDCreate;
                return RedirectToAction("GroupeActivites", "Activites", new { idgroupe, IDCreate });
            }
            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type", activites.Type_ActiviteID);
            ViewBag.AgendaID = new SelectList(db.Agenda, "ID", "Nom_agenda", activites.AgendaID);
            return View(activites);
        }
        public ActionResult UpdateStatu(int id, int? idgroupe, string IDCreate)
        {
            try
            {
                Activites ac = db.Activites.Find(id);
                ac.statu = !ac.statu.Value;
                db.Activites.Attach(ac);
                db.Entry(ac).State = EntityState.Modified;
                db.SaveChanges();
                ViewData["idgroupe"] = idgroupe;
                ViewData["IDCreate"] = IDCreate;
            }
            catch (Exception)
            {

            }
            
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
