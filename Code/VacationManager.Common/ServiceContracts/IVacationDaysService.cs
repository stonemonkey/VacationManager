using System.ServiceModel;
using VacationManager.Common.DataContracts;

namespace VacationManager.Common.ServiceContracts
{
    [ServiceContract]
    public interface IVacationDaysService
    {
        /// <summary>
        /// Retrives info on consumed/available/payed vacation days
        /// for current (caller) employee.
        /// </summary>
        /// <returns>Instance containing info data.</returns>
        [OperationContract]
        VacationDaysDto GetVacationDays();
    }
}