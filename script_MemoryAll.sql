USE [master]
GO
/****** Object:  Database [Memory]    Script Date: 1/14/2015 8:45:49 AM ******/
CREATE DATABASE [Memory]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Memory', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Memory.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Memory_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Memory_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Memory] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Memory].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Memory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Memory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Memory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Memory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Memory] SET ARITHABORT OFF 
GO
ALTER DATABASE [Memory] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Memory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Memory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Memory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Memory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Memory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Memory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Memory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Memory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Memory] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Memory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Memory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Memory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Memory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Memory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Memory] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Memory] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Memory] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Memory] SET  MULTI_USER 
GO
ALTER DATABASE [Memory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Memory] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Memory] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Memory] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Memory] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Memory]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 1/14/2015 8:45:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateView] [datetime] NOT NULL,
	[NextDateView] [datetime] NOT NULL,
	[Question] [varchar](max) NOT NULL,
	[Note] [varchar](max) NOT NULL,
	[NumView] [int] NOT NULL,
 CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Addmemo]    Script Date: 1/14/2015 8:45:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Creazione STORED PROCEDURE
CREATE PROCEDURE [dbo].[Addmemo]
	-- AGGIUNTA DEI PARAMETRI NELLA PROCEDURA

	@DateView VARCHAR(50),
	@Question varchar(MAX),
	@Note varchar(MAX),
	@Numview int
	

AS
	
BEGIN
	
	SET NOCOUNT ON;

	
	insert into Notes(DateView,NextDateView, Question, Note, NumView) VALUES (@DateView,(DATEADD (SECOND, 20,GETDATE())),@Question,@Note,@Numview);
	
	
END


GO
/****** Object:  StoredProcedure [dbo].[NoteView]    Script Date: 1/14/2015 8:45:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Creazione STORED PROCEDURE
CREATE PROCEDURE [dbo].[NoteView]
	-- AGGIUNTA DEI PARAMETRI NELLA PROCEDURA
	
AS
	
BEGIN
	
	SET NOCOUNT ON;

	
	--select info from Promemoria where((sum(Datepart(minute,([dataPromemoria]-[numViste]))))=@dataAdesso);
	--select info from Promemoria --where(GETDATE()<=dataPromemoria);
	select	 Id, Question, Note  from Notes where((GETDATE()>= NextDateView) AND (NextDateView=( select MIN(NextDateView) from Notes)));
	
	-- UPDATE Notes SET NextDateView = DATEADD(SECOND, NumView+5, GETDATE()), NumView=NumView*2 WHERE Note= (select	 Note from Notes where(GETDATE()>= NextDateView AND NextDateView=( select MIN(NextDateView) from Notes)));
	
	END
	
	
GO
USE [master]
GO
ALTER DATABASE [Memory] SET  READ_WRITE 
GO
