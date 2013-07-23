using System.Collections.Generic;
using System.Linq;
using Common.Model;
using Csla;
using Persistence;

namespace BusinessObjects.Employees
{
    public partial class EmployeeInfoList
    {
        protected void DataPortal_Fetch()
        {
            DataPortal_Fetch(null);
        }

        protected void DataPortal_Fetch(EmployeeSearchCriteria criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            
            using (var ctx = new VacationManagerContext())
            {
                IList<Persistence.Model.EmployeeEntity> employees;

                if (criteria == null)
                    employees = ctx.Employees
                        .ToList();
                else
                    employees = ctx.Employees
                        .Where(x => (x.Firstname == criteria.FirstName) || 
                                    (x.LastName == criteria.LastName))
                        .ToList();

                foreach (var item in employees)
                    Add(DataPortal.Create<Employee>(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
    }
}