/* Check if database already exists and delete it if it does exist*/
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'boatDatabase') 
BEGIN
	DROP DATABASE boatDatabase
	print '' print '*** dropping database boatDatabase'
END
GO

print '' print '*** creating database boatDatabase'
GO
CREATE DATABASE boatDatabase
GO

print '' print '*** using database boatDatabase'
GO
USE [boatDatabase]
GO

print '' print '*** Creating Employee Table'
GO
/* ***** Object:  Table [dbo].[Employees]     ***** */
CREATE TABLE [dbo].[Employee](
	[EmployeeID] 	[int] IDENTITY (100000,1)	NOT NULL,
	[FirstName]		[varchar](50)			NOT NULL,
	[LastName]		[varchar](100)			NOT NULL,
	[PhoneNumber]	[varchar](10)			NOT NULL,
	[Email]			[varchar](100)			,
	[UserName]		[varchar](20)			,
	[PasswordHash]	[varchar](100)			NOT NULL DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]					NOT NULL DEFAULT 1,

	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_UserName] UNIQUE ([UserName] ASC)
)
GO

print '' print '*** Inserting Employee Test Records'
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [UserName])
	VALUES
		('Joanne', 'Smith', '3195557777', 'joanne@company.com', 'jsmith'),
		('Martin', 'Jones', '3195558888', 'martin@company.com', 'mjones'),
		('Leo', 'Williams', '3195559999', 'leo@company.com', 'lwilliams')
GO


print '' print '*** Creating EmployeeRole Table'
GO
CREATE TABLE [dbo].[EmployeeRole](
	[EmployeeID]			[int]							NOT NULL,
	[RoleID]				[int]							NOT NULL,
	CONSTRAINT [pk_EmployeeIDRoleID] PRIMARY KEY([EmployeeID] ASC, [RoleID] ASC)
)
GO

print '' print '*** Creating Roles Table'
GO
CREATE TABLE [dbo].[Role](
	[RoleID]				[int] IDENTITY (100000,1)		NOT NULL,
	[RoleName]				[varchar](50)					NOT NULL,
	[RoleDescription]		[varchar](250)					NOT NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO

print '' print '*** Inserting Role Records'
INSERT INTO [dbo].[Role]
		([RoleName], [RoleDescription])
	VALUES
		('Rental', 'Rents Boats'),
		('Checkout', 'Checks Out Boats'),
		('Checkin', 'Checks In Boats'),
		('Inspection', 'Inspects Boats'),
		('Maintenance', 'Maintains Boats'),
		('Prep', 'Preps Boats'),
		('Manager', 'Manages Boats and Employees')
GO


print '' print '*** Creating EmployeeRole EmployeeID foreign key'
GO
ALTER TABLE [dbo].[EmployeeRole]  WITH NOCHECK 
	ADD CONSTRAINT [FK_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating EmployeeRole RoleID foreign key'
GO
ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK 
	ADD CONSTRAINT [FK_RoleID] FOREIGN KEY([RoleID])
	REFERENCES [dbo].[Role] ([RoleID])
	ON UPDATE CASCADE
GO

print '' print '*** Inserting Sample EmployeeRole Records'
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeId], [RoleId])
	VALUES
		(100000, 100000),
		(100001, 100001),
		(100001, 100002),
		(100001, 100003),
		(100001, 100004),
		(100001, 100005),
		(100002, 100001),
		(100002, 100006)
GO

print '' print '*** Creating sp_update_employee_email'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_email]
	(
	@EmployeeID		int,
	@EmailAddress	varchar(100)
	)
AS
	BEGIN
		UPDATE Employee
			SET Email = @EmailAddress
			WHERE EmployeeID = @EmployeeID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
	@Username		varchar(20),
	@PasswordHash	varchar(100)
	)
AS
	BEGIN
		SELECT COUNT(EmployeeID)
		FROM Employee
		WHERE Username = @Username
		AND PasswordHash = @PasswordHash
	END
GO

