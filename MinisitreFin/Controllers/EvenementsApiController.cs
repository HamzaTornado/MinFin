using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MinisitreFin.Models;
using MinisitreFin.ViewModels;
using System.Web.Http.Cors;
using System.Web.Http.Description;
namespace MinisitreFin.Controllers
{
    [EnableCors(origins: "http://localhost:4401", headers: "*", methods: "get") ]
    public class EvenementsApiController : ApiController
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        [HttpGet]
        public IHttpActionResult GetEvenements()
        {
            try
            {
                var events = db.Evenements.ToList();
                List<EvenementsList> evn = new List<EvenementsList>();
                foreach (var even in events )
                {
                    EvenementsList evenements = new EvenementsList
                    {
                        ID = even.ID,
                        Titre_even = even.Titre_even,
                        Description = even.Description,
                        Date_debut = even.Date_debut.Value,
                        Date_fin = even.Date_fin.Value
                    };
                    evn.Add(evenements);
                }
                return Ok(evn);
            }
            catch (Exception)
            {
                
                return BadRequest();
            } 
            
        }
        // GET: api/EvenementsApi/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var even = db.Evenements.Find(id);
                if (even == null)
                {
                    return NotFound();
                }
                EvenementsList evn = new EvenementsList()
                {
                    ID = even.ID,
                    Titre_even = even.Titre_even,
                    Description = even.Description,
                    Date_debut = even.Date_debut.Value,
                    Date_fin = even.Date_fin.Value
                };
                return Ok(evn);
            }
            catch(Exception)
            {
                
                return BadRequest();
            }
            
        }
    }
}


//[ResponseType(typeof(Evenements))]
//public IHttpActionResult GetEvenements(int id)
//{
//    Evenements evenements = db.Evenements.Find(id);
//    if (evenements == null)
//    {
//        return NotFound();
//    }

//    return Ok(evenements);
//}

// GET: api/EvenementsApi
//[HttpGet]
//public IEnumerable Get()
//{
//    List<EvenementsList> Eventlist = new List<EvenementsList>();
//    var events = db.Evenements.Where(e=>e.Statut==true).ToList();
//    if (events == null)
//    {
//        yield return NotFound();
//    }
//    var request = HttpContext.Current.Request;
//    var appUrl = HttpRuntime.AppDomainAppVirtualPath;
//    if (appUrl != "/")
//        appUrl = "/" + appUrl;
//    var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

//    foreach (var even in events)
//    {
//        EvenementsList evn = new EvenementsList() {
//            ID = even.ID,
//            Titre_even = even.Titre_even,
//            Description = even.Description,
//            Date_debut = even.Date_debut.Value,
//            Date_fin = even.Date_fin.Value
//        };
//        Eventlist.Add(evn);
//    }
//    yield return db.Evenements.Where(e => e.Statut == true).ToArray();
//}
