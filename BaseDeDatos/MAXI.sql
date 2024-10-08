CREATE DATABASE [MAXI]
GO
USE [MAXI]
GO
/****** Object:  UserDefinedTableType [dbo].[PercentageTableType]    Script Date: 23/08/2024 12:51:20 a. m. ******/
CREATE TYPE [dbo].[PercentageTableType] AS TABLE(
	[Id] [int] NULL,
	[ParticipationPercentage] [int] NULL
)
GO
/****** Object:  Table [dbo].[Beneficiaries]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beneficiaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Curp] [varchar](18) NOT NULL,
	[Ssn] [varchar](11) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[Nationality] [varchar](50) NOT NULL,
	[ParticipationPercentage] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IdEmployee] [int] NOT NULL,
 CONSTRAINT [PK_Beneficiaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[EmployeeNumber] [int] NOT NULL,
	[Curp] [varchar](18) NOT NULL,
	[Ssn] [varchar](11) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[Nationality] [varchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Pass] [varbinary](8000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([IdUser], [UserName], [Pass]) VALUES (1, N'maxi', 0x8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__C9F284561077B309]    Script Date: 23/08/2024 12:51:20 a. m. ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__C9F28456E983FB44]    Script Date: 23/08/2024 12:51:20 a. m. ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Beneficiaries] ADD  CONSTRAINT [DF__Beneficia__IsDel__398D8EEE]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Beneficiaries]  WITH CHECK ADD  CONSTRAINT [FK_Beneficiaries_Employees] FOREIGN KEY([IdEmployee])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Beneficiaries] CHECK CONSTRAINT [FK_Beneficiaries_Employees]
GO
/****** Object:  StoredProcedure [dbo].[SPR_ADD_BENEFICIARY]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_ADD_BENEFICIARY]
(
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	--@ParticipationPercentage int,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10) ,
	@Nationality NVARCHAR(50),
	@IdEmployee int,
	@Id INT OUTPUT
)
AS          
BEGIN TRY
	SET NOCOUNT ON;
	DECLARE @CalculatedPercentage INT=100

	IF EXISTS(SELECT TOP 1 1 FROM dbo.Beneficiaries WHERE IdEmployee=@IdEmployee AND IsDeleted=0)
	BEGIN
		SET @CalculatedPercentage=0
	END

	INSERT INTO 
		dbo.Beneficiaries
			([FirstName],[LastName],[DateOfBirth],[ParticipationPercentage],[Curp],[Ssn],[PhoneNumber],[Nationality],[IdEmployee]) 
	VALUES
		(@FirstName,@LastName,@DateOfBirth,@CalculatedPercentage,@Curp,@Ssn,@PhoneNumber,@Nationality,@IdEmployee)
	SET @Id = @@IDENTITY

END TRY
BEGIN CATCH
	SET @Id = 0
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[SPR_ADD_EMPLOYEE]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_ADD_EMPLOYEE]
(
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	@EmployeeNumber int,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10) ,
	@Nationality NVARCHAR(50),
	@Id INT OUTPUT
)
AS          
BEGIN TRY
	SET NOCOUNT ON;

	INSERT INTO 
		Employees
			([FirstName],[LastName],[DateOfBirth],[EmployeeNumber],[Curp],[Ssn],[PhoneNumber],[Nationality]) 
	VALUES
		(@FirstName,@LastName,@DateOfBirth,@EmployeeNumber,@Curp,@Ssn,@PhoneNumber,@Nationality)
	SET @Id = @@IDENTITY

END TRY
BEGIN CATCH
	SET @Id = 0
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[SPR_DELETE_BENEFICIARY]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_DELETE_BENEFICIARY]
(
	@Id INT,
	@HasError BIT OUTPUT
)
AS          
BEGIN TRY
	SET NOCOUNT ON;

	Declare @IdEmployee INT, @ParticipationPercentage INT
	SELECT TOP 1 @IdEmployee=IdEmployee,@ParticipationPercentage=ParticipationPercentage FROM dbo.Beneficiaries WITH(NOLOCK) WHERE Id = @Id

	UPDATE dbo.Beneficiaries SET [IsDeleted] = 1 WHERE Id = @Id

	UPDATE TOP (1) 
		dbo.Beneficiaries 
	SET 
		ParticipationPercentage=ParticipationPercentage+@ParticipationPercentage
	WHERE IdEmployee = @IdEmployee AND [IsDeleted] = 0

	SET @HasError=0
END TRY
BEGIN CATCH
	SET @HasError=1
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[SPR_DELETE_EMPLOYEE]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_DELETE_EMPLOYEE]
(
	@Id INT,
	@HasError BIT OUTPUT
)
AS          
BEGIN TRY
	SET NOCOUNT ON;

	BEGIN TRAN T1;

		UPDATE Employees SET [IsDeleted] = 1 WHERE Id = @Id

		UPDATE Beneficiaries SET [IsDeleted] = 1 WHERE IdEmployee = @Id

		

	COMMIT TRAN T1;
	
	SET @HasError=0
END TRY
BEGIN CATCH
	SET @HasError=1
	IF (@@TRANCOUNT > 0)
	BEGIN
		ROLLBACK TRANSACTION T1
	END 
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[SPR_GET_ALL_BENEFICIARIES_OF_EMPLOYEE]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_GET_ALL_BENEFICIARIES_OF_EMPLOYEE]
(
	@IdEmployee INT
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		B.Id,
		B.FirstName,
		B.LastName,
		B.DateOfBirth,
		B.Curp,
		B.Ssn,
		B.PhoneNumber,
		B.Nationality,
		B.ParticipationPercentage
	FROM
		dbo.Beneficiaries B WITH(NOLOCK)
	WHERE
		B.IdEmployee = @IdEmployee AND B.IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[SPR_GET_ALL_EMPLOYEES]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_GET_ALL_EMPLOYEES]
AS          
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id,
		FirstName,
		LastName,
		DateOfBirth,
		EmployeeNumber,
		Curp,
		Ssn,
		PhoneNumber,
		Nationality
	FROM
		dbo.Employees WITH(NOLOCK)
	WHERE
		IsDeleted=0

END
GO
/****** Object:  StoredProcedure [dbo].[SPR_UPDATE_BENEFICIARY]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_UPDATE_BENEFICIARY]
(
	@Id INT,
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	@ParticipationPercentage INT,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10) ,
	@Nationality NVARCHAR(50),
	@HasError BIT OUTPUT
)
AS          
BEGIN TRY
	SET NOCOUNT ON;
	UPDATE 
		dbo.Beneficiaries
	SET
		FirstName = @FirstName,
		LastName = @LastName,
		DateOfBirth = @DateOfBirth,
		Curp = @Curp,
		Ssn = @Ssn,
		PhoneNumber = @PhoneNumber,
		Nationality = @Nationality,
		ParticipationPercentage=@ParticipationPercentage
	WHERE
		Id = @Id

	SET @HasError = 0
END TRY
BEGIN CATCH
	SET @HasError = 1
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[SPR_UPDATE_EMPLOYEE]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_UPDATE_EMPLOYEE]
(
	@Id INT,
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	@EmployeeNumber int,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10),
	@Nationality NVARCHAR(50),
	@HasError BIT OUTPUT
)
AS
BEGIN TRY
	SET NOCOUNT ON;
	UPDATE
		Employees 
	SET
		FirstName = @FirstName, 
		LastName = @LastName, 
		DateOfBirth = @DateOfBirth, 
		EmployeeNumber = @EmployeeNumber, 
		Curp = @Curp, 
		Ssn = @Ssn, 
		PhoneNumber = @PhoneNumber, 
		Nationality = @Nationality 
	WHERE
		Id = @Id;

	SET @HasError=0;
END TRY
BEGIN CATCH
	SET @HasError=1;
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[SPR_UPDATE_PERCENTAGE]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_UPDATE_PERCENTAGE]
(
	@PercentageType PercentageTableType READONLY,
	@HasError BIT OUTPUT
)
AS          
BEGIN TRY
	SET NOCOUNT ON;

	UPDATE
		B
	SET
		B.ParticipationPercentage = P.ParticipationPercentage
	FROM
		dbo.Beneficiaries B
			INNER JOIN @PercentageType P ON P.Id = B.Id

	SET @HasError = 0
END TRY
BEGIN CATCH
	SET @HasError = 1
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[SPR_VALIDATE_USER]    Script Date: 23/08/2024 12:51:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPR_VALIDATE_USER]
(
	@UserName VARCHAR(50),
	@Pass VARCHAR(50),
	@UserExist BIT OUTPUT
)
AS          
BEGIN TRY
	SET NOCOUNT ON;

	IF EXISTS(SELECT TOP 1
		IdUser,
		UserName,
		Pass
	FROM
		dbo.Users WITH(NOLOCK)
	where 
		UserName=@UserName and Pass=HASHBYTES('SHA2_256', @Pass))
	BEGIN
		SET @UserExist=1
	END
	ELSE
	BEGIN
		SET @UserExist=0
	END

END TRY
BEGIN CATCH
	SET @UserExist=0
END CATCH;
GO