print '' print '*** Creating sp_retrieve_employee_roles'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_roles]
	(
	@EmployeeID		int
	)
AS
	BEGIN
		SELECT [Role].RoleID, RoleName, RoleDescription
		FROM EmployeeRole, Role
		WHERE [EmployeeRole].[EmployeeID] = @EmployeeID
		AND [EmployeeRole].[RoleID] = [Role].[RoleID]
	END
GO

print '' print '*** Creating sp_update_passwordHash'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
	(
	@EmployeeID			int,
	@OldPasswordHash	varchar(100),
	@NewPasswordHash	varchar(100)
	)
AS
	BEGIN
		UPDATE Employee
			SET PasswordHash = @NewPasswordHash
			WHERE EmployeeID = @EmployeeID
			AND PasswordHash = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_employee_by_username'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_username]
	(
	@Username		varchar(20)
	)
AS
	BEGIN
		SELECT EmployeeID, FirstName, LastName, PhoneNumber, Email, UserName, Active
		FROM Employee
		WHERE UserName = @Username
	END
GO	

print '' print '*** Creating Boat table'
GO
/* ***** Object: Table [dbo].[Boat] ***** */
CREATE TABLE [dbo].[Boat](
	[BoatID]		[int]	IDENTITY(100000,1)	NOT NULL,
	[BoatTypeID]	[int]						NOT NULL,
	[Name]			[varchar](50) 				NOT NULL,
	[Hours]			[int]						NOT NULL,
	[ModelYear] 	[int] 						NOT NULL,
	[PurchaseDate] 	[date]						NOT NULL,
	[BoatStatusID] 	[varchar](50)				NOT NULL,
	[DockSlipID]	[int]						NOT NULL,
	[Color]			[varchar](50) 				NOT NULL,
	[Active] 		[bit]						NOT NULL	DEFAULT 1,
	CONSTRAINT [pk_BoatID] PRIMARY KEY ([BoatID] ASC),
	CONSTRAINT [ak_Name] UNIQUE ([Name] ASC)
)
GO	

print '' print '*** Creating BoatStatus Table'
GO
CREATE TABLE [dbo].[BoatStatus](
	[BoatStatusID] 	[varchar](50) 				NOT NULL,
	CONSTRAINT [pk_BoatStatusID] PRIMARY KEY ([BoatStatusID] ASC)
)
GO

print '' print '*** Inserting BoatStatus Records'
GO
INSERT INTO [dbo].[BoatStatus]
		([BoatStatusID])
	VALUES
		("Available"),
		("Rented"),
		("Out"),
		("In"),
		("Needs Prep"),
		("Needs Maintenance"),
		("Maintenance"),
		("Out of Service")
GO

print '' print '*** Inserting Boat Records'	
INSERT INTO [dbo].[Boat]
		([BoatTypeID], [Name], [Hours], [ModelYear], [PurchaseDate], 
		[BoatStatusID], [DockSlipID], [Color])
	VALUES
		(100000, "Minnow", 0, 2016, '01/01/2016', "Available", 100000, "Blue"),
		(100000, "Titanic", 0, 2016, '01/01/2016', "Available", 100001, "Red"),
		(100000, "Black Pearl", 0, 2016, '01/01/2016', "Available", 100002, "Yellow"),
		(100000, "Lusitania", 0, 2016, '01/01/2016', "Available", 100003, "White"),
		(100000, "Mayflower", 0, 2016, '01/01/2016', "Available", 100004, "Green"),
		(100000, "Nina", 0, 2016, '01/01/2016', "Available", 100005, "Orange"),
		(100000, "Pinta", 0, 2016, '01/01/2016', "Available", 100006, "Black"),
		(100000, "Santa Maria", 0, 2016, '01/01/2016', "Available", 100007, "Gray"),
		(100000, "Essex", 0, 2016, '01/01/2016', "Available", 100008, "Brown")		
GO	

