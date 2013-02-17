@ECHO OFF

ECHO.
ECHO This script will update an empty Database to an initial state.
ECHO.
ECHO You'll have to provide database name and SQL database server instance name.
ECHO.
ECHO Please note that you have to have Powershell v2 or later installed and appropriate rights (public) on the server throug WindowsAuthentication and on database (db_owner) to create and drop database objects.
ECHO.
ECHO Warning! All existing data from Database and its structure will be erased and recreated. Before doing that existent database will be backedup to default SQL Server backup folder.
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

ECHO.
ECHO Start patching ...
ECHO.

powershell "%_setupScriptName% init -dbServerName '%_dbServerName%' -dbName '%_dbName%' -backupExistent"

ECHO.
ECHO Finished patching.

PAUSE
