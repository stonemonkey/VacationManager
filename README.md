VacationManager
===============

Application for dealing with vacation days requests, approvals etc ...

To run this project you need:
- MS Visual Studio 2012 (.NET 4.5);
- IIS 8.0 (.NET 4.0);
- Access to internet for installing third party frameworks/libraries at first build.

Some other considerations:
- You need also to have non-http activation feature installed since project uses net.tcp binding;
- Depending on which user is running ApplicationPool in IIS you need to give this user rights on VacationManager.IISHost folder (read and execute scripts). 
In my configuration I am hosting service under Default Web Site/VacationManager running DefaultAppPool (.NET v4.0, PipelineMode Integrated, Identity NETWORK SERVICE);
- Currently client project is expecting to find service on net.tcp://localhost/VacationManager/Locator.svc. In order to create automaticaly the Virtual Directory
in IIS go to VacationManager.IISHost project properties -> Web -> Use Local IIS Web server (set Project url to whathever suited for you, currently is 
"http://localhost/VacationManager") -> Create Virtual Directory (must have been started Visual Studio with "Run as Administrator").