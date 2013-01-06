using System.ServiceModel;
using VacationManager.Common.DataContracts;

namespace VacationManager.Common.ServiceContracts
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        EmployeeDto GetEmployeeById(long id);
    }
}