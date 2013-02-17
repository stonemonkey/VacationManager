-------------------------------------------------------------------------------------------------
-- Update script from '<x-1>' to '<x>'
-- Any statement that will fail, will cancel the whole transaction. Automatic rollback is issued.
-------------------------------------------------------------------------------------------------

SET XACT_ABORT ON
BEGIN TRANSACTION

-- <VERSION CHECK>
IF NOT EXISTS (SELECT * FROM [dbo].[Versions] WHERE [Name] = 'VacationManager Database' AND [VersionNumber] = '<x-1>')
BEGIN
	IF @@TRANCOUNT > 0 BEGIN
		ROLLBACK TRANSACTION
	END
	raiserror('Incorrect version detected. Expected version is: 0.0.<x-1>. Patch not applied !', 20, -1) with log
END
-- </VERSION CHECK>

-- TODO: 
-- 1) copy and rename file to reflect target version e.g. for <x> = 0.0.2 you will have:
-- Code\VacationManager.Persistence\Updates\updates_00.000\update_00.002.sql
-- 2) replace <x-1> and <x> with relevant versions e.g. <x-1> = 0.0.1 and <x> = 0.0.2
-- 3) add here any db changes, if target version did not have any db changes there is nothing to 
-- add here, but still the file must exist in order to keep db version in sync with service version.
GO

-- <VERSION INCREMENT>
BEGIN TRY
	-- Create first record for publisher version
	UPDATE [dbo].[Versions] SET [VersionNumber] = '<x>' WHERE [Name] = 'VacationManager Database' 
	IF @@TRANCOUNT > 0 BEGIN
		COMMIT TRANSACTION
	END
	PRINT 'Patch <x> applied successfuly !'
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 BEGIN
		ROLLBACK TRANSACTION
	END
	PRINT 'Patch <x> not applied. Errors occured !'
END CATCH
-- </VERSION INCREMENT>