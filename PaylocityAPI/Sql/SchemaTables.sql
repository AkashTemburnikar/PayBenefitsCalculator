CREATE TABLE [dbo].[Employee]
(
	[EmployeeId]    INT IDENTITY(1,1)NOT NULL,
	[FirstName]		NVARCHAR (100) NOT NULL,
	[LastName]		NVARCHAR (100) NOT NULL,
	[EmailID]		NVARCHAR (100)NOT NULL UNIQUE,
	[Salary]		INT  DEFAULT (2000),
	[CreatedDate]	DATETIME2 (7)  DEFAULT (GETDATE()), 
	[ModifiedDate]	DATETIME2 (7) NULL, 

	CONSTRAINT [PK_Employee] PRIMARY KEY ([EmployeeId])
)

GO

CREATE TABLE [dbo].[DependentType]
(
	[DependentTypeId]	INT IDENTITY(1,1) NOT NULL,
	[Type]		NVARCHAR (100) NOT NULL,
	[Yearly] INT NOT NULL

	CONSTRAINT [PK_DependentType] PRIMARY KEY ([DependentTypeId]),
);

GO

CREATE TABLE [dbo].[Dependents]
(
	[DependentId]	INT IDENTITY(1,1) NOT NULL,
	[EmployeeId]	INT NOT NULL,
	[DepTypeId] INT NOT NULL,
	[Name] NVARCHAR (100) NOT NULL,
	[CreatedDate]	DATETIME2 (7)  DEFAULT (GETDATE()), 
	[ModifiedDate]	DATETIME2 (7) NULL, 


	CONSTRAINT [PK_Dependents] PRIMARY KEY ([DependentId]),
	CONSTRAINT [FK_Dependents_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([EmployeeId]),
	CONSTRAINT [FK_Dependents_DependentType] FOREIGN KEY ([DepTypeId]) REFERENCES [DependentType]([DependentTypeId])
);

INSERT INTO DependentType Values ('Child', 500), ('Spouse', 500) 

