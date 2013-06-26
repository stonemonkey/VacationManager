using System;
using System.Diagnostics;
using VacationManager.Common.ServiceContracts;
using Vm.BusinessObjects.Server;

namespace Vm.BusinessObjects.Employees
{
    public partial class Employee
    {
        #region DataPortal_XYZ methods

        protected void DataPortal_Fetch(long id)
        {
            using (var proxy = new ServiceProxy<IEmployeeService>(Configuration.ServiceAddress))
            {
                try
                {
                    var serviceObject = proxy.GetChannel().GetEmployeeById(id);

                    Id = serviceObject.Id;
                    Surname = serviceObject.Surname;
                    Firstname = serviceObject.Firstname;
                    Roles = serviceObject.Roles;
                }
                catch (Exception e)
                {
                    // Things done:
					//	Windows Process Activation Service is running OK
					//	Net.Tcp Listener Adapter service is OK.
					//	Net.Tcp Port Sharing service is OK.
					//	We made net.tcp binding for the web application.
					//	The net.tcp protocol is enabled for the web application. 
                    //  Reinstall Net.Tcp activation.
					//  Remove/add net.tcp on the site with diffrent port: appcmd set site /site.name:"Default Web Site" /+bindings.[protocol='net.tcp',bindingInformation='8011:*']
					// !!! AND STILL HAVING ERROR HERE when using IISHost !!!
                    Debug.WriteLine(e.Message);
                }
            }
        }

        #endregion
    }
}
