@ECHO OFF

ECHO.
ECHO This script will create new database.
ECHO.
ECHO You'll have to provide SQL database name, SQL database server instance name and the name of the user through which WebService will access database.
ECHO.
ECHO Please note that you have to have Powershell v2 or later installed and appropriate rights (sysadmin) on the server throug WindowsAuthentication to backup, create and drop databases.
ECHO.
ECHO Warning! In case there is already a Database with same name it will be backedup to default SQL Server backup folder and dropped before creating it.
ECHO.

SET _setupScriptName=Misc\setup-db.ps1
IF "%~1" == "" (
	SET _setupScriptName=.\%_setupScriptName%
) ELSE (
	SET _setupScriptName=%~1\%_setupScriptName%
)

IF NOT EXIST "%_setupScriptName%" (
	ECHO "%_setupScriptName%" could not be found!
	PAUSE
	GOTO :eof
)

(SET _yesNo=)
SET /P _yesNo=Do you want to continue [y/N]?
IF /i NOT "%_yesNo%"=="y" (
	ECHO Exit ok.
	GOTO :eof
)

(SET _dbName=)
SET /P _dbName=Database name, default 'VacationManager':
IF "%_dbName%"=="" (SET _dbName=VacationManager)

(SET _dbServerName=)
SET /P _dbServerName=Database server name, default 'localhost': 
IF "%_dbServerName%"=="" (SET _dbServerName=localhost)

(SET _wsUserName=)
SET /P _wsUserName=WebService user name, default 'NT AUTHORITY\NETWORK SERVICE':
IF "%_wsUserName%"=="" (SET _wsUserName=NT AUTHORITY\NETWORK SERVICE)

ECHO.
ECHO Start patching ...
ECHO.

powershell "%_setupScriptName% create -dbServerName '%_dbServerName%' -dbName '%_dbName%' -dbUserName '%_wsUserName%' -backupExistent"

ECHO.
ECHO Finished patching.

PAUSE
