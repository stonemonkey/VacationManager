using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;
using Common.Model;
using VacationManager.Ui.Components.ApprovedRequests;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Components.EmployeeSituation;
using VacationManager.Ui.Components.PendingRequests;
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
        public IContextViewModel Context { get; set; }
        
        [Inject]
        public EmployeeSituationViewModel EmployeeSituation { get; set; }

        [Inject]
        public PendingRequestsViewModel PendingRequests { get; set; }

        [Inject]
        public ApprovedRequestsViewModel ApprovedRequests { get; set; }

        #endregion

        // TODO: should be moved somewhere else
        public bool IsManager
        {
            get { return Csla.ApplicationContext.User.IsInRole(EmployeeRoles.Manager.ToString()); }
        }

        // TODO: should be moved somewhere else
        public bool IsHr
        {
            get { return Csla.ApplicationContext.User.IsInRole(EmployeeRoles.Hr.ToString()); }
        }

        public DashboardViewModel()
        {
            DisplayName = DashboardStrings.Title + " " + Csla.ApplicationContext.User.Identity.Name;
        }

        public IEnumerable<IResult> Populate()
        {
            yield return Populate(EmployeeSituation);
            
            if (IsManager)
                yield return Populate(PendingRequests);
            
            if (IsHr)
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