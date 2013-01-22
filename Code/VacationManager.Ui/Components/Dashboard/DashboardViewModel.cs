using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.ApprovedRequests;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Components.PendingRequests;
using VacationManager.Ui.Components.VacationStatus;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.Dashboard
{
    // TODO: maybe this can be a Conductor<IPopulableViewModel>.Collection.AllActive
    public class DashboardViewModel : Screen, IPopulableViewModel
    {
        public static DashboardStrings Localization
        {
            get
            {
                return new DashboardStrings();
            }
        }

        #region External dependencies

        [Inject]
        public IUiService UiService { get; set; }
        
        [Inject]
        public IContextViewModel Context { get; set; }
        
        [Inject]
        public VacationStatusViewModel VacationStatus { get; set; }

        [Inject]
        public PendingRequestsViewModel PendingRequests { get; set; }

        [Inject]
        public ApprovedRequestsViewModel ApprovedRequests { get; set; }

        #endregion

        public DashboardViewModel()
        {
            DisplayName = DashboardStrings.Title;
        }

        public IEnumerable<IResult> Populate()
        {
            yield return Populate(VacationStatus);
            
            if (Context.IsManager)
                yield return Populate(PendingRequests);
            
            if (Context.IsHr)
                yield return Populate(ApprovedRequests);
        }

        private IResult Populate(IPopulableViewModel populableViewModel)
        {
            IResult result;
            
            try
            {
                result = new SequentialResult(populableViewModel.Populate().GetEnumerator());
            }
            catch (Exception ex)
            {
                result = UiService.ShowMessageBox(ex.Message, GlobalStrings.ErrorCaption);
                TryClose();
            }
            
            return result;
        }
    }
}