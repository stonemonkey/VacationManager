using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using Caliburn.Micro;
using Ninject;
using Ninject.Extensions.Conventions;
using VacationManager.Ui.Components.Shell;

namespace VacationManager.Ui
{
    public class NInjectBootstrapper : Bootstrapper<IShellViewModel>
    {
        private IKernel _kernel;

        protected override void Configure()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<IWindowManager>()
                .To<WindowManager>().InSingletonScope();
            
            _kernel.Bind<IEventAggregator>()
                .To<EventAggregator>().InSingletonScope();

            _kernel.Bind<IShellViewModel>()
                .To<ShellViewModel>().InSingletonScope();

            // scan services
            _kernel.Bind(scanner => scanner
                .FromAssemblyContaining<IShellViewModel>()
                .Select(IsServiceType)
                .BindDefaultInterface()
                .Configure(binding => binding.InSingletonScope()));

            // scan view models
            _kernel.Bind(scanner => scanner
                .FromAssemblyContaining<IShellViewModel>()
                .Select(IsViewModelType)
                .BindDefaultInterface());

            ConventionManager.AddElementConvention<DatePicker>(DatePicker.SelectedDateProperty, "SelectedDate", "Loaded");
        }

        private static bool IsViewModelType(Type type)
        {
            return !type.Name.EndsWith("ShellViewModel") && !type.Name.EndsWith("Service");
        }

        private static bool IsServiceType(Type type)
        {
            return type.Name.EndsWith("Service");
        }

        protected override object GetInstance(Type service, string key)
        {
            return _kernel.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                Assembly.GetExecutingAssembly(),    // this is Shell assembly
            };
        }
    }
} 
 