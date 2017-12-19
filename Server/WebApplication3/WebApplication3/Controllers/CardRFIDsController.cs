using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CardRFIDsController : ApiController
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: api/CardRFIDs
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(IQueryable<CardRFID>))]
        public IHttpActionResult GetCardRfids(int idQueue)
        {
            string userId = User.Identity.GetUserId();

            if (db.Administrators.Count(a => a.Id == userId) == 0)
            {
                BadRequest("You dont administrator");
            }

            IQueryable<CardRFID> cardRfids = db.CardRfids.Where(c => c.Queue.Id == idQueue);
            return Ok(cardRfids);
        }

        // GET: api/CardRFIDs/5
        [ResponseType(typeof(CardRFID))]
        public async Task<IHttpActionResult> GetCardRFID(int id)
        {
            CardRFID cardRFID = await db.CardRfids.FindAsync(id);
            if (cardRFID == null)
            {
                return NotFound();
            }

            return Ok(cardRFID);
        }

        // PUT: api/CardRFIDs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCardRFID(int id, CardRFID cardRFID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardRFID.Id)
            {
                return BadRequest();
            }

            db.Entry(cardRFID).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardRFIDExists(id))
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

        // POST: api/CardRFIDs
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(CardRFID))]
        public async Task<IHttpActionResult> PostCardRFID(CardRFID card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            if (db.Administrators.Count(a => a.Id == userId) == 0)
            {
                BadRequest("You dont administrator");
            }

            card.Queue = db.Queues.FirstOrDefault(q => q.Id == card.Queue.Id);
            card.Uid = card.Uid.ToUpper();

            db.CardRfids.Add(card);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = card.Id }, card);
        }

        // DELETE: api/CardRFIDs/5
        [ResponseType(typeof(CardRFID))]
        public async Task<IHttpActionResult> DeleteCardRFID(int id)
        {
            CardRFID cardRFID = await db.CardRfids.FindAsync(id);
            if (cardRFID == null)
            {
                return NotFound();
            }

            db.CardRfids.Remove(cardRFID);
            await db.SaveChangesAsync();

            return Ok(cardRFID);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardRFIDExists(int id)
        {
            return db.CardRfids.Count(e => e.Id == id) > 0;
        }
    }
}