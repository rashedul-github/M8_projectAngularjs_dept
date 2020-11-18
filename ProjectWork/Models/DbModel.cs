using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectWork.Models
{
    public class Department
    {
        public Department()
        {
            this.permanentStaff = new HashSet<PermanentStaff>();
            this.contactualStaff = new HashSet<ContactualStaff>();
        }
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Department name is required."), StringLength(40)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        //
        public ICollection<PermanentStaff> permanentStaff { get; set; }
        public ICollection<ContactualStaff> contactualStaff { get; set; }
    }
    public class PermanentStaff
    {
        [Display(Name = "PermanentStaff Id")]
        public int PermanentStaffId { get; set; }
        [Required(ErrorMessage = "Staff name is required."), StringLength(40)]
        [Display(Name = "Permanent Staff Name")]
        public string PermanentStaffName { get; set; }
        [Required(ErrorMessage = "Join date is required."), Column(TypeName = "date"), Display(Name = "Join Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
        [Required(ErrorMessage = "Salary is required."), Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Monthly Salary")]
        public decimal MonthlySalary { get; set; }
        [Required]
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        //
        [ForeignKey("DepartmentId")]
        public virtual Department department { get; set; }
    }
    public class ContactualStaff
    {
        [Display(Name = "Contactual Staff Id")]
        public int ContactualStaffId { get; set; }
        [Required(ErrorMessage = "Staff name is required."), StringLength(40)]
        [Display(Name = "Contactual Staff Name")]
        public string ContactualStaffName { get; set; }
        [Required(ErrorMessage = "Start date is required."), Column(TypeName = "date"), Display(Name = "Start Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Salary is required."), Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Weekly Salary")]
        public decimal WeeklySalary { get; set; }
        [Required]
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        //
        [ForeignKey("DepartmentId")]
        public virtual Department department { get; set; }
    }
    public class DepartmentDbContext : DbContext
    {
        public DepartmentDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PermanentStaff> PermanentStaffs { get; set; }
        public DbSet<ContactualStaff> ContactualStaffs { get; set; }
    }
}