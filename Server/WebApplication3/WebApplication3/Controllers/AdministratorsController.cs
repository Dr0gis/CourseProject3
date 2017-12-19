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
    [RoutePrefix("api/Administrators")]
    public class AdministratorsController : ApiController
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: api/Administrators
        public IQueryable<Administrator> GetAdministrators()
        {
            return db.Administrators;
        }

        // GET: api/Administrators/AdministratorInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(Administrator))]
        [Route("AdministratorInfo")]
        public async Task<IHttpActionResult> GetAdministratorInfo(/*string id*/)
        {
            string userId = User.Identity.GetUserId();
            Administrator administrator = await db.Administrators.FirstOrDefaultAsync(a => a.Id == userId);
            if (administrator == null)
            {
                return NotFound();
            }

            return Ok(administrator);
        }

        // PUT: api/Administrators/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAdministrator(string id, Administrator administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrator.Id)
            {
                return BadRequest("You already administrator");
            }

            db.Entry(administrator).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/Administrators/AddOrganization
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(void))]
        [Route("AddOrganization")]
        public async Task<IHttpActionResult> PostAddOrganization(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Entry(administrator).State = EntityState.Modified;

            string userId = User.Identity.GetUserId();
            Administrator administrator = await db.Administrators.FirstOrDefaultAsync(a => a.Id == userId);
            administrator.Organization = await db.Organizations.FirstOrDefaultAsync(o => o.Id == organization.Id);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(userId))
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

        // POST: api/Administrators
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(Administrator))]
        public async Task<IHttpActionResult> PostAdministrator()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string email = User.Identity.GetUserName();
            if (db.Administrators.Count(a => a.User.Email == email) > 0)
            {
                return BadRequest();
            }

            Administrator administrator = new Administrator();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Email == email);
            administrator.Id = user.Id;
            administrator.User = user;

            db.Administrators.Add(administrator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdministratorExists(administrator.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = administrator.Id }, administrator);
        }

        // DELETE: api/Administrators/Organization
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [ResponseType(typeof(Administrator))]
        [Route("Organization")]
        public async Task<IHttpActionResult> DeleteOrganization(/*string id*/)
        {
            string userId = User.Identity.GetUserId();
            Administrator administrator = db.Administrators.FirstOrDefault(a => a.Id == userId);
            if (administrator == null)
            {
                return NotFound();
            }

            administrator.Organization = null;

            await db.SaveChangesAsync();

            return Ok(administrator);
        }

        // DELETE: api/Administrators/5
        [ResponseType(typeof(Administrator))]
        public async Task<IHttpActionResult> DeleteAdministrator(string id)
        {
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            db.Administrators.Remove(administrator);
            await db.SaveChangesAsync();

            return Ok(administrator);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministratorExists(string id)
        {
            return db.Administrators.Count(e => e.Id == id) > 0;
        }
    }
}