print '' print '*** Creating Boat table / BoatStatusID Constraint'
GO	
ALTER TABLE [dbo].[Boat]  WITH NOCHECK 
	ADD CONSTRAINT [fk_BoatStatusID] FOREIGN KEY([BoatStatusID])
	REFERENCES [dbo].[BoatStatus] ([BoatStatusID])
	ON UPDATE CASCADE
GO	

print '' print '*** Creating sp_retrieve_boat_by_status'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_boats_by_status]
	(
	@BoatStatusID		varchar(50)
	)
AS
	BEGIN
		SELECT BoatID, BoatTypeID, Name, Hours, ModelYear, PurchaseDate,
			BoatStatusID, DockSlipID, Color
		FROM Boat
		WHERE BoatStatusID = @BoatStatusID
		  AND Active = 1
	END
GO	

print '' print '*** Creating sp_update_boatstatus'
GO
CREATE PROCEDURE [dbo].[sp_update_boatstatus]
	(
	@BoatID				int,
	@OldBoatStatusID	varchar(50),
	@NewBoatStatusID	varchar(50)
	)
AS
	BEGIN
		UPDATE Boat
			SET BoatStatusID = @NewBoatStatusID
			WHERE BoatStatusID = @OldBoatStatusID
			AND BoatID = @BoatID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_boats_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_boats_by_active]
	(
	@Active			[bit]
	)
AS
	BEGIN
		SELECT BoatID, BoatTypeID, Name, Hours, ModelYear, PurchaseDate,
			BoatStatusID, DockSlipID, Color, Active
		FROM Boat
		WHERE Active = @Active
	END
GO	

print '' print '*** Creating sp_insert_boat'
GO
CREATE PROCEDURE [dbo].[sp_insert_boat]
	(
		@BoatTypeID			[int], 
		@Name				[varchar](50), 
		@Hours				[int], 
		@ModelYear			[int], 
		@PurchaseDate		[date], 
		@BoatStatusID		[varchar](50), 
		@DockSlipID			[int], 
		@Color				[varchar](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Boat]
			([BoatTypeID], [Name], [Hours], [ModelYear], [PurchaseDate], 
				[BoatStatusID], [DockSlipID], [Color])
		VALUES
			(@BoatTypeID, @Name, @Hours, @ModelYear, @PurchaseDate, 
				@BoatStatusID, @DockSlipID, @Color)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_boat'
GO
CREATE PROCEDURE [dbo].[sp_update_boat]
	(
	@BoatID					[int],
	
	@OldBoatTypeID			[int], 
	@OldName				[varchar](50), 
	@OldHours				[int], 
	@OldModelYear			[int], 
	@OldPurchaseDate		[date], 
	@OldBoatStatusID		[varchar](50), 
	@OldDockSlipID			[int], 
	@OldColor				[varchar](50),
	@OldActive				[bit],
	
	@NewBoatTypeID			[int], 
	@NewName				[varchar](50), 
	@NewHours				[int], 
	@NewModelYear			[int], 
	@NewPurchaseDate		[date], 
	@NewBoatStatusID		[varchar](50), 
	@NewDockSlipID			[int], 
	@NewColor				[varchar](50),
	@NewActive				[bit]
	)
AS
	BEGIN
		UPDATE Boat
		      SET BoatTypeID = @NewBoatTypeID,
				  Name = @NewName,
				  Hours = @NewHours, 
			      ModelYear = @NewModelYear,
			      PurchaseDate = @NewPurchaseDate,
			      BoatStatusID = @NewBoatStatusID,
			      DockSlipID = @NewDockSlipID,
			      Color = @NewColor,
			      Active = @NewActive
			WHERE BoatTypeID = @OldBoatTypeID
			  AND Name = @OldName
			  AND Hours = @OldHours
			  AND ModelYear = @OldModelYear
			  AND PurchaseDate = @OldPurchaseDate
			  AND BoatStatusID = @OldBoatStatusID
			  AND DockSlipID = @OldDockSlipID
			  AND Color = @OldColor
			  AND Active = @OldActive
			  AND BoatID = @BoatID
		RETURN @@ROWCOUNT
	END
GO












