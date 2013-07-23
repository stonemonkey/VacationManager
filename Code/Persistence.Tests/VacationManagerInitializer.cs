using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Persistence.Tests
{
    public class VacationManagerInitializer : DropCreateDatabaseAlways<VacationManagerContext>
    {
        protected override void Seed(VacationManagerContext context)
        {
            var employees = TestDataBuilder.BuildEmployees();
            TestDataBuilder.BuildEmployeeSituations(employees);
            context.Employees.AddOrUpdate(employees.ToArray());
            
            var requests = TestDataBuilder.BuildVacationRequests(employees);
            context.Requests.AddOrUpdate(requests.ToArray());
        }
    }
}