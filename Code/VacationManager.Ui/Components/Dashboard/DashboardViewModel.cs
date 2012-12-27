using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.PendingRequests;
using VacationManager.Ui.Components.VacationDays;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.Dashboard
{
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
        public VacationDaysViewModel VacationDays { get; set; }
        
        [Inject]
        public PendingRequestsViewModel PendingRequests { get; set; }

        #endregion

        public DashboardViewModel()
        {
            DisplayName = DashboardStrings.Title;
        }

        public IEnumerable<IResult> Populate()
        {
            yield return Populate(VacationDays);
            yield return Populate(PendingRequests);
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