using System.Collections.Generic;
using System.ServiceModel;
using VacationManager.Common.DataContracts;

namespace VacationManager.Common.ServiceContracts
{
    [ServiceContract]
    public interface IVacationRequestService
    {
        /// <summary>
        /// Creates a new request for the current (caller) employee.
        /// </summary>
        /// <param name="request">Containe partial compleated request
        /// instance.</param>
        /// <returns>Saved request. Service is responsable to complete
        /// fields like reques number, creation date, state etc.</returns>
        [OperationContract]
        VacationRequestDto CreateRequest(VacationRequestDto request);

        /// <summary>
        /// Updates a request state for current (caller) employee.
        /// </summary>
        /// <param name="id">Request number.</param>
        /// <param name="toState">New state for the request.</param>
        [OperationContract]
        void ChangeRequestState(long id, VacationRequestState toState);
        
        /// <summary>
        /// Remove a request for current (caller) employee. Request must
        /// be in submitted state.
        /// </summary>
        /// <param name="id">Request number.</param>
        [OperationContract]
        void DeleteRequest(long id);

        /// <summary>
        /// Retrive requests created by the current (caller) employee, 
        /// no matter of the state.
        /// </summary>
        /// <returns>List of requests.</returns>
        [OperationContract]
        List<VacationRequestDto> GetMyRequests();

        /// <summary>
        /// Retrive other emplyees submitted requests for which current 
        /// employee is responsible to accept or decline.
        /// </summary>
        /// <returns>List of requests.</returns>
        [OperationContract]
        List<VacationRequestDto> GetPendingRequests();
    }
}
