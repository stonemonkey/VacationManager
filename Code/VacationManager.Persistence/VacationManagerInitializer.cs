using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder.Dates;
using VacationManager.Common.DataContracts;
using VacationManager.Persistence.Model;

namespace VacationManager.Persistence
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

        private static List<VacationStatus> BuildVacationStatuses(List<Employee> employees)
        {
            var ioana = employees.Single(x => x.Firstname == "Ioana");
            var mihai = employees.Single(x => x.Firstname == "Mihai");
            var costin = employees.Single(x => x.Firstname == "Costin");
            var daniel = employees.Single(x => x.Firstname == "Daniel");
            return new List<VacationStatus>
            {
                new VacationStatus { Employee = ioana, TotalNumber = 21, Taken = 0, Left = 21, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = mihai, TotalNumber = 28, Taken = 0, Left = 28, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = costin, TotalNumber = 21, Taken = 4, Left = 17, Paid = 0, Year = 2013, },
                new VacationStatus { Employee = daniel, TotalNumber = 21, Taken = 1, Left = 20, Paid = 0, Year = 2013, },
            };
        }

        private static List<VacationRequest> BuildVacationRequests(IEnumerable<Employee> employees)
        {
            var costin = employees.Single(x => x.Firstname == "Costin");

            return new List<VacationRequest>
            {
                new VacationRequest { State = VacationRequestState.Submitted, Employee = costin, CreationDate = January.The10th, VacationDays = January.The18th.ToString(VacationRequest.VacationDaysFormat) },
                new VacationRequest { State = VacationRequestState.Submitted, Employee = costin, CreationDate = January.The21st, VacationDays = February.The11th.ToString(VacationRequest.VacationDaysFormat) + ";" + February.The12th.ToString(VacationRequest.VacationDaysFormat) + ";" + February.The13th.ToString(VacationRequest.VacationDaysFormat) },
            };
        }
    }
}