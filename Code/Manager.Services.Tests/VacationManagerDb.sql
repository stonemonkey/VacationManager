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
    [Employee_Id] [bigint] not null,
    primary key ([RequestNumber])
);
alter table [dbo].[Employees] add constraint [Employee_Manager] foreign key ([Manager_Id]) references [dbo].[Employees]([Id]);
alter table [dbo].[VacationRequests] add constraint [VacationRequest_Employee] foreign key ([Employee_Id]) references [dbo].[Employees]([Id]) on delete cascade;
