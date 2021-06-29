USE master
GO
IF DB_ID('NETFLIX_DB') IS NOT null
DROP DATABASE NETFLIX_DB
GO
CREATE DATABASE NETFLIX_DB
GO
USE NETFLIX_DB
GO

CREATE TABLE Account_Type(
	ID int identity(1,1) Primary Key ,
	name nvarchar(50) 
)


CREATE TABLE Category(
	id int identity(1,1) Primary Key,
	name nvarchar(50) 
)


CREATE TABLE Celebrites(
	id int identity(1,1) Primary Key,
	name nvarchar(50) ,
	DOB date 
)


CREATE TABLE Movie_Infomation(
	id int identity(1,1) Primary Key,
	Detail nvarchar(200) ,
	length time(7) ,
	rate float ,
	Distribute_Year date ,
	Country nchar(50) 
)

/****** Object:  Table [dbo].[Admin]    Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE [dbo].[Admin](
	[ID] int Primary key,
	[name] [nvarchar](255) NULL,
	[active] [int] NULL,
	[created] [date] NULL,
)
/****** Object:  Table [dbo].[Feature]    Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE [dbo].[Feature](
	[ID] [int] Primary key,
	[name] [nvarchar](255) NULL,
	[money] [money] NULL,
	[active] [int] NULL,
	[created] [date] NULL,
) 
/****** Object:  Table [dbo].[Package]    Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE [dbo].[Package](
	[ID] [int] Primary key,
	[name] [nvarchar](255) NULL,
	[money] [money] NULL,
	[active] [int] NULL,
	[created] [date] NULL,
) 
/****** Object:  Table [dbo].[Member]    Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE Member(
	[ID] [int] Primary key,
	[name] [nvarchar](50) NULL,
	[phone] [char](10) NULL,
	[DOB] [date] NULL,
	[active] [int] NULL,
	[adress] [nvarchar](255) NULL,
	[created] [date] NULL,
	StartDay date NULL,
	EndDay date NULL,
	ID_Package int,
	Foreign key(ID_Package) References Package(ID),
) 

CREATE TABLE Account(
	Email nchar(50) Primary Key,
	Password nchar(50) ,
	Type int ,
	Information int ,

	FOREIGN KEY(TYPE) REFERENCES Account_Type(ID),
	FOREIGN KEY(Information) References Admin(ID),
	Foreign key(Information) References Member(ID),
	
)

/****** Object:  Table [dbo].[Permission]    Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE [dbo].[Permission](
	[ID] [int] Primary key,
	[name] [nvarchar](255) NULL,
	[active] [int] NULL,
	[created] date NULL,
) 
/****** Object:  Table [dbo].[Admin_Permission] Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE [dbo].[Admin_Permission](
	[ID] [int] Primary key,
	ID_Admin int,
	ID_Permission int,

	Foreign key(ID_Admin) References Admin(ID),
	Foreign key(ID_Permission) References Permission(ID),
) 

/****** Object:  Table [dbo].[Feature_pack]    Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE [dbo].[Feature_pack](
	[ID] [int] Primary key,
	[ID_Package] [int] NULL,
	[ID_Feature] [int] NULL,
	[active] [int] NULL,
	[created] [date] NULL,
	Foreign key(ID_Package) References Package(ID),
	Foreign key(ID_Feature) References Feature(ID),
) 
/****** Object:  Table [dbo].[Bill]    Script Date: 6/10/2021 4:44:44 PM ******/
CREATE TABLE [dbo].[Bill](
	[ID] [int] Primary key,
	[ID_Member] [int] NULL,
	[ID_Package] [int] NULL,
	[money] [money] NULL,
	[created] [date] NULL,
	Foreign key(ID_Package) References Package(ID),
	Foreign key(ID_Member) References Member(ID),
)

CREATE TABLE Movie(
	id int  identity(1,1) Primary Key,
	name nvarchar(50) ,
	information int ,
	poster nchar(200) ,
	video nchar(200) ,
	audio nchar(200)
	
	FOREIGN KEY(information) REFERENCES Movie_Infomation(ID) 
)


CREATE TABLE Classify(
	Category_ID int ,
	Movie_ID int
	
	PRIMARY KEY (Movie_ID,Category_ID)

	FOREIGN KEY(Category_ID) REFERENCES Category(ID),
	FOREIGN KEY(Movie_ID) REFERENCES Movie(ID) 	 
)


CREATE TABLE Credits(
	Movies_ID int,
	Person_ID int,
	Year date ,
	role nvarchar(50) 

	PRIMARY KEY (Movies_ID,Person_ID)

	FOREIGN KEY(Person_ID) REFERENCES Celebrites(ID),
	FOREIGN KEY(Movies_ID) REFERENCES Movie(ID) 

)

CREATE TABLE Favourite_Movies(
	Username nchar(50) ,
	Movie_ID int
	
	PRIMARY KEY (Username,Movie_ID)

	FOREIGN KEY(Username) REFERENCES Account(Email),
	FOREIGN KEY(Movie_ID) REFERENCES Movie(ID)  
)


