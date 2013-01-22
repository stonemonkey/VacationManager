using System.ServiceModel;
using VacationManager.Common.DataContracts;

namespace VacationManager.Common.ServiceContracts
{
    [ServiceContract]
    public interface IVacationStatusService
    {
        [OperationContract]
        VacationStatusDto GetVacationStatusByEmployeeId(long employeeId);
    }
}