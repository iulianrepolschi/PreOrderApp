USE [master]
GO

/****** Object:  Database [PreOrderApp]    Script Date: 11/22/2012 00:24:53 ******/
CREATE DATABASE [PreOrderApp] ON  PRIMARY 
( NAME = N'aspnet-PreOrderApp-20121121162018', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\aspnet-PreOrderApp-20121121162018.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'aspnet-PreOrderApp-20121121162018_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\aspnet-PreOrderApp-20121121162018_log.LDF' , SIZE = 504KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [PreOrderApp] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PreOrderApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PreOrderApp] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PreOrderApp] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PreOrderApp] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PreOrderApp] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PreOrderApp] SET ARITHABORT OFF 
GO

ALTER DATABASE [PreOrderApp] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [PreOrderApp] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [PreOrderApp] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PreOrderApp] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PreOrderApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PreOrderApp] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PreOrderApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PreOrderApp] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PreOrderApp] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PreOrderApp] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PreOrderApp] SET  ENABLE_BROKER 
GO

ALTER DATABASE [PreOrderApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PreOrderApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PreOrderApp] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PreOrderApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PreOrderApp] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PreOrderApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PreOrderApp] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PreOrderApp] SET  READ_WRITE 
GO

ALTER DATABASE [PreOrderApp] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PreOrderApp] SET  MULTI_USER 
GO

ALTER DATABASE [PreOrderApp] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PreOrderApp] SET DB_CHAINING OFF 
GO

