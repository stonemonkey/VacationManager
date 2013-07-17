using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;
using Vm.BusinessObjects.VacationRequests;
using Calendar = System.Windows.Controls.Calendar;

namespace VacationManager.Ui.Components.MyRequests
{
    public class MyRequestDetailsViewModel : Screen
    {
        private VacationRequest _item;
        private bool _useStartEndDateSelecetion;

        public static MyRequestDetailsStrings Localization
        {
            get
            {
                return new MyRequestDetailsStrings();
            }
        }

        #region External dependencies

        [Inject]
        public IUiService UiService { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IContextViewModel Context { get; set; }

        #endregion

        #region Binding properties

        public VacationRequest Item
        {
            get { return _item; }
            set
            {
                _item = value;

                UpdateTitle();

                NotifyOfPropertyChange(() => Item);
                NotifyOfPropertyChange(() => CanSaveRequest);
                NotifyOfPropertyChange(() => CanCancelRequest);
            }
        }

        public bool UseStartEndDateSelection {
            get { return _useStartEndDateSelecetion; }
            set
            {
                _useStartEndDateSelecetion = value; 

                NotifyOfPropertyChange(() => IsCalendarAvailableForSelection);
            }
        }

        public bool IsCalendarAvailableForSelection
        {
            get { return Item.IsDirty && !UseStartEndDateSelection; }
        }

        public bool CanSaveRequest
        {
            get { return Item.IsDirty; }
        }

        public bool CanCancelRequest
        {
            get { return !CanSaveRequest; }
        }

        #endregion

        #region Actions

        public override void CanClose(Action<bool> callback)
        {
            if (Item.IsDirty)
                callback(IsUserOkToCloseWithoutSaving());
            else
                base.CanClose(callback);
        }

        public IEnumerable<IResult> SaveRequest()
        {
            yield return UiService.ShowBusy();

            _item.EmployeeId = Context.CurrentEmployee.Id;
            var result = DataService.Update(_item);
            yield return result;

            yield return UiService.HideBusy();

            if (result.Error == null)
            {
                Item = result.Result;
                UpdateTitle();
            }
            else
            {
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
            }
        }

        public IEnumerable<IResult> CancelRequest()
        {
            yield return UiService.ShowBusy();

            var result = DataService.Delete(Item, x => x.RequestNumber);
            yield return result;

            yield return UiService.HideBusy();

            if (ReferenceEquals(null, result.Error))
                TryClose();
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
        }

        //==============================================================================================
        // TODO: FIX ME! This is an ugly way of doing "binding" to Calendar.SelectedDates which is 
        // not a dependency property. This is only one way (VM->V) => for creating new requests is 
        // enaugh but not for editing existent ones.
        public void UpdateModelSelectedDates(Calendar calendar)
        {
            if (UseStartEndDateSelection || 
                (calendar == null) || (calendar.SelectedDates == null) || !calendar.SelectedDates.Any())
                return;

            Item.StartDate = calendar.SelectedDates.Min();
            Item.EndDate = calendar.SelectedDates.Max();

            NotifyOfPropertyChange(() => Item);

            // This forces calendar to release mouse in order to be able to click on other things 
            // only once after you selected some dates in calendar not twice.
            calendar.CaptureMouse();
            calendar.ReleaseMouseCapture();
        }

        public void UpdateViewSelectedDates(Calendar calendar)
        {
            if ((calendar == null) || (calendar.SelectedDates == null))
                return;

            // That is for forcing Start and EndDate to be updated before calculating the period.
            NotifyOfPropertyChange(() => Item);

            var numberOfDays = ((Item.EndDate - Item.StartDate).Days + 1);
            if (numberOfDays > 0)
                calendar.SelectedDates.AddRange(Item.StartDate, Item.EndDate);    
        }
        //==============================================================================================

        #endregion

        private bool IsUserOkToCloseWithoutSaving()
        {
            var result = UiService.ShowWindowsMessageBox(
                MyRequestDetailsStrings.CloseWhenUnsavedMesage, 
                GlobalStrings.QuestionCaption, MessageBoxButton.OKCancel);

            result.Execute();

            return (result.Result == MessageBoxResult.OK);
        }

        private void UpdateTitle()
        {
            var dirty = string.Empty;
            if (Item.IsDirty)
                dirty = MyRequestDetailsStrings.DirtyTitlePartText;

            var requestNumber = string.Empty;
            if (!Item.IsNew)
                requestNumber = Item.RequestNumber.ToString(CultureInfo.InvariantCulture);

            DisplayName = DisplayName = string.Format(MyRequestDetailsStrings.Title, requestNumber, dirty);
        }
    }
}