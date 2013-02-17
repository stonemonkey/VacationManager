VacationManager
===============

Application for dealing with vacation days requests, approvals etc ...

To run this project you need:
- MS Visual Studio 2012 (.NET 4.5);
- IIS or IISExpress 8.0 (.NET 4.0);
- MSSQL 2008 or 2012 (Express versions can not be configured for mail notifications/Database Mail, if you do not need this feature Express is a good choice);
- Access to internet for installing third party frameworks/libraries at first build.

Some other considerations:

- You need also to have non-http activation feature installed since project uses net.tcp binding;

- Depending on which user is running ApplicationPool in IIS you need to give this user rights on VacationManager.IISHost folder (read and execute scripts). 
In my configuration I am hosting service under Default Web Site/VacationManager running DefaultAppPool (.NET v4.0, PipelineMode Integrated, Identity NETWORK SERVICE);

- Currently client project is expecting to find service on net.tcp://localhost/VacationManager/Locator.svc. In order to create automaticaly the Virtual Directory
in IIS go to VacationManager.IISHost project properties -> Web -> Use Local IIS Web server (set Project url to whathever suited for you, currently is 
"http://localhost/VacationManager") -> Create Virtual Directory (must have been started Visual Studio with "Run as Administrator");

- In order to prepare a database to be used by the service you can use one of the following scripts: createDatabase.bat, initDatabase.bat, updateDatabase.bat from 
Code\VacationManager.Persistence. Create is intended to be use when you do not have a database or you want to create brand new one (you need sys_admin rights on the 
SQL server for this). Initialize is intended to be used when you have a database on the SQL server (you do not have sys_admin rights still you have to be db_owner on
the database) and you want to delete all objects inside existent database and recreate them. Update is intended to be used when you have a database filled with data
and you need just to update it to the last version.

- To populate database with some test data you can run Code\VacationManager.Persistence\Updates\_test_data.sql script. This is not needed for running Persistence.Tests
since Test project is recreating a separated test database on every launch.