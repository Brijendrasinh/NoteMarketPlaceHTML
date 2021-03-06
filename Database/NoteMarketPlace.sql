USE [master]
GO
/****** Object:  Database [NoteMarketPlace]    Script Date: 16-04-2021 14:03:13 ******/
CREATE DATABASE [NoteMarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NoteMarketPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NoteMarketPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NoteMarketPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NoteMarketPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NoteMarketPlace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NoteMarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NoteMarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NoteMarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NoteMarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [NoteMarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [NoteMarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NoteMarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NoteMarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NoteMarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NoteMarketPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NoteMarketPlace', N'ON'
GO
ALTER DATABASE [NoteMarketPlace] SET QUERY_STORE = OFF
GO
USE [NoteMarketPlace]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CountryCode] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Downloads]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Downloads](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[Seller] [int] NOT NULL,
	[Downloader] [int] NOT NULL,
	[IsSellerHasAllowedDownloads] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsAttachmentDownloaded] [bit] NOT NULL,
	[AttachmentDownloadDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[PurchasedPrice] [decimal](18, 0) NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategory] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Downloads] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](15) NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteCategories]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_NoteCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteReports]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReportedByID] [int] NOT NULL,
	[DownloaderID] [int] NOT NULL,
	[Remarks] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteReviews]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReviews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReviewedByID] [int] NOT NULL,
	[DownloadsID] [int] NOT NULL,
	[Ratings] [decimal](18, 0) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteTypes]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_NoteTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reference]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reference](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[RefCategory] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Reference] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellNoteAttachments]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellNoteAttachments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellNoteAttachments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellNotes]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SellerID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ActionedBy] [int] NOT NULL,
	[AdminRemarks] [varchar](max) NULL,
	[PublishedDate] [datetime] NULL,
	[Title] [varchar](100) NOT NULL,
	[Category] [int] NOT NULL,
	[DisplayPicture] [varchar](500) NULL,
	[NoteType] [int] NULL,
	[NumberOfPages] [int] NULL,
	[Discription] [varchar](max) NOT NULL,
	[UniversityName] [varchar](200) NULL,
	[Country] [int] NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[Professor] [varchar](100) NULL,
	[IsPaid] [bit] NOT NULL,
	[SellingPrice] [decimal](18, 0) NULL,
	[NotesPreview] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ApprovedBy] [int] NULL,
	[RejectedBy] [int] NULL,
	[AttachmentSize] [nvarchar](50) NULL,
 CONSTRAINT [PK_SellNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfiguration]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfiguration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Keys] [varchar](100) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SystemConfiguration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DOB] [datetime] NULL,
	[Gender] [int] NULL,
	[SecondaryEmailAddress] [varchar](100) NULL,
	[CountryCode] [varchar](5) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](500) NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'India', N'+91', CAST(N'2021-03-04T21:22:00.000' AS DateTime), 3, CAST(N'2021-04-08T17:48:15.957' AS DateTime), 3, 1)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Downloads] ON 

INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownloads], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (12, 30, 19, 20, 1, N'~/Members/19/30/Attachment/', 1, CAST(N'2021-04-08T21:08:33.683' AS DateTime), 1, CAST(200 AS Decimal(18, 0)), N'Computer', N'Science', CAST(N'2021-03-20T14:07:59.767' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownloads], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (18, 30, 19, 21, 0, N'~/Members/19/30/Attachment/', 0, NULL, 1, CAST(200 AS Decimal(18, 0)), N'Computer', N'Science', CAST(N'2021-03-20T17:42:01.273' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownloads], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (19, 30, 19, 22, 1, N'~/Members/19/30/Attachment/', 1, CAST(N'2021-04-08T16:15:38.673' AS DateTime), 1, CAST(200 AS Decimal(18, 0)), N'Computer', N'Science', CAST(N'2021-03-20T17:45:17.473' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownloads], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (24, 39, 20, 19, 1, N'~/Members/20/39/Attachment/', 1, CAST(N'2021-03-20T19:17:24.043' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'Environmental studies', N'Science', CAST(N'2021-03-20T19:17:24.043' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownloads], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (25, 1035, 20, 22, 1, N'~/Members/20/1035/Attachment/', 1, CAST(N'2021-03-21T15:16:57.013' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'C++', N'Science', CAST(N'2021-03-21T15:16:57.017' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownloads], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (26, 1036, 20, 21, 0, N'~/Members/20/1036/Attachment/', 0, NULL, 1, CAST(100 AS Decimal(18, 0)), N'C S', N'Science', CAST(N'2021-03-23T10:51:26.717' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownloads], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1020, 2044, 1003, 22, 1, N'~/Members/1003/2044/Attachment/', 1, CAST(N'2021-04-08T19:14:12.147' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'Artificial Intelligence', N'Science', CAST(N'2021-04-08T19:14:12.150' AS DateTime), 22, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Downloads] OFF
GO
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([ID], [Name]) VALUES (1, N'Male')
INSERT [dbo].[Gender] ([ID], [Name]) VALUES (2, N'Female')
INSERT [dbo].[Gender] ([ID], [Name]) VALUES (3, N'Unknown')
SET IDENTITY_INSERT [dbo].[Gender] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteCategories] ON 

INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Science', N'Science Stream', CAST(N'2021-03-05T00:09:00.000' AS DateTime), 1, CAST(N'2021-04-05T14:35:02.530' AS DateTime), 0, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Economy', N'Lorem Ipsum Is great', CAST(N'2021-04-05T14:35:25.960' AS DateTime), 0, NULL, NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Information and Technology', N'Software', CAST(N'2021-04-13T21:23:36.353' AS DateTime), 1010, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteReports] ON 

INSERT [dbo].[NoteReports] ([ID], [NoteID], [ReportedByID], [DownloaderID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 1035, 22, 25, N'lorem get this', CAST(N'2021-04-08T16:50:09.383' AS DateTime), 7, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteReports] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteReviews] ON 

INSERT [dbo].[NoteReviews] ([ID], [NoteID], [ReviewedByID], [DownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 30, 20, 12, CAST(5 AS Decimal(18, 0)), N'Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  ', CAST(N'2021-03-27T13:42:05.313' AS DateTime), 1, NULL, NULL, 0)
INSERT [dbo].[NoteReviews] ([ID], [NoteID], [ReviewedByID], [DownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 30, 20, 12, CAST(4 AS Decimal(18, 0)), N'Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  ', CAST(N'2021-03-27T13:42:51.263' AS DateTime), 4, NULL, NULL, 0)
INSERT [dbo].[NoteReviews] ([ID], [NoteID], [ReviewedByID], [DownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 30, 20, 12, CAST(4 AS Decimal(18, 0)), N'Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  ', CAST(N'2021-03-27T13:43:11.087' AS DateTime), 6, NULL, NULL, 0)
INSERT [dbo].[NoteReviews] ([ID], [NoteID], [ReviewedByID], [DownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 39, 20, 24, CAST(5 AS Decimal(18, 0)), N'Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  ', CAST(N'2021-03-27T13:47:21.227' AS DateTime), 8, NULL, NULL, 0)
INSERT [dbo].[NoteReviews] ([ID], [NoteID], [ReviewedByID], [DownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 1035, 20, 25, CAST(4 AS Decimal(18, 0)), N'Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  Lorem  ', CAST(N'2021-03-27T13:47:37.283' AS DateTime), 9, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[NoteReviews] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteTypes] ON 

INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Fiction', N'Lorem Ipsum', CAST(N'2021-03-05T00:10:00.000' AS DateTime), 1, CAST(N'2021-03-05T00:10:00.000' AS DateTime), 1, 1)
INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'novel ', N'Lorem Ipsum Is great', CAST(N'2021-04-05T14:01:59.487' AS DateTime), 0, CAST(N'2021-04-08T17:48:00.593' AS DateTime), 3, 1)
SET IDENTITY_INSERT [dbo].[NoteTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Reference] ON 

INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Free', N'Selling Mode', CAST(N'2021-02-10T18:22:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:23:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Paid', N'Selling Mode', CAST(N'2021-02-10T18:23:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:24:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Draft', N'Note Status', CAST(N'2021-02-10T18:24:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:24:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Submitted For Review', N'Note Status', CAST(N'2021-02-10T18:25:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:25:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'In Review', N'Note Status', CAST(N'2021-02-10T18:26:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:26:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Approved', N'Note Status', CAST(N'2021-02-10T18:27:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:28:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'Rejected', N'Note Status', CAST(N'2021-02-10T18:28:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:28:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Reference] ([ID], [Value], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, N'Removed', N'Note Status', CAST(N'2021-02-10T18:29:00.000' AS DateTime), 1, CAST(N'2021-02-10T18:29:00.000' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Reference] OFF
GO
SET IDENTITY_INSERT [dbo].[SellNoteAttachments] ON 

INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 30, N'4.pdf', N'C:\Users\Brijendra\source\repos\NoteMarketPlace\WebApplication5MVCdemo\Members\19\30\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 46, N'7.JPG', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\20\46\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 47, N'8.JPG', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\20\47\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 39, N'10.JPG', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\20\39\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1010, 1035, N'1010.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\20\1035\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1011, 1036, N'1011_2021032103470430.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\20\1036\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1012, 1037, N'1012_2021032104294579.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\20\1037\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2019, 2044, N'2019_ppr1_b_20210408025311.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\1003\2044\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2020, 2044, N'2020_solo_20210408025315.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\1003\2044\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2021, 2045, N'2021___Members_20_1035_Attachment_1010_20210408043757.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\22\2045\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2022, 2045, N'2022_Byju''s JD_20210408043758.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\22\2045\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2023, 2045, N'2023_SEM_8_Introductory_Session_Time_Table_20210408043758.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\22\2045\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2024, 2046, N'2024___Members_20_1035_Attachment_1010_20210408043934.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\22\2046\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2025, 2046, N'2025_Byju''s JD_20210408043934.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\22\2046\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2026, 2046, N'2026_20201223175906_475175_20210408043934.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\22\2046\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2027, 2046, N'2027_SEM_8_Introductory_Session_Time_Table_20210408043934.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\22\2046\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2028, 2054, N'2028_solo_20210411044240.pdf', N'C:\Users\Brijendra\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\20\2054\Attachment\', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellNoteAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2029, 2060, N'2029_????? ????? ???_20210415025255.pdf', N'C:\Users\Hello Tech\Desktop\Back on Git Repo\mvc\NoteMarketPlace\WebApplication5MVCdemo\Members\1003\2060\Attachment\', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellNoteAttachments] OFF
GO
SET IDENTITY_INSERT [dbo].[SellNotes] ON 

INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (30, 19, 6, 3, NULL, CAST(N'2021-03-05T11:04:00.000' AS DateTime), N'Computer', 1, N'DP_.jpg', 1, 155, N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Soluta quod eius est voluptas officiis, facilis, illo doloribus iure.', N'l j ', 1, N'IT', N'452133', N'R B', 1, CAST(200 AS Decimal(18, 0)), N'PreviewFile_.pdf', CAST(N'2021-03-05T11:04:00.000' AS DateTime), NULL, NULL, NULL, 1, 20, NULL, N'411.7KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (39, 20, 7, 3, N'Lorem ds', CAST(N'2021-03-17T14:28:08.447' AS DateTime), N'Environmental studies', 1, N'DP_.JPG', 1, 400, N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Soluta quod eius est voluptas officiis, facilis, illo doloribus iure.', N'l j', 1, N'CE', N'452126', N'L E', 0, CAST(0 AS Decimal(18, 0)), N'PreviewFile_.JPG', CAST(N'2021-03-17T14:28:08.447' AS DateTime), NULL, CAST(N'2021-04-08T17:39:05.623' AS DateTime), 3, 1, 19, 3, N'401.7KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (46, 20, 6, 3, NULL, CAST(N'2021-03-17T14:29:08.000' AS DateTime), N'E M E', 1, N'DP_.JPG', 1, 400, N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Soluta quod eius est voluptas officiis, facilis, illo doloribus iure.', N'l j', 1, N'CE', N'452126', N'L E', 0, CAST(0 AS Decimal(18, 0)), N'PreviewFile_.JPG', CAST(N'2021-03-17T14:29:08.000' AS DateTime), NULL, NULL, NULL, 1, 19, NULL, N'421.7KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (47, 20, 7, 3, N'this is fake note', CAST(N'2021-03-17T14:30:08.000' AS DateTime), N'Java Programming', 1, N'DP_.JPG', 1, 400, N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Soluta quod eius est voluptas officiis, facilis, illo doloribus iure.', N'l j', 1, N'CE', N'452126', N'L E', 0, CAST(0 AS Decimal(18, 0)), N'PreviewFile_.JPG', CAST(N'2021-03-17T14:30:08.000' AS DateTime), NULL, CAST(N'2021-04-02T23:13:08.463' AS DateTime), 3, 1, 19, 3, N'451.7KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (1035, 20, 6, 3, NULL, CAST(N'2021-03-21T15:01:17.423' AS DateTime), N'C++', 1, N'DP_.png', 1, 155, N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Soluta quod eius est voluptas officiis, facilis, illo doloribus iure.', N'l j ', 1, N'IT', N'452133', N'J S', 0, CAST(0 AS Decimal(18, 0)), N'PreviewFile_.pdf', CAST(N'2021-03-21T15:01:17.423' AS DateTime), NULL, NULL, NULL, 1, 19, NULL, N'441.7KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (1036, 20, 6, 3, NULL, CAST(N'2021-03-21T15:43:04.207' AS DateTime), N'C S', 1, N'DP_2021442103441335.png', 1, 300, N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Soluta quod eius est voluptas officiis, facilis, illo doloribus iure.', N'l j ', 1, N'IT', N'452133', N'J S', 1, CAST(100 AS Decimal(18, 0)), N'PreviewFile_2021032103460708.pdf', CAST(N'2021-03-21T15:43:04.207' AS DateTime), NULL, NULL, NULL, 1, 19, NULL, N'481.7KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (1037, 20, 6, 3, NULL, CAST(N'2021-03-21T16:28:44.037' AS DateTime), N'Dot net ', 1, N'DP_2021032104291214.png', 1, 150, N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Soluta quod eius est voluptas officiis, facilis, illo doloribus iure.', N'l j ', 1, N'CE', N'135456', N'R B', 1, CAST(100 AS Decimal(18, 0)), N'PreviewFile_2021032104293353.pdf', CAST(N'2021-03-21T16:28:44.037' AS DateTime), NULL, NULL, NULL, 1, 19, NULL, N'411.7KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (2044, 1003, 6, 3, NULL, CAST(N'2021-04-08T17:30:17.127' AS DateTime), N'Artificial Intelligence', 1, N'DP_20210407032826.jpg', 1, 277, N'Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem ', N'l j ', 1, N'IT', N'452133', N'MT', 0, CAST(0 AS Decimal(18, 0)), N'PreviewFile_20210407032826.pdf', CAST(N'2021-04-08T14:53:09.313' AS DateTime), NULL, NULL, NULL, 1, 3, NULL, N'411.7 KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (2045, 22, 6, 3, NULL, CAST(N'2021-04-08T17:30:04.090' AS DateTime), N'python programing', 1, NULL, 1, 300, N'lorem ', N'LDRP', 1, N'IT', N'125643', N'R B', 1, CAST(200 AS Decimal(18, 0)), N'PreviewFile_20210408043755.pdf', CAST(N'2021-04-08T16:37:54.600' AS DateTime), NULL, NULL, NULL, 1, 3, NULL, N'0.5 MB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (2046, 22, 6, 3, NULL, CAST(N'2021-04-08T17:30:22.570' AS DateTime), N'Computer Programming', 2, NULL, 1, 155, N'Lorem sbcjbdj', N'LDRP', 1, N'CE', N'452133', N'J S', 0, CAST(0 AS Decimal(18, 0)), NULL, CAST(N'2021-04-08T16:39:34.170' AS DateTime), NULL, NULL, NULL, 1, 3, NULL, N'0.7 MB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (2054, 20, 3, 3, NULL, NULL, N'Mobile computing and wireless Communication', 1, NULL, 2, NULL, N'Lorem', NULL, 1, NULL, NULL, NULL, 0, NULL, NULL, CAST(N'2021-04-11T16:42:37.420' AS DateTime), NULL, NULL, NULL, 1, NULL, NULL, N'118.6 KB')
INSERT [dbo].[SellNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberOfPages], [Discription], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ApprovedBy], [RejectedBy], [AttachmentSize]) VALUES (2060, 1003, 3, 3, NULL, NULL, N'Database Management System', 3, N'DP_20210415025245.jpg', 1, NULL, N',vljw vrwrvdc', NULL, 1, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, CAST(N'2021-04-15T14:52:41.063' AS DateTime), NULL, NULL, NULL, 1, NULL, NULL, N'1.1 MB')
SET IDENTITY_INSERT [dbo].[SellNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemConfiguration] ON 

INSERT [dbo].[SystemConfiguration] ([ID], [Keys], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'SupportEmail', N'brijendrasinhchavda2018@gmail.com', CAST(N'2021-04-06T16:44:00.000' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[SystemConfiguration] ([ID], [Keys], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'SupportPhoneNumber', N'9773149052', CAST(N'2021-04-06T16:44:00.000' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[SystemConfiguration] ([ID], [Keys], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'DefaultNotePicture', N'DefaultNotePicture.jpg', CAST(N'2021-04-06T16:44:00.000' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[SystemConfiguration] ([ID], [Keys], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'DefaultProfilePicture', N'DefaultProfilePicture.jpg', CAST(N'2021-04-06T16:44:00.000' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[SystemConfiguration] ([ID], [Keys], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Facebook', N'https://www.facebook.com/TatvaSoft', CAST(N'2021-04-06T16:44:00.000' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[SystemConfiguration] ([ID], [Keys], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'Twitter', N'https://twitter.com/tatvasoft', CAST(N'2021-04-06T16:44:00.000' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[SystemConfiguration] ([ID], [Keys], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, N'Linkedin', N'https://www.linkedin.com/company/tatvasoft', CAST(N'2021-04-06T16:44:00.000' AS DateTime), 3, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SystemConfiguration] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 20, CAST(N'1999-10-27T00:00:00.000' AS DateTime), 1, NULL, N'+91', N'9773149052', N'DP_03042021090754.jpg', N'249, Ambika Nagar Part-1', N'near Damru circle,', N'Ahmedabad', N'Gujarat', N'380016', N'India', N'Gujarat Technological University', N'L J Institute Of Engineering and Technology', CAST(N'2021-03-13T12:24:25.070' AS DateTime), NULL, CAST(N'2021-04-03T21:07:35.000' AS DateTime), NULL)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 19, CAST(N'1999-12-28T00:00:00.000' AS DateTime), 1, NULL, N'+91', N'9773149052', NULL, N'249, Ambikanagar Part-1', N'Meghaninagar', N'Ahmedabad', N'Gujarat', N'380016', N'India', N'Gujarat Technological University', N'L J Institute Of Engineering and Technology', CAST(N'2021-03-13T22:19:06.380' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1002, 1009, NULL, NULL, NULL, N'+91', N'9016816131', NULL, N'Admin', N'Admin', N'Admin', N'Admin', N'Admin', N'Admin', NULL, NULL, CAST(N'2021-04-06T11:28:16.880' AS DateTime), 0, CAST(N'2021-04-06T11:53:25.657' AS DateTime), 0)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1003, 22, CAST(N'1995-10-31T00:00:00.000' AS DateTime), 1, NULL, N'+91', N'9787878784', NULL, N'249, Ambika Nagar Part-1,', N'near Damru circle,', N'Ahmedabad', N'Gujarat', N'381400', N'India', N'Gujarat Technological University', N'L J Institute Of Engineering and Technology', CAST(N'2021-04-08T16:13:43.857' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1004, 3, NULL, NULL, NULL, N'+91', N'9773149052', N'DP_08042021061353.png', N'Admin', N'Admin', N'Admin', N'Admin', N'Admin', N'Admin', NULL, NULL, CAST(N'2021-04-08T18:13:52.480' AS DateTime), 3, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1005, 1010, NULL, NULL, NULL, N'+91', N'9773149052', NULL, N'Admin', N'Admin', N'Admin', N'Admin', N'Admin', N'Admin', NULL, NULL, CAST(N'2021-04-10T11:38:29.623' AS DateTime), 3, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1006, 1003, NULL, 1, NULL, N'+91', N'99874523157', N'DP_16042021014825.jpg', N'jdghj', N'efdfdfd', N'vdsvdv', N'sdvvvs', N'sdvsdvd', N'India', NULL, NULL, CAST(N'2021-04-16T13:49:41.010' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Super Admin', N'everthing he can add , and full control of whole website', CAST(N'2021-02-11T18:32:00.000' AS DateTime), 1, CAST(N'2021-02-11T18:32:00.000' AS DateTime), 1, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Admin', N'any book he/she can sell or purchase notes', CAST(N'2021-02-11T18:33:00.000' AS DateTime), 1, CAST(N'2021-02-11T18:33:00.000' AS DateTime), 1, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Member', N'just downloader', CAST(N'2021-02-11T18:34:00.000' AS DateTime), 1, CAST(N'2021-02-11T18:34:00.000' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 1, N'Brijendrasinh', N'Chavda', N'sedkeha1234@gmail.com', N'B1chavd@', 1, CAST(N'2021-02-23T18:41:00.000' AS DateTime), 3, CAST(N'2021-02-23T18:41:00.000' AS DateTime), 3, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (19, 3, N'Viraj', N'Vaghela', N'virajvaghela@gmail.com', N'V1vaghel@', 1, CAST(N'2021-03-02T23:32:00.000' AS DateTime), 19, CAST(N'2021-03-02T23:32:00.000' AS DateTime), 19, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (20, 3, N'Kamlesh', N'Vaghela', N'kamleshvaghela@gmail.com', N'K1vaghel@', 1, CAST(N'2021-03-02T23:34:00.000' AS DateTime), 20, CAST(N'2021-03-02T23:34:00.000' AS DateTime), 20, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (21, 3, N'Mahavir', N'Chavda', N'yiboyak762@onzmail.com', N'M1chavd@', 1, CAST(N'2021-03-03T00:12:45.920' AS DateTime), 21, CAST(N'2021-03-03T00:12:45.920' AS DateTime), 21, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (22, 3, N'Jayvir', N'Vaghela', N'xecax78236@mailnest.net', N'J1vaghel@', 1, CAST(N'2021-03-03T03:03:17.010' AS DateTime), 22, CAST(N'2021-03-03T03:03:17.013' AS DateTime), 22, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1003, 3, N'Viraj', N'Chavda', N'VirajChavda@gmail.com', N'V1chavd@', 1, CAST(N'2021-04-01T18:09:00.000' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1009, 2, N'rajveer', N'chauhan', N'rajveerchauhan@gmail.com', N'admin', 1, CAST(N'2021-04-06T11:27:57.570' AS DateTime), 3, CAST(N'2021-04-06T11:53:25.657' AS DateTime), 3, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1010, 2, N'Admin1', N'test', N'brijendrasinhchavda2018@gmail.com', N'admin', 1, CAST(N'2021-04-10T11:38:18.793' AS DateTime), 3, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_UserProfile]    Script Date: 16-04-2021 14:03:14 ******/
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [IX_UserProfile] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NoteCategories] ADD  CONSTRAINT [DF_NoteCategories_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NoteReviews] ADD  CONSTRAINT [DF_Reviews_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NoteTypes] ADD  CONSTRAINT [DF_NoteTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Reference] ADD  CONSTRAINT [DF_Reference_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SellNoteAttachments] ADD  CONSTRAINT [DF_SellNoteAttachments_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SellNotes] ADD  CONSTRAINT [DF_SellNotes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SystemConfiguration] ADD  CONSTRAINT [DF_SystemConfiguration_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [DF_UserRoles_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsEmailVerified]  DEFAULT ((0)) FOR [IsEmailVerified]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_Downloads] FOREIGN KEY([ID])
REFERENCES [dbo].[Downloads] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_Downloads]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_SellNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellNotes] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_SellNotes]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_Users] FOREIGN KEY([Seller])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_Users]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_Users1] FOREIGN KEY([Downloader])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_Users1]
GO
ALTER TABLE [dbo].[NoteReports]  WITH CHECK ADD  CONSTRAINT [FK_NoteReports_Downloads] FOREIGN KEY([DownloaderID])
REFERENCES [dbo].[Downloads] ([ID])
GO
ALTER TABLE [dbo].[NoteReports] CHECK CONSTRAINT [FK_NoteReports_Downloads]
GO
ALTER TABLE [dbo].[NoteReports]  WITH CHECK ADD  CONSTRAINT [FK_NoteReports_SellNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellNotes] ([ID])
GO
ALTER TABLE [dbo].[NoteReports] CHECK CONSTRAINT [FK_NoteReports_SellNotes]
GO
ALTER TABLE [dbo].[NoteReports]  WITH CHECK ADD  CONSTRAINT [FK_NoteReports_Users] FOREIGN KEY([ReportedByID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteReports] CHECK CONSTRAINT [FK_NoteReports_Users]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_Downloads] FOREIGN KEY([DownloadsID])
REFERENCES [dbo].[Downloads] ([ID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_Downloads]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_SellNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellNotes] ([ID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_SellNotes]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_Users] FOREIGN KEY([ReviewedByID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_Users]
GO
ALTER TABLE [dbo].[SellNoteAttachments]  WITH CHECK ADD  CONSTRAINT [FK_SellNoteAttachments_SellNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellNotes] ([ID])
GO
ALTER TABLE [dbo].[SellNoteAttachments] CHECK CONSTRAINT [FK_SellNoteAttachments_SellNotes]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_Countries] FOREIGN KEY([Country])
REFERENCES [dbo].[Countries] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_Countries]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_NoteCategories] FOREIGN KEY([Category])
REFERENCES [dbo].[NoteCategories] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_NoteCategories]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_NoteTypes] FOREIGN KEY([NoteType])
REFERENCES [dbo].[NoteTypes] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_NoteTypes]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_Reference] FOREIGN KEY([Status])
REFERENCES [dbo].[Reference] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_Reference]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_Users] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_Users]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_Users1] FOREIGN KEY([ActionedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_Users1]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_Users2] FOREIGN KEY([ApprovedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_Users2]
GO
ALTER TABLE [dbo].[SellNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellNotes_Users3] FOREIGN KEY([RejectedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellNotes] CHECK CONSTRAINT [FK_SellNotes_Users3]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Gender] FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([ID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Gender]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRoles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[UserRoles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserRoles]
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetCategoryData]
As
Begin
	select NC.ID,NC.Name As Category,NC.Description,NC.CreatedDate As DateAdded,(UD.FirstName+' '+UD.LastName)As AddedBy
	,NC.IsActive As Active from NoteCategories As NC 
	left join
	(
		select * from Users As U
	)As UD
	on NC.createdby = UD.ID
End
GO
/****** Object:  StoredProcedure [dbo].[GetCountryData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetCountryData]
As
Begin
	select C.ID,C.Name As CountryName,C.CountryCode,C.CreatedDate As DateAdded,(UD.FirstName+' '+UD.LastName)As AddedBy
	,C.IsActive As Active from Countries As C 
	left join
	(
		select * from Users As U
	)As UD
	on C.createdby = UD.ID
End
GO
/****** Object:  StoredProcedure [dbo].[getDahboardData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDahboardData]
AS
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		where S.Status = (Select id from [dbo].[Reference] where Value = 'Approved')

END
GO
/****** Object:  StoredProcedure [dbo].[GetDownloadedNotesData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetDownloadedNotesData]
--declare
@NoteID INT = 0,
@SellerID INT = 0,
@DownloaderID INT = 0
As
Begin
			select D.ID,D.NoteID,D.NoteTitle,D.NoteCategory,D.Downloader As BuyerID,D.Seller AS SellerID,D.IsPaid AS SellType 
			,D.PurchasedPrice,D.AttachmentDownloadDate,D.IsAttachmentDownloaded,
			(UD.FirstName+' '+UD.LastName)AS SellerName,(UD1.FirstName+' '+UD1.LastName)AS BuyerName
			from [NoteMarketPlace].[dbo].[Downloads] As D
			left join 
			(
			select * from [NoteMarketPlace].[dbo].[Users] As U
			)As UD
			on D.Seller = UD.ID
			left join 
			(
			select * from [NoteMarketPlace].[dbo].[Users] As U
			)As UD1
			on D.Downloader = UD1.ID
			
		
		
		--where ((D.Seller = @SellerID OR @SellerID = 0) And D.IsAttachmentDownloaded = 1) 
		--And ((D.Downloader = @DownloaderID OR @DownloaderID = 0) And D.IsAttachmentDownloaded = 1)
		--And ((D.NoteID = @NoteID OR @NoteID = 0) And D.IsAttachmentDownloaded = 1)

		where (D.Seller = @SellerID OR @SellerID = 0)
		And (D.Downloader = @DownloaderID OR @DownloaderID = 0)
		And (D.NoteID = @NoteID OR @NoteID = 0)
		And (D.IsAttachmentDownloaded = 1)
		
				
End
GO
/****** Object:  StoredProcedure [dbo].[GetMembersData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetMembersData]
--declare

As
Begin
	select U.ID,U.FirstName,U.LastName,U.EmailID,U.CreatedDate,NUR.NotesUnderReview,PN.PublishedNotes,
	 DU.DownloadedNote,DU1.TotalExpenses,DU2.TotalEarnings from [NoteMarketPlace].[dbo].[Users] As U
		left join
		(
		select D.Downloader,Count(D.ID)As DownloadedNote  from 
		[NoteMarketPlace].[dbo].[Downloads] As D where D.IsSellerHasAllowedDownloads = 1 and IsAttachmentDownloaded =1  group by D.Downloader
		) As DU
		on U.ID = DU.Downloader
		left join
		(
		select D1.Downloader,sum(D1.PurchasedPrice) As TotalExpenses from 
		[NoteMarketPlace].[dbo].[Downloads] As D1 where D1.IsSellerHasAllowedDownloads = 1   group by D1.Downloader
		) As DU1
		on U.ID = DU1.Downloader
		left join
		(
		select D2.Seller,sum(D2.PurchasedPrice) As TotalEarnings from 
		[NoteMarketPlace].[dbo].[Downloads] As D2 where D2.IsSellerHasAllowedDownloads = 1   group by D2.Seller
		) As DU2
		on U.ID = DU2.Seller
		left join
		(
		select count(SN.ID)As NotesUnderReview,SN.SellerID from [NoteMarketPlace].[dbo].[SellNotes] As SN 
		where (SN.Status = (select id from [NoteMarketPlace].[dbo].[Reference] where value='Submitted For Review') )
		OR (SN.Status =(select id from [NoteMarketPlace].[dbo].[Reference] where value='In Review') ) group by SN.SellerID
		)As NUR
		on U.ID = NUR.SellerID
		left join
		(
		select count(SN.ID)As PublishedNotes,SN.SellerID from [NoteMarketPlace].[dbo].[SellNotes] As SN 
		where (SN.Status = (select id from [NoteMarketPlace].[dbo].[Reference] where value='Approved') ) group by SN.SellerID
		)As PN
		on U.ID = PN.SellerID
		
		where U.RoleID = (select id from [NoteMarketPlace].[dbo].[UserRoles] where Name = 'Member')
		--where PN.SellerID = U.ID
End
GO
/****** Object:  StoredProcedure [dbo].[GetMembersDetails]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetMembersDetails]
--declare
@ID INT = 0
As
Begin
	select S.ID,S.Title,(select NC.Name from [NoteMarketPlace].[dbo].[NoteCategories] As NC where S.Category = NC.ID) As Category
		,(select St.Value from [NoteMarketPlace].[dbo].[Reference] As St where S.Status = St.ID)As Status
		,S.CreatedDate,S.PublishedDate,DD.NumberOfDownload,
		DD1.TotalEarnings
		from [NoteMarketPlace].[dbo].[SellNotes] As S
		left join
		(
			select noteID,count(D.NoteID) As NumberOfDownload from [NoteMarketPlace].[dbo].[Downloads] As D
			where D.IsAttachmentDownloaded = 1 
			  group by D.NoteID 
			
		)As DD
		on S.ID = DD.NoteID 
		left join
		(
			select noteID,sum(D1.PurchasedPrice)As TotalEarnings from [NoteMarketPlace].[dbo].[Downloads] AS D1 
			where D1.IsSellerHasAllowedDownloads = 1 group by D1.NoteID
		) As DD1
		on S.ID = DD1.NoteID

		where (S.SellerID = @ID OR @ID = 0 And S.Status = (select St.ID from [NoteMarketPlace].[dbo].[Reference] As St where St.Value ='Approved')) OR
				(S.SellerID = @ID OR @ID = 0 And S.Status = (select St.ID from [NoteMarketPlace].[dbo].[Reference] As St where St.Value ='Submitted For Review')) OR
				(S.SellerID = @ID OR @ID = 0 And S.Status = (select St.ID from [NoteMarketPlace].[dbo].[Reference] As St where St.Value ='In Review'))

End
GO
/****** Object:  StoredProcedure [dbo].[GetNotesUnderReviewData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetNotesUnderReviewData]
--declare
@data varchar(200) = null
As
Begin
	Select S.ID,SellerID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,
		S.CreatedDate As DateAdded,(U.FirstName+' '+U.LastName) As Seller,
		(select St.Value from [dbo].[Reference] As St where S.Status = St.ID) As Status from [dbo].[SellNotes] As S
		
			LEFT JOIN
			(
			SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
			) AS U
			ON S.SellerID = U.ID
			where	 ((S.SellerID = @data OR @data is null) And
					(S.Status = (Select id from [dbo].[Reference] where Value = 'Submitted For Review'))) OR 
					((S.SellerID = @data OR @data is null) And
					(S.Status = (Select id from [dbo].[Reference] where Value = 'In Review')))
End
GO
/****** Object:  StoredProcedure [dbo].[GetNoteTypeData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetNoteTypeData]
As
Begin
	select NT.ID,NT.Name As Type,NT.Description,NT.CreatedDate As DateAdded,(UD.FirstName+' '+UD.LastName)As AddedBy
	,NT.IsActive As Active from NoteTypes As NT 
	left join
	(
		select * from Users As U
	)As UD
	on NT.createdby = UD.ID
End
GO
/****** Object:  StoredProcedure [dbo].[GetPublishedNotesData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetPublishedNotesData]
--declare
@data varchar(200) = null
As
Begin
	Select S.ID,S.SellerID,S.Title,S.IsPaid,S.SellingPrice,(select CG.Name from [NoteMarketPlace].[dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,
		S.PublishedDate,(U.FirstName+' '+U.LastName) As Seller,STD.Value,NOD.numberOfDownload,
		(select (UD.FirstName+' '+UD.LastName) As ApprovedBy from [NoteMarketPlace].[dbo].[Users] As UD where S.ApprovedBy = UD.ID) As ApprovedBy
		 from [NoteMarketPlace].[dbo].[SellNotes] As S
		
			 left JOIN
			(
			SELECT UP.ID,UP.FirstName,UP.LastName FROM [NoteMarketPlace].[dbo].[Users] AS UP 
			) AS U
			ON S.SellerID = U.ID
			left join 
			(
			select St.Value,St.ID from [NoteMarketPlace].[dbo].[Reference] As St where St.Value = 'Approved'
			) As STD
			on S.Status = STD.ID
			left join
			(
				select Count(NoteID) As NumberOfDownload,NoteID from [NoteMarketPlace].[dbo].[Downloads] where IsAttachmentDownloaded = 1
				group by NoteID 
				
			) As NOD
			on S.ID = NOD.NoteID

			where (S.SellerID = @data OR @data is null) And (S.Status = (select id from [NoteMarketPlace].[dbo].[Reference] where Value = 'Approved'))
End
GO
/****** Object:  StoredProcedure [dbo].[GetRejectedNotesData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetRejectedNotesData]
--declare
@data varchar(200) = null
As
Begin
	Select S.ID,S.SellerID,S.Title,(select CG.Name from [NoteMarketPlace].[dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,
		S.ModifiedDate As DateEdited,(U.FirstName+' '+U.LastName) As Seller,S.AdminRemarks As Remarks,
		(select (UD.FirstName+' '+UD.LastName) from [NoteMarketPlace].[dbo].[Users] As UD where S.RejectedBy = UD.ID) As RejectedBy
		 from [NoteMarketPlace].[dbo].[SellNotes] As S
		
			 left JOIN
			(
			SELECT UP.ID,UP.FirstName,UP.LastName FROM [NoteMarketPlace].[dbo].[Users] AS UP 
			) AS U
			ON S.SellerID = U.ID
			
			where (S.SellerID = @data OR @data is null) And (S.Status = (select id from [NoteMarketPlace].[dbo].[Reference] where Value = 'Rejected'))
			
End
GO
/****** Object:  StoredProcedure [dbo].[NewGetDashboadrd]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER procedure [dbo].[getDahboardData]
--declare 
--@data INT = 0
--AS
CREATE procedure [dbo].[NewGetDashboadrd]
@data INT = 0
As
Begin
	IF(@data = 0)
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.AttachmentSize,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN 
		where NVN.IsAttachmentDownloaded = 1
		GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		
		where (S.Status = (Select id from [dbo].[Reference] where Value = 'Approved'))
			And (S.PublishedDate >= Dateadd(Month, Datediff(Month, 0, DATEADD(m, -0,
			current_timestamp)), 0)) order by S.PublishedDate desc
	
END
-- for last month
IF(@data = 1)
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.AttachmentSize,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN 
		where NVN.IsAttachmentDownloaded = 1
		GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		
		where (S.Status = (Select id from [dbo].[Reference] where Value = 'Approved'))
			And (S.PublishedDate >= Dateadd(Month, Datediff(Month, 0, DATEADD(m, -1,
			current_timestamp)), 0)) order by S.PublishedDate desc
	
END

--for last two months
IF(@data = 2)
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.AttachmentSize,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN 
		where NVN.IsAttachmentDownloaded = 1
		GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		
		where (S.Status = (Select id from [dbo].[Reference] where Value = 'Approved'))
			And (S.PublishedDate >= Dateadd(Month, Datediff(Month, 0, DATEADD(m, -2,
			current_timestamp)), 0)) order by S.PublishedDate desc
	
END

--for last three months
IF(@data = 3)
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.AttachmentSize,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN 
		where NVN.IsAttachmentDownloaded = 1
		GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		
		where (S.Status = (Select id from [dbo].[Reference] where Value = 'Approved'))
			And (S.PublishedDate >= Dateadd(Month, Datediff(Month, 0, DATEADD(m, -3,
			current_timestamp)), 0)) order by S.PublishedDate desc
	
END

--for last four months
IF(@data = 4)
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.AttachmentSize,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN 
		where NVN.IsAttachmentDownloaded = 1
		GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		
		where (S.Status = (Select id from [dbo].[Reference] where Value = 'Approved'))
			And (S.PublishedDate >= Dateadd(Month, Datediff(Month, 0, DATEADD(m, -4,
			current_timestamp)), 0)) order by S.PublishedDate desc
	
END

--for last five months
IF(@data = 5)
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.AttachmentSize,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN 
		where NVN.IsAttachmentDownloaded = 1
		GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		
		where (S.Status = (Select id from [dbo].[Reference] where Value = 'Approved'))
			And (S.PublishedDate >= Dateadd(Month, Datediff(Month, 0, DATEADD(m, -5,
			current_timestamp)), 0)) order by S.PublishedDate desc
	
END

--for last six months
IF(@data = 6)
Begin
	Select S.ID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,S.IsPaid,
	S.AttachmentSize,
	S.SellingPrice,S.PublishedDate,DS.NumberOfDownload,(U.FirstName+' '+U.LastName) As Publisher from [dbo].[SellNotes] As S
		LEFT JOIN 
		(
		SELECT NVN.NoteID,COUNT(NVN.ID) As NumberOfDownload FROM [dbo].[Downloads] AS NVN 
		where NVN.IsAttachmentDownloaded = 1
		GROUP BY NVN.NoteID
		) AS DS
		ON S.ID = DS.NoteID
		LEFT JOIN
		(
		SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
		) AS U
		ON S.SellerID = U.ID
		
		where (S.Status = (Select id from [dbo].[Reference] where Value = 'Approved'))
			And (S.PublishedDate >= Dateadd(Month, Datediff(Month, 0, DATEADD(m, -6,
			current_timestamp)), 0)) order by S.PublishedDate desc
	 
END

END
GO
/****** Object:  StoredProcedure [dbo].[NewGetMembersData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[NewGetMembersData]
--declare

As
Begin
	select U.ID,U.FirstName,U.LastName,U.EmailID,U.CreatedDate,NUR.NotesUnderReview,PN.PublishedNotes,
	 DU.DownloadedNote,DU1.TotalExpenses,DU2.TotalEarnings from [NoteMarketPlace].[dbo].[Users] As U
		left join
		(
		select D.Downloader,Count(D.ID)As DownloadedNote  from 
		[NoteMarketPlace].[dbo].[Downloads] As D where D.IsSellerHasAllowedDownloads = 1 and IsAttachmentDownloaded =1  group by D.Downloader
		) As DU
		on U.ID = DU.Downloader
		left join
		(
		select D1.Downloader,sum(D1.PurchasedPrice) As TotalExpenses from 
		[NoteMarketPlace].[dbo].[Downloads] As D1 where D1.IsSellerHasAllowedDownloads = 1   group by D1.Downloader
		) As DU1
		on U.ID = DU1.Downloader
		left join
		(
		select D2.Seller,sum(D2.PurchasedPrice) As TotalEarnings from 
		[NoteMarketPlace].[dbo].[Downloads] As D2 where D2.IsSellerHasAllowedDownloads = 1   group by D2.Seller
		) As DU2
		on U.ID = DU2.Seller
		left join
		(
		select count(SN.ID)As NotesUnderReview,SN.SellerID from [NoteMarketPlace].[dbo].[SellNotes] As SN 
		where (SN.Status = (select id from [NoteMarketPlace].[dbo].[Reference] where value='Submitted For Review') )
		OR (SN.Status =(select id from [NoteMarketPlace].[dbo].[Reference] where value='In Review') ) group by SN.SellerID
		)As NUR
		on U.ID = NUR.SellerID
		left join
		(
		select count(SN.ID)As PublishedNotes,SN.SellerID from [NoteMarketPlace].[dbo].[SellNotes] As SN 
		where (SN.Status = (select id from [NoteMarketPlace].[dbo].[Reference] where value='Approved') ) group by SN.SellerID
		)As PN
		on U.ID = PN.SellerID
		
		where U.RoleID = (select id from [NoteMarketPlace].[dbo].[UserRoles] where Name = 'Member')
		--where PN.SellerID = U.ID
End
GO
/****** Object:  StoredProcedure [dbo].[NewGetMembersDetails]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[NewGetMembersDetails]
--declare
@ID INT = 0
As
Begin
	select S.ID,S.Title,(select NC.Name from [NoteMarketPlace].[dbo].[NoteCategories] As NC where S.Category = NC.ID) As Category
		,(select St.Value from [NoteMarketPlace].[dbo].[Reference] As St where S.Status = St.ID)As Status
		,S.CreatedDate,S.PublishedDate,DD.NumberOfDownload,
		DD1.TotalEarnings
		from [NoteMarketPlace].[dbo].[SellNotes] As S
		left join
		(
			select noteID,count(D.NoteID) As NumberOfDownload from [NoteMarketPlace].[dbo].[Downloads] As D
			where D.IsAttachmentDownloaded = 1 
			  group by D.NoteID 
			
		)As DD
		on S.ID = DD.NoteID 
		left join
		(
			select noteID,sum(D1.PurchasedPrice)As TotalEarnings from [NoteMarketPlace].[dbo].[Downloads] AS D1 
			where D1.IsSellerHasAllowedDownloads = 1 group by D1.NoteID
		) As DD1
		on S.ID = DD1.NoteID

		where ((S.SellerID = @ID OR @ID = 0) And (S.Status = (select St.ID from [NoteMarketPlace].[dbo].[Reference] As St where St.Value ='Approved'))) OR
				((S.SellerID = @ID OR @ID = 0) And (S.Status = (select St.ID from [NoteMarketPlace].[dbo].[Reference] As St where St.Value ='Submitted For Review'))) OR
				((S.SellerID = @ID OR @ID = 0) And (S.Status = (select St.ID from [NoteMarketPlace].[dbo].[Reference] As St where St.Value ='In Review')))

End
GO
/****** Object:  StoredProcedure [dbo].[NewGetNotesUnderReviewData]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[NewGetNotesUnderReviewData]
@data varchar(200) = null
As
Begin
	Select S.ID,SellerID,S.Title,(select CG.Name from [dbo].[NoteCategories] As CG where S.Category = CG.ID) As Category,
		S.CreatedDate As DateAdded,(U.FirstName+' '+U.LastName) As Seller,
		(select St.Value from [dbo].[Reference] As St where S.Status = St.ID) As Status from [dbo].[SellNotes] As S
		
			LEFT JOIN
			(
			SELECT UP.ID,UP.FirstName,UP.LastName FROM [dbo].[Users] AS UP 
			) AS U
			ON S.SellerID = U.ID
			where	 ((S.SellerID = @data OR @data is null) And
					(S.Status = (Select id from [dbo].[Reference] where Value = 'Submitted For Review'))) OR 
					((S.SellerID = @data OR @data is null) And
					(S.Status = (Select id from [dbo].[Reference] where Value = 'In Review')))
End
GO
/****** Object:  StoredProcedure [dbo].[NewGetSellerNotesDetails]    Script Date: 16-04-2021 14:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[NewGetSellerNotesDetails]
--declare
@FK_Type int = 0,
@FK_Category int = 0,
@FK_Country int = 0,
@FK_University nvarchar(200) = null,
@FK_Course nvarchar(200) = null,
@PageSize INT = 2,
@PageNumber INT = 1,
@Search nvarchar(200) = null,
@Rating nvarchar(100) = null
As 
Begin 
	SELECT S.ID,S.SellerID,S.DisplayPicture,S.Title,S.Category,S.Country,S.CreatedDate,S.UniversityName,S.NumberOfPages,S.PublishedDate,(SELECT C.Name FROM Countries AS C WHERE C.ID = S.Country) AS CountyName,
	(SELECT COUNT(SN.ID) FROM SellNotes AS SN 
		LEFT JOIN 
		(
		SELECT AVG(NVN.ratings) AS RatingNew,NVN.NoteID FROM [dbo].[NoteReviews] AS NVN GROUP BY NVN.NoteID
		) AS DSN
		ON SN.ID = DSN.NoteID
		WHERE (SN.NoteType = @FK_Type OR @FK_Type = 0) AND (SN.Category = @FK_Category OR @FK_Category = 0)
			And (SN.Country = @FK_Country OR @FK_Country = 0) AND (SN.UniversityName = @FK_University OR @FK_University is null)
			And (SN.Course = @FK_Course OR @FK_Course is null) AND (Status = (select id from Reference where Value = 'Approved'))
			And (SN.Title Like '%'+@Search+'%' OR @Search is null) And (DSN.RatingNew >= @Rating OR @Rating is null)) AS TotalCount,DM.Report,DS.Rating,DS.TotalRating from SellNotes AS S
	LEFT JOIN 
	(
	SELECT COUNT(NR.NoteID) AS Report,NR.NoteID FROM [dbo].[NoteReports] AS NR GROUP BY NR.NoteID
	) AS DM
	ON S.ID = DM.NoteID
	LEFT JOIN 
	(
	SELECT AVG(NV.ratings) AS Rating, COUNT(NV.Ratings) As TotalRating,NV.NoteID FROM [dbo].[NoteReviews] AS NV GROUP BY NV.NoteID
	) AS DS
	ON S.ID = DS.NoteID
	 WHERE (S.NoteType = @FK_Type OR @FK_Type = 0) AND (S.Category = @FK_Category OR @FK_Category = 0)
			And (S.Country = @FK_Country OR @FK_Country = 0) AND (S.UniversityName = @FK_University OR @FK_University is null)
			And (S.Course = @FK_Course OR @FK_Course is null) AND (Status = (select id from Reference where Value = 'Approved'))
			And (S.Title Like '%'+@Search+'%' OR @Search is null) And (DS.Rating >= @Rating OR @Rating is null)
	ORDER BY S.ID DESC
	OFFSET (@PageNumber - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY

End
GO
USE [master]
GO
ALTER DATABASE [NoteMarketPlace] SET  READ_WRITE 
GO
