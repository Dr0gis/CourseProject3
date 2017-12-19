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
    [RoutePrefix("api/Results")]
    public class ResultsController : ApiController
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public class ResultTemp
        {
            public string Email { get; set; }
            public string DateTimeRegistration { get; set; }

            public ResultTemp(string email, string dateTimeRegistration)
            {
                Email = email;
                DateTimeRegistration = dateTimeRegistration;
            }
        }

        // GET: api/Results
        public List<ResultTemp> GetResult(int idQueue)
        {
            List<Result> list = db.Result.Where(r => r.Queue.Id == idQueue).ToList();
            List<ResultTemp> listResultTemps = new List<ResultTemp>();
            foreach (var item in list)
            {
                if (item.DateSuccess == null)
                {
                    listResultTemps.Add(new ResultTemp(item.User.Email, item.DateTimeRegistration));
                }
            }
            return listResultTemps;
        }

        //// GET: api/Results/5
        //[ResponseType(typeof(Result))]
        //public async Task<IHttpActionResult> GetResult(int id)
        //{
        //    Result result = await db.Result.FindAsync(id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

        // PUT: api/Results/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutResult(int id, Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.Id)
            {
                return BadRequest();
            }

            db.Entry(result).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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

        // POST: api/Results
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(Result))]
        public async Task<IHttpActionResult> PostResult(Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.CardRfids.Count(c => c.Queue.Id == result.Queue.Id && c.Id == result.CardRfid.Id) == 0)
            {
                return BadRequest("Error card number");
            }

            string userId = User.Identity.GetUserId();
            if (db.Result.Count(r => r.User.Id == userId) > 0)
            {
                return BadRequest("User already exists");
            }

            if (db.Result.Count(r => r.CardRfid.Id == result.CardRfid.Id) > 0)
            {
                return BadRequest("Card already exists");
            }

            result.CardRfid = await db.CardRfids.FirstOrDefaultAsync(c => c.Id == result.CardRfid.Id);
            result.Queue = await db.Queues.FirstOrDefaultAsync(q => q.Id == result.Queue.Id);
            result.User = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (result.User == null)
            {
                return BadRequest("You dont login");
            }

            db.Result.Add(result);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = result.Id }, result);
        }

        // POST: api/Results/Success
        [Route("Success")]
        public IHttpActionResult PostResult(int idQueue, string uidCard)
        {
            Result result = db.Result.OrderBy(r => r.DateTimeRegistration).FirstOrDefault(r => r.Queue.Id == idQueue);
            if (result.CardRfid.Uid == uidCard)
            {
                result.DateSuccess = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
            }
            db.SaveChanges();
            return Ok();
        }

        // DELETE: api/Results/5
        [ResponseType(typeof(Result))]
        public async Task<IHttpActionResult> DeleteResult(int id)
        {
            Result result = await db.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            db.Result.Remove(result);
            await db.SaveChangesAsync();

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultExists(int id)
        {
            return db.Result.Count(e => e.Id == id) > 0;
        }
    }
}