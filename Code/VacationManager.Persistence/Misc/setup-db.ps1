$errExitCode = 1
$createCommandName = 'create'
$initCommandName = 'init'
$updateCommandName = 'update'
$cleanupCommandName = 'cleanup'

function Usage 
{  
    write-output "Error: Invalid parameter(s)!"
    write-output "`nUsage:`nsetup-db.ps1 <command> -dbServerName <sql_server_instance_name> -dbName <database_name> -dbUserName <name_of_user_used_to_create_db> [-backupExistent]"
    write-output "`nWhere command can be:"
    write-output " � create`t to setup a new database"
    write-output " � init`t to setup an old database, but first delete everything from it."
    write-output " � cleanup`t to delete structure and all objects within existent database without dropping database itself."
    write-output " � update`t to update an old database."
    write-output "`nExample:`n.\setup-db.ps1 create -dbServerName '.' -dbName 'VacationManager' -dbUserName 'NT AUTHORITY\NETWORK SERVICE' -backupExistent`n"
    write-output "`nNote:`nThis script needs to be run by an user having sysadmin rights on sql server for create command and public for the rest of commands with db_owner on database specified.`n"
}

function Test-Argument ([string] $argumentName, [object[]] $arguments)
{
    foreach($a in $arguments)
    {
        if($a.ToLower() -eq $argumentName.ToLower())
        {
            return $true
        }
    }

    return $false
}

function Get-Argument ([string] $argumentName, [object[]] $arguments)
{
    $argumentValue = ""
    $isArgumentValueComing = $false
    
    foreach($a in $arguments)
    {
        if($a.ToLower() -eq $argumentName.ToLower())
        {
            $isArgumentValueComing = $true
        }
        elseif($isArgumentValueComing)
        {
            $argumentValue = $a
            $isArgumentValueComing = $false
        }
    }

    return $argumentValue
}

