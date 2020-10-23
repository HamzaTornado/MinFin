using MinisitreFin.Models;
using MinisitreFin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace MinisitreFin.Controllers
{

    public class InitiativesApiController : ApiController
    {

        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();
     
       
        public List<InitiativesList> GetInitiatives()
        {
            List<InitiativesList> LIL = new List<InitiativesList>();
            var oini = db.Initiatives.ToList();
            foreach (var ini in oini)
            {
                InitiativesList initiatives = new InitiativesList()
                {
                    ID = ini.ID,
                    Nom_init = ini.Nom_init,
                    Date_debu = ini.Date_debu.Value,
                    Date_fin = ini.Date_fin.Value,
                    Objectifs_generaux = ini.Objectifs_generaux,
                    Obgectifs_specifiques = ini.Obgectifs_specifiques,
                    Description_court = ini.Description_court,
                    Description_detaillee = ini.Description_detaillee,
                    Budget = ini.Budget,
                    Approbateur = ini.Approbateur,
                    Cofinancement = ini.Cofinancement
                };
                LIL.Add(initiatives);
            }

            return LIL;
        }

        
        public IHttpActionResult GetInitiative(int id)
        {
            var ini = db.Initiatives.Find(id);
            if (ini == null)
            {
                UserResponseMessage userResponse= new UserResponseMessage
                {
                    IsSuccess = false,
                    Message = "",
                    Errors=null

                };
                return BadRequest("Initiative introuvable");
                 
            }
            InitiativesList initiatives = new InitiativesList()
            {
                ID = ini.ID,
                Nom_init = ini.Nom_init,
                Date_debu = ini.Date_debu.Value,
                Date_fin = ini.Date_fin.Value,
                Objectifs_generaux = ini.Objectifs_generaux,
                Obgectifs_specifiques = ini.Obgectifs_specifiques,
                Description_court = ini.Description_court,
                Description_detaillee = ini.Description_detaillee,
                Budget = ini.Budget,
                Approbateur = ini.Approbateur,
                Cofinancement = ini.Cofinancement
            };
            return Ok(initiatives);
        }
        //public async Task<> Get(int id)
        //{
        //    var even = await db.Evenements.FindAsync(id);
        //    //if (even != null )
        //    //{
        //    //    return BadRequest();
        //    //}
        //    EvenementsList evn = new EvenementsList()
        //    {
        //        ID = even.ID,
        //        Titre_even = even.Titre_even,
        //        Description = even.Description,
        //        Date_debut = even.Date_debut.Value,
        //        Date_fin = even.Date_fin.Value
        //    };
        //    return Ok(evn);
        //}

    }
    //public class InitiativesApiController : Controller
    //{

    //    private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

    //    public  ActionResult GetInitiatives()
    //    {
    //        List<InitiativesList> LIL = new List<InitiativesList>();
    //        var oini = db.Initiatives.ToList();

    //        return Json(db.Initiatives.AsEnumerable().Select(i=>new {
    //            id = i.ID,
    //            nom_init = i.Nom_init,
    //            date_debu = i.Date_debu.Value.Date.ToString("yyyy-MM-dd"),
    //            date_fin = i.Date_fin.Value.Date.ToString("yyyy-MM-dd"),
    //            objectifs_generaux = i.Objectifs_generaux,
    //            obgectifs_specifiques = i.Obgectifs_specifiques,
    //            description_court = i.Description_court,
    //            description_detaillee = i.Description_detaillee,
    //            budget = i.Budget,
    //            aprobateur = i.Approbateur,
    //            cofinancement = i.Cofinancement
    //        }).ToList(), JsonRequestBehavior.AllowGet);
    //    }

    //}
}
