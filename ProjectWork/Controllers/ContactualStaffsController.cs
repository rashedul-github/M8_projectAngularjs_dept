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
using ProjectWork.Models;

namespace ProjectWork.Controllers
{
    public class ContactualStaffsController : ApiController
    {
        private DepartmentDbContext db = new DepartmentDbContext();

        // GET: api/ContactualStaffs
        public IQueryable<ContactualStaff> GetContactualStaffs()
        {
            return db.ContactualStaffs;
        }

        // GET: api/ContactualStaffs/5
        [ResponseType(typeof(ContactualStaff))]
        public IHttpActionResult GetContactualStaff(int id)
        {
            ContactualStaff contactualStaff = db.ContactualStaffs.Find(id);
            if (contactualStaff == null)
            {
                return NotFound();
            }

            return Ok(contactualStaff);
        }

        // PUT: api/ContactualStaffs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactualStaff(int id, ContactualStaff contactualStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactualStaff.ContactualStaffId)
            {
                return BadRequest();
            }

            db.Entry(contactualStaff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactualStaffExists(id))
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

        // POST: api/ContactualStaffs
        [ResponseType(typeof(ContactualStaff))]
        public IHttpActionResult PostContactualStaff(ContactualStaff contactualStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactualStaffs.Add(contactualStaff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactualStaff.ContactualStaffId }, contactualStaff);
        }

        // DELETE: api/ContactualStaffs/5
        [ResponseType(typeof(ContactualStaff))]
        public IHttpActionResult DeleteContactualStaff(int id)
        {
            ContactualStaff contactualStaff = db.ContactualStaffs.Find(id);
            if (contactualStaff == null)
            {
                return NotFound();
            }

            db.ContactualStaffs.Remove(contactualStaff);
            db.SaveChanges();

            return Ok(contactualStaff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactualStaffExists(int id)
        {
            return db.ContactualStaffs.Count(e => e.ContactualStaffId == id) > 0;
        }
    }
}