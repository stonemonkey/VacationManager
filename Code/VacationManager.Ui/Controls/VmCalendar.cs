//using System;
//using System.Collections.Generic;
//using System.Windows;
//using System.Windows.Controls;

//namespace VacationManager.Ui.Controls
//{
//    // Not used and not ready at this moment.
//    // With this I would like to try solving MVVM issue related the binding to Calnedar.SelectedDates.
//    public class VmCalendar : Calendar
//    {
//        public static readonly DependencyProperty BindableSelectedDatesProperty =
//        DependencyProperty.Register("BindableSelectedDates", typeof(List<DateTime>), typeof(VmCalendar));
//        public List<DateTime> BindableSelectedDates
//        {
//            get
//            {
//                return GetValue(BindableSelectedDatesProperty) as List<DateTime>;
//            }
//            set
//            {
//                SetValue(BindableSelectedDatesProperty, value);
//            }
//        }
//    }
//}