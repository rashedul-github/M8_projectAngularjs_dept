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
    public class DepartmentsController : ApiController
    {
        private DepartmentDbContext db = new DepartmentDbContext();

        // GET: api/Departments
        public IQueryable<Department> GetDepartments()
        {
            return db.Departments;
        }

        // GET: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult GetDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartment(int id, Department dept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dept.DepartmentId)
            {
                return BadRequest();
            }

            var ext = db.Departments.Include(x => x.permanentStaff).Include(y => y.contactualStaff).First(x => x.DepartmentId == dept.DepartmentId);
            ext.DepartmentName = dept.DepartmentName;
            //permanent
            if (dept.permanentStaff != null && dept.permanentStaff.Count > 0)
            {
                var perStaff = dept.permanentStaff.ToArray();
                for (var i = 0; i < perStaff.Length; i++)
                {
                    var temp = ext.permanentStaff.FirstOrDefault(c => c.PermanentStaffId == perStaff[i].PermanentStaffId);
                    if (temp != null)
                    {
                        temp.PermanentStaffName = perStaff[i].PermanentStaffName;
                        temp.JoinDate = perStaff[i].JoinDate;
                        temp.MonthlySalary = perStaff[i].MonthlySalary;
                    }
                    else
                    {
                        ext.permanentStaff.Add(perStaff[i]);
                    }
                }
                perStaff = ext.permanentStaff.ToArray();
                for (var i = 0; i < perStaff.Length; i++)
                {
                    var temp = dept.permanentStaff.FirstOrDefault(d => d.PermanentStaffId == perStaff[i].PermanentStaffId);
                    if (temp == null)
                        db.Entry(perStaff[i]).State = EntityState.Deleted;
                }
            }
            //contacttual
            if (dept.contactualStaff != null && dept.contactualStaff.Count > 0)
            {
                var conStaff = dept.contactualStaff.ToArray();
                for (var i = 0; i < conStaff.Length; i++)
                {
                    var temp = ext.contactualStaff.FirstOrDefault(c => c.ContactualStaffId == conStaff[i].ContactualStaffId);
                    if (temp != null)
                    {
                        temp.ContactualStaffName = conStaff[i].ContactualStaffName;
                        temp.StartDate = conStaff[i].StartDate;
                        temp.WeeklySalary = conStaff[i].WeeklySalary;
                    }
                    else
                    {
                        ext.contactualStaff.Add(conStaff[i]);
                    }
                }
                conStaff = ext.contactualStaff.ToArray();
                for (var i = 0; i < conStaff.Length; i++)
                {
                    var temp = dept.contactualStaff.FirstOrDefault(d => d.ContactualStaffId == conStaff[i].ContactualStaffId);
                    if (temp == null)
                        db.Entry(conStaff[i]).State = EntityState.Deleted;
                }
            }

            //db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        [ResponseType(typeof(Department))]
        public IHttpActionResult PostDepartment(Department department)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentId == id) > 0;
        }
        //custom
        [Route("custom/DeptList")]
        public IQueryable<Department> GetDeptWithChild()
        {
            return db.Departments.Include(x => x.permanentStaff).Include(c=>c.contactualStaff);
        }
        //custom {id}
        [Route("custom/Departments/{id}")]
        public IHttpActionResult GetDeptWithRelated(int id)
        {
            return Ok(db.Departments
                    .Include(x => x.permanentStaff)
                    .Include(x => x.contactualStaff)
                    .FirstOrDefault(x => x.DepartmentId == id));
        }

    }
}