using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder.Dates;
using VacationManager.Common.Model;
using VacationManager.Persistence.Model;

namespace VacationManager.Persistence.Tests
{
    public class VacationManagerInitializer : DropCreateDatabaseAlways<VacationManagerContext>
    {
        protected override void Seed(VacationManagerContext context)
        {
            var employees = BuildEmployees();
            employees.ForEach(e => context.Employees.Add(e));

            var vacationDays = BuildVacationStatuses(employees);
            vacationDays.ForEach(vd => context.VacationStatus.Add(vd));

            var vacationRequests = BuildVacationRequests(employees);
            vacationRequests.ForEach(vr => context.Requests.Add(vr));
        }

        private static List<Employee> BuildEmployees()
        {
            var cornelBrody = new Employee
            {
                Firstname = "Cornel", LastName = "Brody", BirthDate = The.Year(1956).On.May.The10th,
                HireDate = The.Year(1999).On.February.The15th,
                Roles = EmployeeRoles.Manager, 
                Email = "cornel.brody@contoso.com", 
                Manager = null
            };
            var mihaiBarabas = new Employee
            {
                Firstname = "Mihai", LastName = "Barabas", BirthDate = The.Year(1978).On.July.The7th,
                HireDate = The.Year(2004).On.August.The21st,
                Roles = EmployeeRoles.Manager | EmployeeRoles.Executive, 
                Email = "mihai.barabas@contoso.com", 
                Manager = cornelBrody
            };
            var cosminMolnar = new Employee
            {
                Firstname = "Cosmin", LastName = "Molnar", BirthDate = The.Year(1975).On.January.The24th,
                HireDate = The.Year(2012).On.May.The13th,
                Roles = EmployeeRoles.Manager | EmployeeRoles.Executive, 
                Email = "cosmin.molnar@contoso.com", 
                Manager = cornelBrody
            };

            return new List<Employee>
            {
                cornelBrody, mihaiBarabas, cosminMolnar,
                new Employee
                {
                    Firstname = "Hr", LastName = "Generic", BirthDate = The.Year(1900).On.January.The1st,
                    HireDate = The.Year(2012).On.May.The13th,
                    Roles = EmployeeRoles.Hr, 
                    Email = "hr_holidays@contoso.com", 
                    Manager = null,
                },
                new Employee
                {
                    Firstname = "Daniel", LastName = "Savu", BirthDate = The.Year(1980).On.December.The12th,
                    HireDate = The.Year(2012).On.May.The13th,
                    Roles = EmployeeRoles.Executive, 
                    Email = "daniel.savu@contoso.com", 
                    Manager = mihaiBarabas,
                },
                new Employee
                {
                    Firstname = "Costin", LastName = "Morariu", BirthDate = The.Year(1977).On.July.The4th,
                    HireDate = The.Year(2012).On.May.The13th,
                    Roles = EmployeeRoles.Executive, 
                    Email = "costin.morariu@contoso.com", 
                    Manager = mihaiBarabas,
                },
                new Employee
                {
                    Firstname = "Ioana", LastName = "Sandu",  BirthDate = The.Year(1983).On.March.The20th,
                    HireDate = The.Year(2012).On.May.The13th,                    
                    Roles = EmployeeRoles.Hr | EmployeeRoles.Executive, 
                    Email = "ioana.sandu@contoso.com", 
                    Manager = cosminMolnar,
                },
            };
        }

        private static List<VacationStatus> BuildVacationStatuses(ICollection<Employee> employees)
        {
            var cornel = employees.Single(x => x.Firstname == "Cornel");
            var mihai = employees.Single(x => x.Firstname == "Mihai");
            var cosmin = employees.Single(x => x.Firstname == "Cosmin");
            var daniel = employees.Single(x => x.Firstname == "Daniel");
            var costin = employees.Single(x => x.Firstname == "Costin");
            var ioana = employees.Single(x => x.Firstname == "Ioana");

            return new List<VacationStatus>
            {
                new VacationStatus { Employee = cornel, TotalNumber = 32, Taken = 0, Left = 0, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = mihai, TotalNumber = 28, Taken = 1, Left = 27, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = cosmin, TotalNumber = 25, Taken = 0, Left = 0, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = daniel, TotalNumber = 21, Taken = 0, Left = 21, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = costin, TotalNumber = 21, Taken = 1, Left = 20, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = ioana, TotalNumber = 21, Taken = 2, Left = 19, Paid = 0, Year = 2013, },
            };
        }

        private static List<VacationRequest> BuildVacationRequests(ICollection<Employee> employees)
        {
            var mihai = employees.Single(x => x.Firstname == "Mihai");
            var costin = employees.Single(x => x.Firstname == "Costin");
            var ioana = employees.Single(x => x.Firstname == "Ioana");

            return new List<VacationRequest>
            {
                // mihai's requests
                new VacationRequest 
                { 
                    State = VacationRequestState.Approved, Employee = mihai, CreationDate = January.The1st, 
                    StartDate = January.The3rd, 
                    EndDate = January.The3rd, 
                },
                new VacationRequest 
                { 
                    State = VacationRequestState.Submitted, Employee = mihai, CreationDate = August.The10th, 
                    StartDate = August.The13th, 
                    EndDate = August.The14th, 
                },
                // costin's requests
                new VacationRequest 
                { 
                    State = VacationRequestState.Approved, Employee = costin, CreationDate = January.The3rd, 
                    StartDate = January.The3rd,
                    EndDate = January.The3rd,
                },
                new VacationRequest 
                { 
                    State = VacationRequestState.Submitted, Employee = costin, CreationDate = September.The24th, 
                    StartDate = September.The28th,
                    EndDate = October.The1st,
                },
                // ioana's requests
                new VacationRequest 
                { 
                    State = VacationRequestState.Approved, Employee = ioana, CreationDate = January.The21st, 
                    StartDate = February.The4th, 
                    EndDate = February.The4th,
                },
            };
        }
    }
}