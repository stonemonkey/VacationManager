using System.ServiceModel;
using VacationManager.Common.DataContracts;

namespace VacationManager.Common.ServiceContracts
{
    [ServiceContract]
    public interface IVacationDaysService
    {
        [OperationContract]
        VacationDaysDto GetVacationDaysByEmployeeId(long employeeId);
    }
}