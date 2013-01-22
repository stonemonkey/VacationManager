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
alter table [dbo].[Employees] add constraint [Employee_Manager] foreign key ([Manager_Id]) references [dbo].[Employees]([Id]);
alter table [dbo].[VacationRequests] add constraint [VacationRequest_Employee] foreign key ([Employee_Id]) references [dbo].[Employees]([Id]) on delete cascade;
alter table [dbo].[VacationStatus] add constraint [VacationStatus_Employee] foreign key ([Employee_Id]) references [dbo].[Employees]([Id]) on delete cascade;
