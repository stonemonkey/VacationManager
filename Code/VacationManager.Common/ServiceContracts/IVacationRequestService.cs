using System.Collections.Generic;
using System.ServiceModel;
using VacationManager.Common.DataContracts;

namespace VacationManager.Common.ServiceContracts
{
    [ServiceContract]
    public interface IVacationRequestService
    {
        [OperationContract]
        VacationRequestDto CreateRequest(VacationRequestDto request);

        [OperationContract]
        void ChangeRequestState(long id, VacationRequestState toState);
        
        [OperationContract]
        void DeleteRequest(long id);

        [OperationContract]
        List<VacationRequestDto> SearchRequests(VacationRequestSearchCriteriaDto criteria);
    }
}
