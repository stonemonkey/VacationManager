create table [dbo].[Employees] (
    [Id] [bigint] not null identity,
    [LastName] [nvarchar](max) null,
    [Firstname] [nvarchar](max) null,
    [Cnp] [nvarchar](max) null,
    [Gender] [nvarchar](max) null,
    [BirthDate] [datetime] not null,
    [Email] [nvarchar](max) null,
    [Address] [nvarchar](max) null,
    [PhoneNumber] [nvarchar](max) null,
    [HireDate] [datetime] not null,
    [Roles] [int] not null,
    [Salt] [nvarchar](max) null,
    [Password] [nvarchar](max) null,
    [Manager_Id] [bigint] null,
    primary key ([Id])
);
create table [dbo].[EmployeeSituation] (
    [Id] [bigint] not null identity,
    [Year] [int] not null,
    [PaidDays] [int] not null,
    [ConsumedDays] [int] not null,
    [AvailableDays] [int] not null,
    [Employee_Id] [bigint] not null,
    primary key ([Id])
);
create table [dbo].[VacationRequests] (
    [Id] [bigint] not null identity,
    [CreationDate] [datetime] not null,
    [StartDate] [datetime] not null,
    [EndDate] [datetime] not null,
    [State] [int] not null,
    [Employee_Id] [bigint] not null,
    primary key ([Id])
);
alter table [dbo].[Employees] add constraint [EmployeeEntity_Manager] foreign key ([Manager_Id]) references [dbo].[Employees]([Id]);
alter table [dbo].[EmployeeSituation] add constraint [EmployeeSituationEntity_Employee] foreign key ([Employee_Id]) references [dbo].[Employees]([Id]) on delete cascade;
alter table [dbo].[VacationRequests] add constraint [VacationRequestEntity_Employee] foreign key ([Employee_Id]) references [dbo].[Employees]([Id]) on delete cascade;
