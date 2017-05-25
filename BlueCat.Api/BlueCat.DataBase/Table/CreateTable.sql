Create database [School]
go
USE [School]
GO

/****** Object:  Table [dbo].[Enroll]    Script Date: 2017/5/25 13:38:17 ******/


CREATE TABLE [dbo].[Enroll](
	[EnrollmentID] [uniqueidentifier] NOT NULL,
	[CousrseID] [uniqueidentifier] NULL,
	[StudentID] [uniqueidentifier] NULL,
	[Grade] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[EnrollmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
go
CREATE TABLE [dbo].[Student](
	[StudentId] [uniqueidentifier] NOT NULL,
	[LastName] [nvarchar](20) NULL,
	[FirstMidName] [nvarchar](20) NULL,
	[EnrollmentDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

go
CREATE TABLE [dbo].[Course](
	[CousrseID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](20) NULL,
	[Credits] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[CousrseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

