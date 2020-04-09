using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MinisitreFin.Models;

namespace MinisitreFin.Controllers
{
    [Authorize]
    public class GroupeController : Controller
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
        // GET: Groupe
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                //return View(db.Groupe_thematiqe.ToList());
                if (db.Groupe_thematiqe.ToList().Count() > 0)
                {

                    //return View();
                    var gt = db.Groupe_thematiqe.ToList();
                    GroupeIndex GI = new GroupeIndex();

                    List<GroupeModel> ngtL = new List<GroupeModel>();
                    foreach (var a in gt)
                    {
                        GroupeModel ngt = new GroupeModel();
                        ngt.ID = a.ID;
                        ngt.Nom_groupe = a.Nom_groupe;
                        ngt.CreatedById = a.CreatedById;
                        ngt.Date_createion = a.Date_createion;
                        ngt.CreatedByIdString = db.Utilisateur.FirstOrDefault(s => s.ID == a.CreatedById).UserId;
                        ngt.Statut = a.Statut;
                        ngt.ActCount = db.Activites.Where(ac => ac.Agenda.GroupId == a.ID && ac.Date > DateTime.Now).ToList().Count();
                            //a.Agenda.FirstOrDefault(p => p.GroupId == a.ID).Activites.Where(p => p.Date > DateTime.Now).ToList().Count();
                        ngtL.Add(ngt);
                    }
                    GI.GML = ngtL;
                    return View(GI);
                }
            }
            else if (User.IsInRole("BDF"))
            {
                var currentId = User.Identity.GetUserId();
                //Connected User
                //int IdeUser = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId).ID;// Définir Utilisateur                                          
                //int currentGroupe = db.Groupe_thematiqe.FirstOrDefault(p => p.CreatedById == IdeUser).ID;//Définir le groupe pour l'ajoute
                //int nbrmembre = db.Membre_group.Where(mg => mg.GroupId == currentGroupe).Count();
                var x = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId);
                var members = db.Membre_group.FirstOrDefault(m => m.MembreId == x.ID);
                if (db.Groupe_thematiqe.Where(p => p.CreatedById == x.ID).Count() > 0)
                {
                    
                    //return View();
                    var gt = db.Groupe_thematiqe.Where(p => p.CreatedById == x.ID).ToList();
                    GroupeIndex GI = new GroupeIndex();

                    List<GroupeModel> ngtL = new List<GroupeModel>();
                    foreach (var a in gt)
                    {
                        GroupeModel ngt = new GroupeModel();
                        ngt.ID = a.ID;
                        ngt.Nom_groupe = a.Nom_groupe;
                        ngt.CreatedById = a.CreatedById;
                        ngt.Date_createion = a.Date_createion;
                        ngt.CreatedByIdString = db.Utilisateur.FirstOrDefault(s => s.ID == a.CreatedById).UserId;
                        ngt.Statut = a.Statut;
                        ngt.ActCount =db.Activites.Where(ac => ac.Agenda.GroupId == a.ID && ac.Date > DateTime.Now).ToList().Count();
                        ngtL.Add(ngt);
                    }
                    GI.GML = ngtL;
                    return View(GI);
                }
                
                //else if(members!=null)
                //{
                //    return View(db.Groupe_thematiqe.Where(p => p.ID == members.GroupId).ToList());
                //}
            }
            return View();
        }
        public ActionResult InviteGroupe()
        {
            if (User.IsInRole("Admin"))
            {
                return View(db.Groupe_thematiqe.ToList());
            }
            else if (User.IsInRole("BDF"))
            {
                var currentId = User.Identity.GetUserId();
                //Connected User
                //int IdeUser = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId).ID;// Définir Utilisateur                                          
                //int currentGroupe = db.Groupe_thematiqe.FirstOrDefault(p => p.CreatedById == IdeUser).ID;//Définir le groupe pour l'ajoute
                //int nbrmembre = db.Membre_group.Where(mg => mg.GroupId == currentGroupe).Count();
                var x = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId);
                var members = db.Membre_group.FirstOrDefault(m => m.MembreId == x.ID);
                if (members != null)
                {
                    var gt = db.Groupe_thematiqe.Where(p => p.ID == members.GroupId);
                    GroupeIndex GI = new GroupeIndex();

                    List<GroupeModel> ngtL = new List<GroupeModel>();
                    foreach (var a in gt)
                    {
                        GroupeModel ngt = new GroupeModel();
                        ngt.ID = a.ID;
                        ngt.Nom_groupe = a.Nom_groupe;
                        ngt.CreatedById = a.CreatedById;
                        ngt.Date_createion = a.Date_createion;
                        ngt.CreatedByIdString = db.Utilisateur.FirstOrDefault(s => s.ID == a.CreatedById).UserId;
                        ngt.Statut = a.Statut;
                        ngt.ActCount = db.Activites.Where(ac => ac.Agenda.GroupId == a.ID && ac.Date > DateTime.Now).ToList().Count();
                        //db.Activites.Where(ac => ac.Agenda.GroupId == a.ID&& ac.Date > DateTime.Now).Count();
                        ngtL.Add(ngt);
                    }
                    GI.GML = ngtL;
  
                    return View(GI);
                }
            }
            return View();
        }
        public ActionResult AjouterAgenda(int? id)
        {

            return View(db.Groupe_thematiqe.Find(id));
        }
        public ActionResult CR(int? idAg)
        {
            var CR=db.compte_rendu.Where(cr=>cr.Activites.AgendaID== idAg);
            return View(CR.ToList());
        }
        // GET: Groupe/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe_thematiqe groupe_thematiqe = db.Groupe_thematiqe.Find(id);

            if (groupe_thematiqe == null)
            {
                return HttpNotFound();
            }
            return View(groupe_thematiqe);
        }
        public ActionResult Consulte(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ////////////////////////////
            var currentId = User.Identity.GetUserId();
            var utilisateurs = db.Utilisateur.Include(u => u.AspNetUsers);
            var idAdmin = "4dcc3cda-6ee1-4a7c-9fd4-33b3a565ab80";
            var idadmin2 = "6c00274e-c4a1-46b8-9b00-f7ee345d0387";
            var gM = db.Membre_group.Where(m => m.Groupe_thematiqe.ID == id);



            //////////////////////////////////////////
            var thisuser = db.Utilisateur.Where(p => p.UserId != currentId &&p.UserId!= idadmin2&& p.UserId != idAdmin && p.CM !=true);
            //var mgids = db.Membre_group.FirstOrDefault(m => m.GroupId == id ).
           
            List<Utilisateur> UL = new List<Utilisateur>();
            foreach (var m in thisuser)
            {
               
                    if (db.Utilisateur.FirstOrDefault(p=>p.ID==m.ID).Membre_group.FirstOrDefault(p=>p.GroupId==id)==null)
                    {
                        Utilisateur util = new Utilisateur();
                        util.ID = m.ID;
                        util.Email = m.Email;
                        UL.Add(util);
                    }

            }

            ViewBag.MembreId = new SelectList(UL.Distinct(), "ID", "Email");
            //////////////////////////////////////////////////////////
           

            ViewBag.Type_ActiviteID = new SelectList(db.Type_Activite, "ID", "Nom_type");
            //////////////////////////////
            GroupeNameMemnersModel GNM = new GroupeNameMemnersModel();
            List<GroupeMembersDetailsModel> LGMDM = new List<GroupeMembersDetailsModel>();
            List<GroupeMembersDetailsModel> LGMDMF = new List<GroupeMembersDetailsModel>();
            List<GroupeMembersDetailsModel> LGFM = new List<GroupeMembersDetailsModel>();

            if (db.Groupe_thematiqe.FirstOrDefault(p => p.ID == id).Membre_group.ToList() != null)
            {
                var x = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId);
                var groupMembers = db.Groupe_thematiqe.FirstOrDefault(p => p.ID == id).Membre_group.ToList();
                foreach (var a in groupMembers)
                {
                    GroupeMembersDetailsModel GMDM = new GroupeMembersDetailsModel();
                    GMDM.IdU = a.Utilisateur.ID;
                    GMDM.nameUser = a.Utilisateur.Nom;
                    GMDM.emailUser = a.Utilisateur.Email;
                    //GMDM.datecreation = memberfiles.datecreation;
                    LGMDM.Add(GMDM);
                }
                foreach (var a in groupMembers)
                {
                    GroupeMembersDetailsModel GMDMF = new GroupeMembersDetailsModel();
                    var memberfiles = db.groue_detail.Where(p => p.Groupe_thematiqeID == id && p.MembreId == a.Utilisateur.ID).ToList();
                    foreach(var c in memberfiles)
                    {
                        if (memberfiles != null)
                        {
                            GMDMF.IdU = a.Utilisateur.ID;
                            GMDMF.nameUser = a.Utilisateur.Nom;
                            GMDMF.emailUser = a.Utilisateur.Email;
                            GMDMF.fichier = c.Fichier;
                            GMDMF.Text = c.Text;
                            GMDMF.datecreation = c.datecreation.Value;
                            LGMDMF.Add(GMDMF);
                        }
                    }
                    //GMDM.datecreation = memberfiles.datecreation; 
                }
                GNM.IDG = db.Groupe_thematiqe.FirstOrDefault(p => p.ID == id).ID;
                GNM.currentUser = x.ID;
                GNM.nomGroupe = db.Groupe_thematiqe.FirstOrDefault(p => p.ID == id).Nom_groupe;
                var cu = db.Groupe_thematiqe.FirstOrDefault(p => p.ID == id).CreatedById;
                if(db.Agenda.FirstOrDefault(a => a.GroupId == id) !=null)
                {
                    GNM.agendaExiste = db.Agenda.FirstOrDefault(a => a.GroupId == id).GroupId;
                    GNM.idagenda = db.Agenda.FirstOrDefault(a => a.GroupId == id).ID;
                }
                else
                {

                }
                GNM.CreatedById = db.Utilisateur.FirstOrDefault(u=>u.ID==cu).UserId;
                
                GNM.dateGroupe = db.Groupe_thematiqe.FirstOrDefault(p => p.ID == id).Date_createion.Value;
                GNM.MemebersFiles = LGMDMF.OrderByDescending(l=>l.datecreation).ToList();
                GNM.Memebers = LGMDM;
            }
            return View(GNM);
        }
       public void Telecharger(string file)
        {   
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
            Response.TransmitFile("~/AppDocuments/" + file);
            Response.Flush();
            Response.End();
        }
        public ActionResult addtextfile(groue_detail groue_Detail, int? id, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Server.MapPath("~/AppDocuments"), file.FileName);
                file.SaveAs(path);
                var currentId = User.Identity.GetUserId();
                var x = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId);
                groue_Detail.Fichier = file.FileName;
                groue_Detail.Groupe_thematiqeID = db.Groupe_thematiqe.FirstOrDefault(p => p.ID == id).ID;
                groue_Detail.MembreId =x.ID ;
                groue_Detail.datecreation=DateTime.Now;
                db.groue_detail.Add(groue_Detail);
                db.SaveChanges();
                //file.SaveAs(path);
                return RedirectToAction("Consulte", "Groupe",new { id });
            }
            
           
            return View();
        }
        // GET: Groupe/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Groupe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Groupe_thematiqe groupe_thematiqe)
        {
            var currentId = User.Identity.GetUserId();
            var x = db.Utilisateur.FirstOrDefault(p => p.UserId == currentId);
            DateTime aDate = DateTime.Now;
            var grpModel = new Groupe_thematiqe()
            {
                CreatedById = x.ID,
                Date_createion = aDate,
                Nom_groupe = groupe_thematiqe.Nom_groupe,
                Statut = false
            };

            if (ModelState.IsValid)
            {

                db.Groupe_thematiqe.Add(grpModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupe_thematiqe);
        }

        // GET: Groupe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe_thematiqe groupe_thematiqe = db.Groupe_thematiqe.Find(id);
            if (groupe_thematiqe == null)
            {
                return HttpNotFound();
            }
            return View(groupe_thematiqe);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateStatu(int id)
        {
            Groupe_thematiqe gt = db.Groupe_thematiqe.Find(id);
            gt.Statut = !gt.Statut.Value;
            db.Groupe_thematiqe.Attach(gt);
            db.Entry(gt).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // POST: Groupe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Groupe_thematiqe groupe_thematiqe)
        {
            if (ModelState.IsValid)
            {
                groupe_thematiqe.CreatedById = groupe_thematiqe.CreatedById;
                groupe_thematiqe.Date_createion = groupe_thematiqe.Date_createion;
                groupe_thematiqe.Statut = groupe_thematiqe.Statut;
                db.Entry(groupe_thematiqe).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupe_thematiqe);
        }

        // GET: Groupe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe_thematiqe groupe_thematiqe = db.Groupe_thematiqe.Find(id);
            if (groupe_thematiqe == null)
            {
                return HttpNotFound();
            }
            return View(groupe_thematiqe);
        }

        // POST: Groupe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groupe_thematiqe groupe_thematiqe = db.Groupe_thematiqe.Find(id);
            db.Groupe_thematiqe.Remove(groupe_thematiqe);
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
