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
            var cornelBrody = new Employee { Firstname = "Cornel", Surname = "Brody", Roles = EmployeeRoles.Manager, EmailAddress = "cornel.brody@iquestgroup.com", Manager = null };
            var mihaiBarabas = new Employee { Firstname = "Mihai", Surname = "Barabas", Roles = EmployeeRoles.Manager | EmployeeRoles.Executive, EmailAddress = "mihai.barabas@iquestgroup.com", Manager = cornelBrody };
            var cosminMolnar = new Employee { Firstname = "Cosmin", Surname = "Molnar", Roles = EmployeeRoles.Manager | EmployeeRoles.Executive, EmailAddress = "cosmin.molnar@iquestgroup.com", Manager = cornelBrody };

            return new List<Employee>
            {
                cornelBrody, mihaiBarabas, cosminMolnar,
                new Employee { Firstname = "Hr", Surname = "Generic", Roles = EmployeeRoles.Hr, EmailAddress = "hr_holidays@iquestgroup.com", Manager = null, },
                new Employee { Firstname = "Daniel", Surname = "Savu", Roles = EmployeeRoles.Executive, EmailAddress = "daniel.savu@iquestgroup.com", Manager = mihaiBarabas, },
                new Employee { Firstname = "Costin", Surname = "Morariu", Roles = EmployeeRoles.Executive, EmailAddress = "costin.morariu@iquestgroup.com", Manager = mihaiBarabas, },
                new Employee { Firstname = "Ioana", Surname = "Sandu", Roles = EmployeeRoles.Hr | EmployeeRoles.Executive, EmailAddress = "ioana.sandu@iquestgroup.com", Manager = cosminMolnar, },
            };
        }

        private static List<VacationStatus> BuildVacationStatuses(IList<Employee> employees)
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

        private static List<VacationRequest> BuildVacationRequests(IList<Employee> employees)
        {
            var mihai = employees.Single(x => x.Firstname == "Mihai");
            var costin = employees.Single(x => x.Firstname == "Costin");
            var ioana = employees.Single(x => x.Firstname == "Ioana");

            return new List<VacationRequest>
            {
                // mihai's requests
                new VacationRequest { State = VacationRequestState.Approved, Employee = mihai, CreationDate = The.Year(2012).On.December.The21st, VacationDays = 
                    January.The3rd.ToString(VacationRequest.VacationDaysFormat) },
                new VacationRequest { State = VacationRequestState.Submitted, Employee = mihai, CreationDate = January.The10th, VacationDays = 
                    January.The13th.ToString(VacationRequest.VacationDaysFormat) + ";" + 
                    January.The14th.ToString(VacationRequest.VacationDaysFormat) },
                // costin's requests
                new VacationRequest { State = VacationRequestState.Approved, Employee = costin, CreationDate = January.The3rd, VacationDays = 
                    January.The3rd.ToString(VacationRequest.VacationDaysFormat) },
                new VacationRequest { State = VacationRequestState.Submitted, Employee = costin, CreationDate = January.The24th, VacationDays = 
                    January.The28th.ToString(VacationRequest.VacationDaysFormat) + ";" + 
                    January.The29th.ToString(VacationRequest.VacationDaysFormat) + ";" + 
                    January.The30th.ToString(VacationRequest.VacationDaysFormat) + ";" + 
                    January.The31st.ToString(VacationRequest.VacationDaysFormat) + ";" + 
                    February.The1st.ToString(VacationRequest.VacationDaysFormat) },
                // ioana's requests
                new VacationRequest { State = VacationRequestState.Approved, Employee = ioana, CreationDate = The.Year(2012).On.December.The21st, VacationDays = 
                    January.The4th.ToString(VacationRequest.VacationDaysFormat) + ";" + 
                    January.The3rd.ToString(VacationRequest.VacationDaysFormat) },
            };
        }
    }
}