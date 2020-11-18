namespace ProjectWork.Migrations.Depts
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation_Depts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactualStaffs",
                c => new
                    {
                        ContactualStaffId = c.Int(nullable: false, identity: true),
                        ContactualStaffName = c.String(nullable: false, maxLength: 40),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        WeeklySalary = c.Decimal(nullable: false, storeType: "money"),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactualStaffId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.PermanentStaffs",
                c => new
                    {
                        PermanentStaffId = c.Int(nullable: false, identity: true),
                        PermanentStaffName = c.String(nullable: false, maxLength: 40),
                        JoinDate = c.DateTime(nullable: false, storeType: "date"),
                        MonthlySalary = c.Decimal(nullable: false, storeType: "money"),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PermanentStaffId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermanentStaffs", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ContactualStaffs", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.PermanentStaffs", new[] { "DepartmentId" });
            DropIndex("dbo.ContactualStaffs", new[] { "DepartmentId" });
            DropTable("dbo.PermanentStaffs");
            DropTable("dbo.Departments");
            DropTable("dbo.ContactualStaffs");
        }
    }
}
