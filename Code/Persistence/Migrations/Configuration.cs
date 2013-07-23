using Persistence.Tests;

namespace Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<VacationManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VacationManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var employees = TestDataBuilder.BuildEmployees();
            TestDataBuilder.BuildEmployeeSituations(employees);
            context.Employees.AddOrUpdate(employees.ToArray());

            var requests = TestDataBuilder.BuildVacationRequests(employees);
            context.Requests.AddOrUpdate(requests.ToArray());
        }
    }
}
