--Author: NTS--

USE master
GO
IF DB_ID('NETFLIX_DB') IS NOT null
DROP DATABASE NETFLIX_DB
GO
CREATE DATABASE NETFLIX_DB
GO
USE NETFLIX_DB
GO

create table ACCOUNT_TYPE
(
	ID int identity(1, 1),
	NAME nvarchar(50) NOT NULL,

	constraint PK_ACCTYPE primary key (ID)
)

create table CELEBRITIES
(
	ID int identity(1, 1),
	NAME nvarchar(50) NOT NULL,
	DATEOFBIRTH date,
	--TYPE == 1 ==> ACTOR
	--TYPE == 2 ==> DIRECTOR
	TYPE int NOT NULL,

	constraint PK_CELEBRITIES primary key (ID)
)

create table MOVIE_TYPE
(
	ID int identity(1, 1),
	NAME nvarchar(50) not null,

	constraint PK_MOVIE_TYPE primary key (ID)
)

create table CATEGORY
(
	ID int identity(1, 1),
	NAME nvarchar(50) NOT NULL,

	constraint PK_CATEGORY primary key (ID)
)

create table MOVIE
(
	ID int identity(1, 1),
	NAME nvarchar(100) UNIQUE NOT NULL,
	INFORMATION int not null,
	POSTER nchar(50) UNIQUE,
	TRAILER_NAME nchar(50) NOT NULL,
	MOVIE_NAME nchar(50) NOT NULL,
	TYPE_ID int not null,
	CATEGORY_ID int not null,
	ACTIVE int NOT NULL,
	VIEWS int DEFAULT 0,
	constraint PK_MOVIE primary key (ID)
)

create table MOVIE_INFORMATION
(
	ID int identity(1, 1),
	DESCRIPTION nvarchar(200),
	LENGTH int,
	RATE float,
	DISTRIBUTE_YEAR int,
	COUNTRY nchar(50),

	constraint PK_MOVIEINFOR primary key (ID)
)

create table USER_INFORMATION
(
	ID int identity(1, 1),
	NAME nvarchar(50) NOT NULL,
	PHONE nchar(20) UNIQUE,
	ADDRESS nvarchar(50) NULL,
	DATEOFBIRTH date NULL,
	GENDER nvarchar(10) NULL,

	constraint PK_USRINFOR primary key (ID)
)

create table ACCOUNT
(
	ID int identity(1, 1),
	EMAIL nchar(50) NOT NULL UNIQUE,
	PASSWORD nchar(50) NOT NULL,
	-- 1: User, 2: Admin
	TYPE int NOT NULL,
	INFORMATION int NOT NULL,
	-- 0: BLocked, 1: Active
	ACTIVE int NOT NULL,

	constraint PK_ACCOUNT primary key (ID)
)

create table CREDITS
(
	MOVIE_ID int NOT NULL,
	PERSON_ID int NOT NULL,
	ROLE nvarchar(50) NOT NULL,

	constraint PK_CREDITS primary key (MOVIE_ID, PERSON_ID)
)

create table FAVOURITE_MOVIES
(
	USER_ID int NOT NULL,
	MOVIE_ID int NOT NULL,
	ACTIVE int NOT NULL,

	constraint PK_FAVMVS primary key (USER_ID, MOVIE_ID)
)

create table PACKAGE
(
	ID int identity(1, 1),
	NAME nvarchar(50),
	PRICE float,
	TIME int,
	Unit nvarchar(10),

	constraint PK_PACKAGE primary key (ID)
)

create table PURCHASE
(
	ID int identity(1, 1),
	PURCHASED_DATE date NOT NULL,
	OUTOFDATE date not null,
	PACKAGE_ID int,
	USER_ID int,

	constraint PK_PURINF primary key (ID)
)

alter table FAVOURITE_MOVIES
add constraint FK_FM_ACCOUNT foreign key (USER_ID) references ACCOUNT (ID)

alter table FAVOURITE_MOVIES
add constraint FK_FM_MOVIE foreign key (MOVIE_ID) references MOVIE (ID)

alter table ACCOUNT
add constraint FK_ACCOUNT_ACCTYPE foreign key (TYPE) references ACCOUNT_TYPE (ID)

alter table ACCOUNT
add constraint FK_ACCOUNT_INFO foreign key (INFORMATION) references USER_INFORMATION (ID)

alter table PURCHASE
add constraint FK_PUR_ACCOUNT foreign key (USER_ID) references ACCOUNT (ID)

alter table PURCHASE
add constraint FK_PUR_PACKAGE foreign key (PACKAGE_ID) references PACKAGE (ID)

