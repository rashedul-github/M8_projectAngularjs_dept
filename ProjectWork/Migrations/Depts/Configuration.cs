namespace ProjectWork.Migrations.Depts
{
    using ProjectWork.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectWork.Models.DepartmentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Depts";
        }

        protected override void Seed(ProjectWork.Models.DepartmentDbContext db)
        {
            Department d = new Department { DepartmentName = "Accounts" };
            d.permanentStaff.Add(new PermanentStaff { PermanentStaffName = "Rashedul", JoinDate = DateTime.Parse("01-01-2019"), MonthlySalary = 25000.00M, DepartmentId = 1 });
            d.permanentStaff.Add(new PermanentStaff { PermanentStaffName = "Shahidul", JoinDate = DateTime.Parse("01-01-2018"), MonthlySalary = 26000.00M, DepartmentId = 1 });
            d.contactualStaff.Add(new ContactualStaff { ContactualStaffName = "Rakibul", StartDate = DateTime.Parse("01-01-2019"), WeeklySalary = 6000.00M, DepartmentId = 1 });
            d.contactualStaff.Add(new ContactualStaff { ContactualStaffName = "Jahidul", StartDate = DateTime.Parse("01-01-2017"), WeeklySalary = 5000.00M, DepartmentId = 1 });
            db.Departments.Add(d);
            Department d1 = new Department { DepartmentName = "IT" };
            d1.permanentStaff.Add(new PermanentStaff { PermanentStaffName = "Shomon", JoinDate = DateTime.Parse("01-01-2019"), MonthlySalary = 35000.00M, DepartmentId = 2 });
            d1.permanentStaff.Add(new PermanentStaff { PermanentStaffName = "Gahid", JoinDate = DateTime.Parse("01-01-2018"), MonthlySalary = 56000.00M, DepartmentId = 2 });
            d1.contactualStaff.Add(new ContactualStaff { ContactualStaffName = "Shomi", StartDate = DateTime.Parse("01-01-2019"), WeeklySalary = 4000.00M, DepartmentId = 2 });
            d1.contactualStaff.Add(new ContactualStaff { ContactualStaffName = "Shojib", StartDate = DateTime.Parse("01-01-2017"), WeeklySalary = 3500.00M, DepartmentId = 2 });
            db.Departments.Add(d1);
            db.SaveChanges();
        }
    }
}
