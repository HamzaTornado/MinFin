using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MinisitreFin.Models;

namespace MinisitreFin.Controllers
{
    public class TESTController : ApiController
    {
        private MinistreFinEntitiesDB db = new MinistreFinEntitiesDB();

        // GET: api/TEST
        public IQueryable<Evenements> GetEvenements()
        {
            return db.Evenements;
        }

        // GET: api/TEST/5
        [ResponseType(typeof(Evenements))]
        public IHttpActionResult GetEvenements(int id)
        {
            Evenements evenements = db.Evenements.Find(id);
            if (evenements == null)
            {
                return NotFound();
            }

            return Ok(evenements);
        }

        // PUT: api/TEST/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvenements(int id, Evenements evenements)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evenements.ID)
            {
                return BadRequest();
            }

            db.Entry(evenements).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TEST
        [ResponseType(typeof(Evenements))]
        public IHttpActionResult PostEvenements(Evenements evenements)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Evenements.Add(evenements);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = evenements.ID }, evenements);
        }

        // DELETE: api/TEST/5
        [ResponseType(typeof(Evenements))]
        public IHttpActionResult DeleteEvenements(int id)
        {
            Evenements evenements = db.Evenements.Find(id);
            if (evenements == null)
            {
                return NotFound();
            }

            db.Evenements.Remove(evenements);
            db.SaveChanges();

            return Ok(evenements);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EvenementsExists(int id)
        {
            return db.Evenements.Count(e => e.ID == id) > 0;
        }
    }
}