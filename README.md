VacationManager
===============

Application for dealing with vacation days requests, approvals etc ...

To run this project you need:
- MS Visual Studio 2012 (.NET 4.5);
- IIS 8.0 (.NET 4.0);
- MSSQL 2012 or 2008;
- Access to internet for installing third party frameworks/libraries at first build.

Some other considerations:
- You need also to have non-http activation feature installed since project uses net.tcp binding;
- Depending on which user is running ApplicationPool in IIS you need to give this user rights on VacationManager.IISHost folder (read and execute scripts). 
In my configuration I am hosting service under Default Web Site/VacationManager running DefaultAppPool (.NET v4.0, PipelineMode Integrated, Identity NETWORK SERVICE);
- Currently client project is expecting to find service on net.tcp://localhost/VacationManager/Locator.svc. In order to create automaticaly the Virtual Directory
in IIS go to VacationManager.IISHost project properties -> Web -> Use Local IIS Web server (set Project url to whathever suited for you, currently is 
"http://localhost/VacationManager") -> Create Virtual Directory (must have been started Visual Studio with "Run as Administrator");
- Running VacationManagerContext.GenerateSql test will create database. In fact any launch of those tests will recreate VacationManager DB. 
For the moment it is needed to manual set the rights for the user web service is using for reading and writing to the DB.
In my case the IIS AppPool used by Vacation Manager web service is running under NT AUTORITY\NETWORK SERVICE, so this user needs rights for reading and writing to 
the VacationManager database (Membership db_datareader and db_datawriter).