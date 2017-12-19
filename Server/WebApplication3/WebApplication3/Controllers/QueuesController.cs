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
    [RoutePrefix("api/Queues")]
    public class QueuesController : ApiController
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        QueuesController()
        {
            
        }

        // GET: api/Queues/All
        [Route("All")]
        public IQueryable<Queue> GetAllAdministrators()
        {
            return db.Queues;
        }

        // GET: api/Queues
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(IQueryable<Event>))]
        public IHttpActionResult GetQueues()
        {
            string userId = User.Identity.GetUserId();

            if (db.Administrators.Count(a => a.Id == userId) == 0)
            {
                BadRequest("You dont administrator");
            }

            Administrator administrator = db.Administrators.FirstOrDefault(a => a.Id == userId);
            var queues = db.Queues.Where(q => q.Event.Organization.Id == administrator.Organization.Id);

            return Ok(queues);
        }

        // GET: api/Queues/5
        [ResponseType(typeof(Queue))]
        public async Task<IHttpActionResult> GetQueue(int id)
        {
            Queue queue = await db.Queues.FindAsync(id);
            if (queue == null)
            {
                return NotFound();
            }

            return Ok(queue);
        }

        // PUT: api/Queues/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQueue(int id, Queue queue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != queue.Id)
            {
                return BadRequest();
            }

            db.Entry(queue).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueExists(id))
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

        // POST: api/Queues
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(Queue))]
        public async Task<IHttpActionResult> PostQueue(Queue queue)
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
            //Administrator administrator = db.Administrators.FirstOrDefault(a => a.Id == userId);

            queue.Event = await db.Events.FirstOrDefaultAsync(e => e.Id == queue.Event.Id);

            db.Queues.Add(queue);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = queue.Id }, queue);
        }

        // DELETE: api/Queues/5
        [ResponseType(typeof(Queue))]
        public async Task<IHttpActionResult> DeleteQueue(int id)
        {
            Queue queue = await db.Queues.FindAsync(id);
            if (queue == null)
            {
                return NotFound();
            }

            db.Queues.Remove(queue);
            await db.SaveChangesAsync();

            return Ok(queue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QueueExists(int id)
        {
            return db.Queues.Count(e => e.Id == id) > 0;
        }
    }
}