alter table MOVIE
add constraint FK_MOVIE_INFO foreign key (INFORMATION) references MOVIE_INFORMATION (ID)

alter table MOVIE
add constraint FK_MOVIE_CATEGORY foreign key (CATEGORY_ID) references CATEGORY (ID)

alter table MOVIE
add constraint FK_MOVIE_TYPE foreign key (TYPE_ID) references MOVIE_TYPE (ID)

alter table CREDITS
add constraint FK_CRE_MOVIE foreign key (MOVIE_ID) references MOVIE(ID)

alter table CREDITS
add constraint FK_CRE_CELEB foreign key (PERSON_ID) references CELEBRITIES(ID)

insert into ACCOUNT_TYPE
--		  Name
values	('User'),
		('Admin')


insert into PACKAGE
--			Name			Price			Time
values		('Basic',		120000,			1,			'Tuan'),
			('Standard',	220000,			1,			'Thang'),
			('Premium',		300000,			6,			'Thang')

insert into CATEGORY
--			Name
values	(N'Hành động'),
		(N'Hài'),
		(N'Kinh dị'),
		(N'Khoa học viễn tưởng'),
		(N'Sử thi'),
		(N'Tình cảm'),
		(N'Phiêu lưu')

insert into MOVIE_TYPE
--			Name
values	('Kids'),
		(N'Phim lẻ'),
		('Game show'),
		(N'Phim chiếu rạp'),
		(N'Phim bộ')

insert into MOVIE_INFORMATION
--																						Discription															Length	Rate	Dis_Year	Country
values	('On the cusp of the year 2000, Colombian brothers Carly and Mateo prepare to move to the US for their last years of high school...',				145,	8.5,	2021,		'Germany'),
		('Filmed over five year, Chasing Wonders is a herat-warming story of a young boy, who, encouraged by his grandfather to live a life of hope...',	129,	8,		2020,		'U.K'),
		('Billy Walker is finally let out of prison after serving a seven-year sentence for manslaughter, only to find out that the only person...',		129,	7.5,	2019,		'Canada'),
		('Hoping to reclaim his former glory, a fallen dominoes champion recruits his step-grandson to help him win an off-the-wall tournament',			149,	8,		2018,		'India'),
		('The film tells the inspiring true story of Dream Alliance, an unlikely race horse bred by small town bartender, Jan Vokes.',						153,	8,		2017,		'Mexico'),
		('Adrift in space with no food or water, Tony Stark sends a message to Pepper Potts as his oxygen supply starts to dwindle.',						301,	9.5,	2019,		'U.S.A'),
		('Fredrick Fitzell is living his best live - until he starts having horrific visions of Cindy, a girl who vanished in high school',					138,	8,		2008,		'Dubai'),
		('In Free Guy, a back teller who discovers he is actually a background player in an open-world video game, decides to become the hero...',			155,	8.5,	2006,		'U.S.A'),
		('Down-and-out backup singer and celebrity ex-husband kasper is invited to complete in the FunHouse, an online Big Brother style reality show',		146,	7.5,	2001,		'China'),
		('A teenage coding prodigy has 30 days to create the world greatest video game and save his family business.',										129,	8,		2000,		'Vietnam'),
		('The world most lethal odd couple - bodyguard Michael Brycce and hitman Darius Kincaid -- are back on another life-threatening...',				139,	7,		2001,		'Korea'),
		('Terror strikes when beachgoers suddenly realize they are aging rapidly',																			235,	6.5,	1999,		'Canada'),
		('After getting kicked out of his home in central Pennsylvania, Paul arrives to NYC dizzying central station with nowhere to go...',				141,	7,		2005,		'Australia'),
		('While her house undergoes repairs, fiercely independent senior Helen temporarily moves into a nearby retirement community...',					140,	7,		2008,		'France'),
		('When Lebron James and his young son Dom are trapped in a digital space by a rogue A.I., LeBron must get them home safe by leading Bugs, ...',		185,	8,		2007,		'Italy')

