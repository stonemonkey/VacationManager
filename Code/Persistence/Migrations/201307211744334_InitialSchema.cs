namespace Persistence.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(),
                        Firstname = c.String(),
                        Cnp = c.String(),
                        Gender = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        HireDate = c.DateTime(nullable: false),
                        Roles = c.Int(nullable: false),
                        Salt = c.String(),
                        Password = c.String(),
                        Manager_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Manager_Id)
                .Index(t => t.Manager_Id);
            
            CreateTable(
                "dbo.EmployeeSituations",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Year = c.Int(nullable: false),
                        PaidDays = c.Int(nullable: false),
                        ConsumedDays = c.Int(nullable: false),
                        AvailableDays = c.Int(nullable: false),
                        Employee_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.VacationRequests",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        Employee_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.VacationRequests", new[] { "Employee_Id" });
            DropIndex("dbo.EmployeeSituations", new[] { "Id" });
            DropIndex("dbo.EmployeeSituations", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "Manager_Id" });
            DropForeignKey("dbo.VacationRequests", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.EmployeeSituations", "Id", "dbo.Employees");
            DropForeignKey("dbo.EmployeeSituations", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Manager_Id", "dbo.Employees");
            DropTable("dbo.VacationRequests");
            DropTable("dbo.EmployeeSituations");
            DropTable("dbo.Employees");
        }
    }
}
