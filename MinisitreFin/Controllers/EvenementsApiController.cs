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
    [EnableCors(origins: "*", headers: "*", methods: "get") ]
    public class EvenementsApiController : ApiController
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        [HttpGet]
        public IHttpActionResult GetEvenements()
        {
            try
            {
                var events = db.Evenements.Where(e => e.Statut == true).ToList();
                var request = HttpContext.Current.Request;
                var baseUrl = request.Url.Scheme + "://" + request.Url.Authority;
                List<EvenementsList> evn = new List<EvenementsList>();
                foreach (var even in events )
                {
                    EvenementsList evenements = new EvenementsList
                    {
                        ID = even.ID,
                        Titre_even = even.Titre_even,
                        Description = even.Description,
                        Date_debut = even.Date_debut.Value,
                        Date_fin = even.Date_fin.Value,
                        ImageUrl = baseUrl+"/AppImg/"+even.Image
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
                var request = HttpContext.Current.Request;
                var baseUrl = request.Url.Scheme + "://" + request.Url.Authority;
                EvenementsList evn = new EvenementsList()
                {
                    ID = even.ID,
                    Titre_even = even.Titre_even,
                    Description = even.Description,
                    Date_debut = even.Date_debut.Value,
                    Date_fin = even.Date_fin.Value,
                    ImageUrl = baseUrl + "/AppImg/" + even.Image
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


