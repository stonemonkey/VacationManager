using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Xceed.Wpf.Toolkit;

namespace VacationManager.Ui.Results
{
    public class BusyResult : IResult
    {
        private readonly bool _isBusy;
        private readonly string _message;
        private readonly string _busyIndicatorName;

        private const string DefaultBusyIndicatorName = "BusyIndicator";

        public BusyResult(bool isBusy, string message = null, string busyIndicatorName = null)
        {
            _isBusy = isBusy;
            _message = message;
            _busyIndicatorName = string.IsNullOrEmpty(busyIndicatorName) ?
                DefaultBusyIndicatorName : busyIndicatorName;
        }

        public void Execute(ActionExecutionContext context)
        {
            FrameworkElement view = GetView(context);

            while (view != null)
            {
                var busyIndicator = //view.FindName(_busyIndicatorName) as BusyIndicator ??
                    FindChild<BusyIndicator>(view, _busyIndicatorName);

                if (busyIndicator != null)
                {
                    busyIndicator.IsBusy = _isBusy;

                    if (!string.IsNullOrWhiteSpace(_message))
                        busyIndicator.BusyContent = _message;

                    break;
                }

                view = view.Parent as FrameworkElement;
            }

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        public static BusyResult Show(string message = null)
        {
            return new BusyResult(true, message);
        }

        public static BusyResult Hide()
        {
            return new BusyResult(false);
        }

        private static FrameworkElement GetView(ActionExecutionContext context)
        {
            FrameworkElement view = null;

            if (context.View == null)
            {
                var viewAware = context.Target as IViewAware;

                if (viewAware != null)
                    view = viewAware.GetView() as FrameworkElement;
            }
            else
            {
                view = context.View as FrameworkElement;
            }

            return view;
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child.</param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null parent is being returned.</returns>
        private static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                var childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }
}