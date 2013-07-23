using System.Collections.Generic;
using System.Linq;
using Common.Model;
using FizzWare.NBuilder.Dates;
using Persistence.Model;

namespace Persistence.Tests
{
    public static class TestDataBuilder
    {
        public static List<EmployeeEntity> BuildEmployees()
        {
            var cornelBrody = new EmployeeEntity
            {
                Firstname = "Cornel",
                LastName = "Brody",
                BirthDate = The.Year(1956).On.May.The10th,
                HireDate = The.Year(1999).On.February.The15th,
                Roles = EmployeeRoles.Manager,
                Email = "cornel.brody@contoso.com",
                Manager = null
            };
            var mihaiBarabas = new EmployeeEntity
            {
                Firstname = "Mihai",
                LastName = "Barabas",
                BirthDate = The.Year(1978).On.July.The7th,
                HireDate = The.Year(2004).On.August.The21st,
                Roles = EmployeeRoles.Manager | EmployeeRoles.Executive,
                Email = "mihai.barabas@contoso.com",
                Manager = cornelBrody
            };
            var cosminMolnar = new EmployeeEntity
            {
                Firstname = "Cosmin",
                LastName = "Molnar",
                BirthDate = The.Year(1975).On.January.The24th,
                HireDate = The.Year(2012).On.May.The13th,
                Roles = EmployeeRoles.Manager | EmployeeRoles.Executive,
                Email = "cosmin.molnar@contoso.com",
                Manager = cornelBrody
            };

            return new List<EmployeeEntity>
            {
                cornelBrody, mihaiBarabas, cosminMolnar,
                new EmployeeEntity
                {
                    Firstname = "Hr", 
                    LastName = "Generic", 
                    BirthDate = The.Year(1900).On.January.The1st,
                    HireDate = The.Year(2012).On.May.The13th,
                    Roles = EmployeeRoles.Hr, 
                    Email = "hr_holidays@contoso.com", 
                    Manager = null,
                },
                new EmployeeEntity
                {
                    Firstname = "Daniel", 
                    LastName = "Savu", 
                    BirthDate = The.Year(1980).On.December.The12th,
                    HireDate = The.Year(2012).On.May.The13th,
                    Roles = EmployeeRoles.Executive, 
                    Email = "daniel.savu@contoso.com", 
                    Manager = mihaiBarabas,
                },
                new EmployeeEntity
                {
                    Firstname = "Costin", 
                    LastName = "Morariu", 
                    BirthDate = The.Year(1977).On.July.The4th,
                    HireDate = The.Year(2012).On.May.The13th,
                    Roles = EmployeeRoles.Executive, 
                    Email = "costin.morariu@contoso.com", 
                    Manager = mihaiBarabas,
                },
                new EmployeeEntity
                {
                    Firstname = "Ioana", 
                    LastName = "Sandu",  
                    BirthDate = The.Year(1983).On.March.The20th,
                    HireDate = The.Year(2012).On.May.The13th,                    
                    Roles = EmployeeRoles.Hr | EmployeeRoles.Executive, 
                    Email = "ioana.sandu@contoso.com", 
                    Manager = cosminMolnar,
                },
            };
        }

        public static void BuildEmployeeSituations(ICollection<EmployeeEntity> employees)
        {
            var cornel = employees.Single(x => x.Firstname == "Cornel");
            cornel.Situation = new EmployeeSituationEntity
            {
                Employee = cornel,
                ConsumedDays = 0,
                AvailableDays = 0,
                PaidDays = 0,
                Year = 2013,
            };
            
            var mihai = employees.Single(x => x.Firstname == "Mihai");
            mihai.Situation = new EmployeeSituationEntity
            {
                Employee = mihai,
                ConsumedDays = 1,
                AvailableDays = 27,
                PaidDays = 0,
                Year = 2013,
            };
            
            var cosmin = employees.Single(x => x.Firstname == "Cosmin");
            cosmin.Situation = new EmployeeSituationEntity
            {
                Employee = cosmin,
                ConsumedDays = 0,
                AvailableDays = 0,
                PaidDays = 0,
                Year = 2013,
            };

            var daniel = employees.Single(x => x.Firstname == "Daniel");
            daniel.Situation = new EmployeeSituationEntity
            {
                Employee = daniel,
                ConsumedDays = 0,
                AvailableDays = 21,
                PaidDays = 0,
                Year = 2013,
            };

            var costin = employees.Single(x => x.Firstname == "Costin");
            costin.Situation = new EmployeeSituationEntity
            {
                Employee = costin,
                ConsumedDays = 1,
                AvailableDays = 20,
                PaidDays = 0,
                Year = 2013,
            };

            var ioana = employees.Single(x => x.Firstname == "Ioana");
            ioana.Situation = new EmployeeSituationEntity
            {
                Employee = ioana,
                ConsumedDays = 2,
                AvailableDays = 19,
                PaidDays = 0,
                Year = 2013,
            };
        }

        public static List<VacationRequestEntity> BuildVacationRequests(ICollection<EmployeeEntity> employees)
        {
            var mihai = employees.Single(x => x.Firstname == "Mihai");
            var costin = employees.Single(x => x.Firstname == "Costin");
            var ioana = employees.Single(x => x.Firstname == "Ioana");

            return new List<VacationRequestEntity>
            {
                // mihai's requests
                new VacationRequestEntity 
                { 
                    State = VacationRequestState.Approved, 
                    Employee = mihai, 
                    CreationDate = StaticMonth<January>.The1st, 
                    StartDate = StaticMonth<January>.The3rd, 
                    EndDate = StaticMonth<January>.The3rd, 
                },
                new VacationRequestEntity 
                { 
                    State = VacationRequestState.Submitted, 
                    Employee = mihai, 
                    CreationDate = StaticMonth<August>.The10th, 
                    StartDate = StaticMonth<August>.The13th, 
                    EndDate = StaticMonth<August>.The14th, 
                },
                // costin's requests
                new VacationRequestEntity 
                { 
                    State = VacationRequestState.Approved, 
                    Employee = costin, 
                    CreationDate = StaticMonth<January>.The3rd, 
                    StartDate = StaticMonth<January>.The3rd,
                    EndDate = StaticMonth<January>.The3rd,
                },
                new VacationRequestEntity 
                { 
                    State = VacationRequestState.Submitted, 
                    Employee = costin, 
                    CreationDate = StaticMonth<September>.The24th, 
                    StartDate = StaticMonth<September>.The28th,
                    EndDate = StaticMonth<October>.The1st,
                },
                // ioana's requests
                new VacationRequestEntity 
                { 
                    State = VacationRequestState.Approved, 
                    Employee = ioana, 
                    CreationDate = StaticMonth<January>.The21st, 
                    StartDate = StaticMonth<February>.The4th, 
                    EndDate = StaticMonth<February>.The4th,
                },
            };
        }
    }
}