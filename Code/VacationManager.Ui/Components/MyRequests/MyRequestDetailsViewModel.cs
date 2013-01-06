using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Ninject;
using VacationManager.Ui.BusinessObjects;
using VacationManager.Ui.Components.Context;
using VacationManager.Ui.Resources;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.MyRequests
{
    public class MyRequestDetailsViewModel : Screen
    {
        private VacationRequest _item;

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
                NotifyOfPropertyChange(() => Item);
                NotifyOfPropertyChange(() => CanSaveRequest);
                NotifyOfPropertyChange(() => CanCancelRequest);
            }
        }

        public bool CanSaveRequest
        {
            get { return Item.IsNew; }
        }

        public bool CanCancelRequest
        {
            get { return !CanSaveRequest; }
        }

        #endregion

        public MyRequestDetailsViewModel()
        {
            DisplayName = MyRequestDetailsStrings.Title;
        }

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

            if (ReferenceEquals(null, result.Error))
                Item = result.Result;
            else
                yield return UiService.ShowMessageBox(result.Error.Message, GlobalStrings.ErrorCaption);
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

        public void UpdateSelectedDates(IList<DateTime> selectedDates)
        {
            // FIX ME: this is an ugly way of doing "binding" to Calendar.SelectedDates
            // which is not a dependency property. This is only one way (VM->V) => for
            // creating new requests is enaugh but not for editing existent ones.
            Item.VacationDays.Clear();
            Item.VacationDays.AddRange(selectedDates);

            NotifyOfPropertyChange(() => Item);
        }

        #endregion

        private bool IsUserOkToCloseWithoutSaving()
        {
            var result = UiService.ShowMessageBox(
                MyRequestDetailsStrings.CloseWhenUnsavedMesage, 
                GlobalStrings.QuestionCaption, MessageBoxButton.OKCancel);

            result.Execute();

            return (result.Result == MessageBoxResult.OK);
        }
    }
}