function Backup-Database ($server, $database)
{
    $serverName = $server.Name
    write-output "`nStart creating backup for database '$databaseName' on '$serverName' ..."
    
    # need SmoExtended for smo.backup
    [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SmoExtended") | out-null
     
    $backupDirectory = $server.Settings.BackupDirectory
    
    # build backup file path
    $databaseName = $database.Name 
    $timeStamp = get-date -format yyyyMMddHHmmss
    $backupFilePath = join-path $backupDirectory ($databaseName + "_" + $timeStamp + ".bak")
    
    # backup database
    $smoBackup = new-object ("Microsoft.SqlServer.Management.Smo.Backup")
    $smoBackup.Action = "Database"
    $smoBackup.BackupSetDescription = "Full Backup of " + $databaseName
    $smoBackup.BackupSetName = $databaseName + " Backup"
    $smoBackup.Database = $databaseName
    $smoBackup.MediaDescription = "Disk"
    $smoBackup.Devices.AddDevice($backupFilePath, "File")
    $smoBackup.SqlBackup($server)

    write-output "Backup location on server: '$backupFilePath'."
    
    return $backupFilePath
}

function Restore-Database ($server, $backupFilePath)
{
    if (test-path -path $backupFilePath -pathtype leaf)
    {
        # need SmoExtended for backup
        [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SmoExtended") | out-null

        $backupDevice = New-Object ("Microsoft.SqlServer.Management.Smo.BackupDeviceItem") ($backupFilePath, "File")
        $smoRestore = new-object("Microsoft.SqlServer.Management.Smo.Restore")

        $smoRestore.NoRecovery = $false;
        $smoRestore.ReplaceDatabase = $true;
        $smoRestore.Action = "Database"
        $smoRestore.PercentCompleteNotification = 10;
        $smoRestore.Devices.Add($backupDevice)
        $smoRestoreDetails = $smoRestore.ReadBackupHeader($server)

        $databaseName = $smoRestoreDetails.Rows[0]["DatabaseName"]
        write-output "Database Name from Backup Header: $databaseName" 

        $smoRestore.Database = $databaseName
        $smoRestore.SqlRestore($server)
    }
    else
    {
        write-output "Nothing to restore $backupFilePath." 
    }
}

function Execute-Script ([Microsoft.SqlServer.Management.SMO.Database] $database, [string] $scriptFilePath)
{
    $sqlContent = (get-content -path $scriptFilePath) -join "`n"
    write-output "Execute-Script($scriptFilePath)"
    $database.ExecuteNonQuery($sqlContent)
}

function Execute-Update ([Microsoft.SqlServer.Management.SMO.Database] $database, [string] $currentVersion, [string] $scriptFilePath)
{
    $match = [regex]::matches($scriptFilePath, "\\update_([0-9]{2}\.[0-9]{3}\.[0-9]{3})[\._]");
    $version = $match[0].Groups[1].Value
    if ($currentVersion -lt $version)
    {
        write-output ""
        $sqlContent = (get-content -path $scriptFilePath) -join "`n"
        write-output "Execute update script '$scriptFilePath'..."
        $database.ExecuteNonQuery($sqlContent)
    }
}

function Add-User ($server, $database, $userName, $backupFilePath)
{
    $databaseName = $database.Name

    # get login for user name
    $login = $server.Logins[$userName]
    if ($login -eq $null) 
    {
        write-output "`nLogin '$userName' not found, try creating it ..."

        $query = "CREATE LOGIN [$userName] FROM WINDOWS WITH DEFAULT_DATABASE = [$databaseName]"
        $database.ExecuteNonQuery($query)
        
        write-output "Ok."
    }

    # replace chars like '\', ' ', '@', '.' with '_'
    $loginName = $userName -replace "\\| |\@\.", "_"
    
    write-output "`nStart creating user '$loginName' for database '$databaseName' ..."

    $query = "CREATE USER $loginName FOR LOGIN [$userName]; EXEC sp_addrolemember @rolename = N'db_datareader', @membername = N'$loginName'; EXEC sp_addrolemember @rolename = N'db_datawriter', @membername = N'$loginName'"
    $database.ExecuteNonQuery($query)
    
    write-output "Ok."
}

function Get-DatabaseVersion ([Microsoft.SqlServer.Management.SMO.Database] $database)
{
    $ds = $database.ExecuteWithResults("SELECT [VersionNumber] FROM [dbo].[Versions] WHERE [Name]='VacationManager Database'")
    $version = $ds.Tables[0].Rows[0][0]
    return $version
}

function Add-ZeroToVersion ([string] $version)
{
    $items = ([regex]::matches($version, "([0-9]+)") | % {$_.value})
    $items[0] = "{0:D2}" -f [int]::Parse($items[0])
    $items[1] = "{0:D3}" -f [int]::Parse($items[1])
    $items[2] = "{0:D3}" -f [int]::Parse($items[2])
    
    return $items -join '.'
}

function OnInfoMessage ($sender, $e)
{
    $msg = $e.Message.ToString()
    write-output $msg
}

function OnServerMessage ($sender, $e)
{
    $msg = $e.Error.ToString()
    write-output $msg
}

$infoMessageHandler = $null

try
{
	# try modify rights to run scripts, this throws exception if not running powershell as Administrator
    #set-executionpolicy remotesigned

    # get script parameter values
    $cmdName = $args[0];
    $dbName = Get-Argument "-dbName" $args
    $dbServerName = Get-Argument "-dbServerName" $args
    $dbUserName = Get-Argument "-dbUserName" $args
    $backupExistent = Test-Argument "-backupExistent" $args

    # show script parameter values
    write-output "Command name: '$cmdName'"
    write-output "Database name: '$dbName'"
    write-output "Database server: '$dbServerName'"
    write-output "Database user name (only for create): '$dbUserName'"
    write-output "Backup existent (optional): '$backupExistent'"

    # check required combination of parameters (if one optional is set all must be present)
    if(($cmdName -eq $null) -or (($cmdName -ne $createCommandName) -and ($cmdName -ne $initCommandName) -and ($cmdName -ne $updateCommandName) -and ($cmdName -ne $cleanupCommandName)) -or `
       ($dbServerName -eq $null) -or ($dbServerName -eq "") -or `
       ($dbName -eq $null) -or ($dbName -eq "") -or `
       (($cmdName -eq $createCommandName) -and (($dbUserName -eq $null) -or ($dbUserName -eq ""))))
    {
        Usage
        exit $errExitCode
    }
    
    # connect to server
    [System.Reflection.Assembly]::LoadWithPartialName('System.Data') | out-null
    [System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SMO') | out-null
    [System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.ConnectionInfo') | out-null
    $server = new-object ('Microsoft.SqlServer.Management.Smo.Server') $dbServerName

    $database = $server.Databases[$dbName]
    $backupFilePath = ""
    
    # check if database exists
    if ($database -ne $null)
    {
        if (($cmdName -eq $createCommandName) -and -not $backupExistent)
        {
            write-output "`nExiting because database exist on specified server and you choose NOT to backup it!"
            exit $errExitCode
        }
        
        if ($backupExistent)
        {		
            # backup existent database
            # TODO: this is voodo can not call function Backup-Database !!!
            #$backupFilePath = Backup-Database $server $database
            write-output "`nStart creating backup for database '$dbName' on '$dbServerName' ..."
            
            # need SmoExtended for smo.backup
            [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SmoExtended") | out-null
             
            $backupDirectory = $server.Settings.BackupDirectory
            
            # build backup file path
            $timeStamp = get-date -format yyyyMMddHHmmss
            $backupFilePath = join-path $backupDirectory ($dbName + "_" + $timeStamp + ".bak")
            
            # backup database
            $smoBackup = new-object ("Microsoft.SqlServer.Management.Smo.Backup")
            $smoBackup.Action = "Database"
            $smoBackup.BackupSetDescription = "Full Backup of " + $dbName
            $smoBackup.BackupSetName = $dbName + " Backup"
            $smoBackup.Database = $dbName
            $smoBackup.MediaDescription = "Disk"
            $smoBackup.Devices.AddDevice($backupFilePath, "File")
            $smoBackup.SqlBackup($server)

            write-output "Backup location on server: '$backupFilePath'."
        }
    
        if ($cmdName -eq $createCommandName)
        {
            write-output "Delete database '$dbName'..."
            # drop existent database
            $database.ExecuteNonQuery("ALTER DATABASE $dbName SET SINGLE_USER with ROLLBACK IMMEDIATE");
            $dataBase.Drop()
        }
    }
    elseif (($cmdName -eq $initCommandName) -or ($cmdName -eq $updateCommandName))
    {
        write-output "`nExiting because database '$dbName' does not exist on specified server!"
        exit $errExitCode
    }
    
    $sqlContent = '';
    $currentVersion = '00.000.000';
    if ($cmdName -eq $createCommandName)
    {
        # create database
        $database = new-object ('Microsoft.SqlServer.Management.SMO.Database') $server, $dbName
        $database.Create()
    
        Add-User $server $database $dbUserName $backupFilePath
    
        write-output "`nDatabase '$dbName' was successfully created on '$dbServerName'."
        $database = $server.Databases[$dbName];       
    }
    elseif ($database -eq $null)
    {
        write-output "`nDatabase '$dbName' does not exists on '$dbServerName'."
        exit $errExitCode
    }
    $infoMessageHandler = [System.Data.SqlClient.SqlInfoMessageEventHandler] { Write-Host $_.Message }
    $server.ConnectionContext.add_InfoMessage($infoMessageHandler);
    if (($cmdName -eq $initCommandName) -or ($cmdName -eq $cleanupCommandName))
    {
        Execute-Script $database 'Misc/CleanupDatabase.sql'
    }
    elseif ($cmdName -eq $updateCommandName)
    {
        $currentVersion = Get-DatabaseVersion($database);
        write-output "`Update database '$dbName' from version '$currentVersion'."
        $currentVersion =  Add-ZeroToVersion $currentVersion;
    }
    if ($cmdName -ne $cleanupCommandName)
    {
		gci "./Updates" -recurse -Include 'update_*.sql' | sort | foreach { Execute-Update $database $currentVersion $_ }
        gci "./Jobs" -recurse -Include '*.job.sql' | sort | foreach { Execute-Script $database "$_" }
    }
    $server.ConnectionContext.remove_InfoMessage($infoMessageHandler);
}
catch
{
	write-output "`nExiting with error because of exception: $_"
	# this does not work when a duplicate item is found in db, until we find a solution keep commented
	#$_ | format-list * -force # this shows full info on exception
	
    if ($infoMessageHandler) { $server.ConnectionContext.remove_InfoMessage($infoMessageHandler); }
    exit $errExitCode
}
