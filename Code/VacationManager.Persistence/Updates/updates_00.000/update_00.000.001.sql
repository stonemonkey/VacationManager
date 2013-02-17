-------------------------------------------------------------------------------------------------
-- Install script for '0.0.1'
-- Any statement that will fail, will cancel the whole transaction. Automatic rollback is issued.
-------------------------------------------------------------------------------------------------

SET XACT_ABORT ON
BEGIN TRANSACTION

-- <VERSION CHECK>
IF OBJECT_ID('dbo.Versions', 'U') IS NOT NULL
BEGIN
	IF @@TRANCOUNT > 0 BEGIN
		ROLLBACK TRANSACTION
	END
	raiserror('Incorrect database detected. Expected an empty database. Patch not applied !', 20, -1) with log
END
-- </VERSION CHECK>
GO

create table [dbo].[Versions](
	[Name] [nvarchar](200) not null,
	[VersionNumber] [nvarchar](10) not null,
	[CreationDate] [datetime] NOT NULL,
);
create table [dbo].[Employees] (
    [Id] [bigint] not null identity,
    [Surname] [nvarchar](max) null,
    [Firstname] [nvarchar](max) null,
    [EmailAddress] [nvarchar](max) null,
    [Roles] [int] not null,
    [Manager_Id] [bigint] null,
    primary key ([Id])
);
create table [dbo].[VacationRequests] (
    [RequestNumber] [bigint] not null identity,
    [CreationDate] [datetime] not null,
    [State] [int] not null,
    [VacationDays] [nvarchar](max) null,
    [Employee_Id] [bigint] not null,
    primary key ([RequestNumber])
);
create table [dbo].[VacationStatus] (
    [Id] [bigint] not null identity,
    [Year] [int] not null,
    [Paid] [int] not null,
    [Left] [int] not null,
    [Taken] [int] not null,
    [TotalNumber] [int] not null,
    [Employee_Id] [bigint] not null,
    primary key ([Id])
);

alter table [dbo].[Versions] add constraint [DF_Versions_CreationDate]  default (getutcdate()) for [CreationDate]
alter table [dbo].[Employees] add constraint [Employee_Manager] foreign key ([Manager_Id]) references [dbo].[Employees]([Id]);
alter table [dbo].[VacationRequests] add constraint [VacationRequest_Employee] foreign key ([Employee_Id]) references [dbo].[Employees]([Id]) on delete cascade;
alter table [dbo].[VacationStatus] add constraint [VacationStatus_Employee] foreign key ([Employee_Id]) references [dbo].[Employees]([Id]) on delete cascade;

COMMIT TRANSACTION;
GO
-- <VERSION INCREMENT>
BEGIN TRY
	-- Create first record for publisher version
	INSERT INTO [dbo].[Versions] (Name, VersionNumber) VALUES('VacationManager Database', '0.0.1')
	IF @@TRANCOUNT > 0 BEGIN
		COMMIT TRANSACTION
	END
	PRINT 'Patch 0.0.1 applied successfuly !'
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 BEGIN
		ROLLBACK TRANSACTION
	END
	PRINT 'Patch 0.0.1 not applied. Errors occured !'
END CATCH
-- </VERSION INCREMENT>
GO