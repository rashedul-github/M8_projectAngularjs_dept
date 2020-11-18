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
    public class PermanentStaffsController : ApiController
    {
        private DepartmentDbContext db = new DepartmentDbContext();

        // GET: api/PermanentStaffs
        public IQueryable<PermanentStaff> GetPermanentStaffs()
        {
            return db.PermanentStaffs;
        }

        // GET: api/PermanentStaffs/5
        [ResponseType(typeof(PermanentStaff))]
        public IHttpActionResult GetPermanentStaff(int id)
        {
            PermanentStaff permanentStaff = db.PermanentStaffs.Find(id);
            if (permanentStaff == null)
            {
                return NotFound();
            }

            return Ok(permanentStaff);
        }

        // PUT: api/PermanentStaffs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermanentStaff(int id, PermanentStaff permanentStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permanentStaff.PermanentStaffId)
            {
                return BadRequest();
            }

            db.Entry(permanentStaff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermanentStaffExists(id))
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

        // POST: api/PermanentStaffs
        [ResponseType(typeof(PermanentStaff))]
        public IHttpActionResult PostPermanentStaff(PermanentStaff permanentStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PermanentStaffs.Add(permanentStaff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = permanentStaff.PermanentStaffId }, permanentStaff);
        }

        // DELETE: api/PermanentStaffs/5
        [ResponseType(typeof(PermanentStaff))]
        public IHttpActionResult DeletePermanentStaff(int id)
        {
            PermanentStaff permanentStaff = db.PermanentStaffs.Find(id);
            if (permanentStaff == null)
            {
                return NotFound();
            }

            db.PermanentStaffs.Remove(permanentStaff);
            db.SaveChanges();

            return Ok(permanentStaff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermanentStaffExists(int id)
        {
            return db.PermanentStaffs.Count(e => e.PermanentStaffId == id) > 0;
        }
    }
}