insert into MOVIE
values	--Name							INFO_ID		POSTER_NAME			TRAILER_NAME							MOVIE_NAME						TYPE_ID		CATEGORY_ID		IS_ACTIVE
		('Blast Beat',					'0001', '0001_poster.jpg', 'BlastBeat_Trailer.mov',					'BlastBeat_Film.mov',					1,			1,				1,		0),
		('Chasing Wonders',				'0002', '0002_poster.jpg', 'ChasingWonders_Trailer.mov',			'ChasingWonders_Film.mov',				3,			3,				1,		0),
		('Death in Texas',				'0003', '0003_poster.jpg', 'DeathInTexas_Trailer.mov',				'DeathInTexas_Film.mov',				4,			4,				1,		0),
		('Domino Battle of the Bones',	'0004', '0004_poster.jpg', 'DominoBattleOfTheBones_Trailer.mov',	'DominoBattleOfTheBones_Film.mov',		5,			5,				1,		0),
		('Dream Horse',					'0005', '0005_poster.jpg', 'DreamHorse_Trailer.mov',				'DreamHorse_Film.mov',					1,			6,				1,		0),
		('End Game',					'0006', '0006_poster.jpg', 'EndGame_Trailer.mov',					'EndGame_Film.mov',						2,			7,				1,		0),
		('Flash Back',					'0007', '0007_poster.jpg', 'FlashBack_Trailer.mov',					'FlashBack_Film.mov',					3,			1,				1,		0),
		('Free Guy',					'0008', '0008_poster.jpg', 'FreeGuy_Trailer.mov',					'FreeGuy_Film.mov',						4,			2,				1,		0),
		('Fun House',					'0009', '0009_poster.jpg', 'FunHouse_Trailer.mov',					'FunHouse_Film.mov',					5,			3,				1,0),
		('Hero Mode',					'0010', '0010_poster.jpg', 'HeroMode_Trailer.mov',					'HeroMode_Film.mov',					1,			4,				1,0),
		('Hitman Wife BodyGuard',		'0011', '0011_poster.jpg', 'HitmanWifeBodyGuard_Trailer.mov',		'HitmanWifeBodyGuard_Film.mov',			2,			5,				1,0),
		('Old',							'0012', '0012_poster.jpg', 'Old_Trailer.mov',						'Old_Film.mov',							3,			6,				1,0),
		('Port Authority',				'0013', '0013_poster.jpg', 'PortAuthority_Trailer.mov',				'PortAuthority_Film.mov',				4,			7,				1,0),
		('Queen Bees',					'0014', '0014_poster.jpg', 'QueenBees_Trailer.mov',					'QueenBees_Film.mov',					5,			1,				1,0),
		('Space Jam a New Legacy',		'0015', '0015_poster.jpg', 'SpaceJamANewLegacy_Trailer.mov',		'SpaceJamANewLegacy_Film.mov',			2,			2,				1,0)

insert into CELEBRITIES
--			Name				DateOfBirth		Role
values	('Chris Evans',			'1981-06-13',	1),
		('Robert Downey Jr.',	'1965-04-04',	1),
		('Chris Hemsworth',		'1983-08-11',	1),
		('Anthony Russo',		'1970-03-03',	2),
		('Joe Russo',			'1971-07-18',	2),
		('Shawn Levy',			'1968-07-23',	2)

insert into CREDITS
--		Movie_ID	Celeb_ID	 Role
values	(1,				1,		'Actor'),
		(1,				2,		'Actor'),
		(1,				4,		'Director'),
		(2,				2,		'Actor'),
		(2,				3,		'Actor'),
		(2,				5,		'Director'),
		(3,				1,		'Actor'),
		(3,				3,		'Actor'),
		(3,				6,		'Director'),
		(4,				1,		'Actor'),
		(4,				2,		'Actor'),
		(4,				5,		'Director'),
		(5,				2,		'Actor'),
		(5,				3,		'Actor'),
		(5,				4,		'Director'),
		(6,				1,		'Actor'),
		(6,				2,		'Actor'),
		(6,				3,		'Actor'),
		(6,				4,		'Director'),
		(6,				5,		'Director'),
		(7,				2,		'Actor'),
		(7,				6,		'Director'),
		(7,				4,		'Director'),
		(7,				5,		'Director'),
		(8,				2,		'Actor'),
		(8,				6,		'Director'),
		(9,				1,		'Actor'),
		(9,				4,		'Director'),
		(10,			3,		'Actor'),
		(10,			4,		'Director'),
		(11,			3,		'Actor'),
		(11,			6,		'Director'),
		(12,			5,		'Director'),
		(12,			1,		'Actor'),
		(13,			6,		'Director'),
		(13,			1,		'Actor'),
		(14,			4,		'Director'),
		(14,			5,		'Director'),
		(14,			6,		'Director'),
		(14,			2,		'Actor'),
		(15,			4,		'Director'),
		(15,			5,		'Director'),
		(15,			6,		'Director'),
		(15,			1,		'Actor'),
		(15,			2,		'Actor'),
		(15,			3,		'Actor')
