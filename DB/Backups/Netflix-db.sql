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
	ID char(4),
	NAME nvarchar(50),

	constraint PK_ACCTYPE primary key (ID)
)

create table CATEGORY
(
	ID char(4),
	NAME nvarchar(50),

	constraint PK_CATEGORY primary key (ID)
)

create table CELEBRITIES
(
	ID char(4),
	NAME nvarchar(50),
	DATEOFBIRTH date,

	constraint PK_CELEBRITIES primary key (ID)
)

create table USER_INFORMATION
(
	ID char(4),
	FIRST_NAME nvarchar(50),
	LAST_NAME nvarchar(50),
	EMAIL nchar(50),
	PHONE nchar(20),
	ADDRESS nvarchar(50),
	DATEOFBIRTH date,

	constraint PK_USRINFOR primary key (ID)
)

create table MOVIE
(
	ID char(4),
	NAME nvarchar(100),
	INFORMATION char(4),
	POSTER nchar(50),

	constraint PK_MOVIE primary key (ID)
)

create table MOVIE_INFORMATION
(
	ID char(4),
	DESCRIPTION nvarchar(200),
	LENGTH int,
	RATE float,
	DISTRIBUTE_YEAR int,
	COUNTRY nchar(50),

	constraint PK_MOVIEINFOR primary key (ID)
)

create table ACCOUNT
(
	USERNAME nchar(50),
	PASSWORD nchar(50),
	BALANCE float,
	TYPE char(4),
	INFORMATION char(4)

	constraint PK_ACCOUNT primary key (USERNAME)
)

create table CLASSIFY
(
	CATEGORY_ID char(4),
	MOVIE_ID char(4),

	constraint PK_CLASSIFY primary key (CATEGORY_ID, MOVIE_ID)
)

create table CREDITS
(
	MOVIE_ID char(4),
	PERSON_ID char(4),
	ROLE nvarchar(50),

	constraint PK_CREDITS primary key (MOVIE_ID, PERSON_ID)
)

create table FAVOURITE_MOVIES
(
	USERNAME nchar(50),
	MOVIE_ID char(4),

	constraint PK_FAVMVS primary key (USERNAME, MOVIE_ID)
)

create table PURCHASE_INFORMATION
(
	USERNAME nchar(50),
	MOVIE_ID char(4),
	PURCHASED_DATE date,

	constraint PK_PURINF primary key (USERNAME, MOVIE_ID)
)

alter table PURCHASE_INFORMATION
add constraint FK_PUR_ACCOUNT foreign key (USERNAME) references ACCOUNT (USERNAME)

alter table PURCHASE_INFORMATION
add constraint FK_PUR_MOVIE foreign key (MOVIE_ID) references MOVIE (ID)

alter table FAVOURITE_MOVIES
add constraint FK_FM_ACCOUNT foreign key (USERNAME) references ACCOUNT (USERNAME)

alter table FAVOURITE_MOVIES
add constraint FK_FM_MOVIE foreign key (MOVIE_ID) references MOVIE (ID)

alter table ACCOUNT
add constraint FK_ACCOUNT_ACCTYPE foreign key (TYPE) references ACCOUNT_TYPE (ID)

alter table ACCOUNT
add constraint FK_ACCOUNT_INFO foreign key (INFORMATION) references USER_INFORMATION (ID)

alter table CLASSIFY
add constraint FK_CATEGORY foreign key (CATEGORY_ID) references CATEGORY (ID)

alter table CLASSIFY
add constraint FK_MOVIE foreign key (MOVIE_ID) references MOVIE (ID)

alter table MOVIE
add constraint FK_MOVIE_INFO foreign key (INFORMATION) references MOVIE_INFORMATION (ID)

insert into ACCOUNT_TYPE 
values	('0001', 'User'),
		('0002', 'Admin')

insert into CATEGORY
values	('0001', 'Hành động'),
		('0002', 'Hài'),
		('0003', 'Kinh dị'),
		('0004', 'Khoa học viễn tưởng'),
		('0005', 'Sử thi'),
		('0006', 'Tình cảm'),
		('0007', 'Cartoon'),
		('0008', 'Lịch sử'),
		('0009', 'Phiêu lưu'),
		('0010', '18+'),
		('0011', 'Chiến tranh'),
		('0012', 'Thể thao'),
		('0013', 'Tội phạm')

insert into MOVIE_INFORMATION
values	('0001', 'On the cusp of the year 2000, Colombian brothers Carly and Mateo prepare to move to the US for their last years of high school...', 145, 8.5, 2021, 'Germany'),
		('0002', 'Filmed over five year, Chasing Wonders is a herat-warming story of a young boy, who, encouraged by his grandfather to live a life of hope...', 129, 8, 2020, 'U.K'),
		('0003', 'Billy Walker is finally let out of prison after serving a seven-year sentence for manslaughter, only to find out that the only person...', 129, 7.5, 2019, 'Canada'),
		('0004', 'Hoping to reclaim his former glory, a fallen dominoes champion recruits his step-grandson to help him win an off-the-wall tournament', 149, 8, 2018, 'India'),
		('0005', 'The film tells the inspiring true story of Dream Alliance, an unlikely race horse bred by small town bartender, Jan Vokes.', 153, 8, 2017, 'Mexico'),
		('0006', 'Adrift in space with no food or water, Tony Stark sends a message to Pepper Potts as his oxygen supply starts to dwindle.', 301, 9.5, 2019, 'U.S.A'),
		('0007', 'Fredrick Fitzell is living his best live - until he starts having horrific visions of Cindy, a girl who vanished in high school', 138, 8, 2008, 'Dubai'),
		('0008', 'In Free Guy, a back teller who discovers he is actually a background player in an open-world video game, decides to become the hero...', 155, 8.5, 2006, 'U.S.A'),
		('0009', 'Down-and-out backup singer and celebrity ex-husband kasper is invited to complete in the FunHouse, an online Big Brother style reality show', 146, 7.5, 2001, 'China'),
		('0010', 'A teenage coding prodigy has 30 days to create the world greatest video game and save his family business.', 129, 8, 2000, 'Vietnam'),
		('0011', 'The world most lethal odd couple - bodyguard Michael Brycce and hitman Darius Kincaid -- are back on another life-threatening...', 139, 7, 2001, 'Korea'),
		('0012', 'Terror strikes when beachgoers suddenly realize they are aging rapidly', 235, 6.5, 1999, 'Canada'),
		('0013', 'After getting kicked out of his home in central Pennsylvania, Paul arrives to NYC dizzying central station with nowhere to go...', 141, 7, 2005, 'Australia'),
		('0014', 'While her house undergoes repairs, fiercely independent senior Helen temporarily moves into a nearby retirement community...', 140, 7, 2008, 'France'),
		('0015', 'When Lebron James and his young son Dom are trapped in a digital space by a rogue A.I., LeBron must get them home safe by leading Bugs, ...', 185, 8, 2007, 'Italy')

insert into MOVIE
values	('0001', 'Blast Beat', '0001', '0001_poster.png'),
		('0002', 'Chasing Wonders', '0002', '0002_poster.png'),
		('0003', 'Death in Texas', '0003', '0003_poster.png'),
		('0004', 'Domino Battle of the Bones', '0004', '0004_poster.png'),
		('0005', 'Dream Horse', '0005', '0005_poster.png'),
		('0006', 'End Game', '0006', '0006_poster.png'),
		('0007', 'Flash Back', '0007', '0007_poster.png'),
		('0008', 'Free Guy', '0008', '0008_poster.png'),
		('0009', 'Fun House', '0009', '0009_poster.png'),
		('0010', 'Hero Mode', '0010', '0010_poster.png'),
		('0011', 'Hitman Wife BodyGuard', '0011', '0011_poster.png'),
		('0012', 'Old', '0012', '0012_poster.png'),
		('0013', 'Port Authority', '0013', '0013_poster.png'),
		('0014', 'Queen Bees', '0014', '0014_poster.png'),
		('0015', 'Space Jam a New Legacy', '0015', '0015_poster.png')

insert into CLASSIFY
values	('0001', '0001'),
		('0002', '0001'),
		('0001', '0002'),
		('0003', '0002'),
		('0004', '0003'),
		('0005', '0003'),
		('0006', '0004'),
		('0007', '0004'),
		('0008', '0005'),
		('0011', '0005'),
		('0009', '0006'),
		('0005', '0006'),
		('0010', '0007'),
		('0013', '0007'),
		('0011', '0008'),
		('0007', '0008'),
		('0012', '0009'),
		('0009', '0009'),
		('0013', '0010'),
		('0011', '0010'),
		('0004', '0011'),
		('0002', '0011'),
		('0006', '0012'),
		('0012', '0012'),
		('0008', '0013'),
		('0002', '0013'),
		('0004', '0014'),
		('0001', '0015'),
		('0003', '0015')

insert into CELEBRITIES
values	('A001', 'Chris Evans', '1981-06-13'),
		('A002', 'Robert Downey Jr.', '1965-04-04'),
		('A003', 'Chris Hemsworth', '1983-08-11'),
		('D001', 'Anthony Russo', '1970-03-03'),
		('D002', 'Joe Russo', '1971-07-18'),
		('D003', 'Shawn Levy', '1968-07-23')

insert into CREDITS
values	('0001', 'A001', 'Actor'),
		('0001', 'A002', 'Actor'),
		('0001', 'D001', 'Director'),
		('0002', 'A002', 'Actor'),
		('0002', 'A003', 'Actor'),
		('0002', 'D002', 'Director'),
		('0003', 'A001', 'Actor'),
		('0003', 'A003', 'Actor'),
		('0003', 'D003', 'Director'),
		('0004', 'A001', 'Actor'),
		('0004', 'A002', 'Actor'),
		('0004', 'D002', 'Director'),
		('0005', 'A002', 'Actor'),
		('0005', 'A003', 'Actor'),
		('0005', 'D001', 'Director'),
		('0006', 'A001', 'Actor'),
		('0006', 'A002', 'Actor'),
		('0006', 'A003', 'Actor'),
		('0006', 'D001', 'Director'),
		('0006', 'D002', 'Director'),
		('0007', 'A002', 'Actor'),
		('0007', 'D003', 'Director'),
		('0007', 'D001', 'Director'),
		('0007', 'D002', 'Director'),
		('0008', 'A002', 'Actor'),
		('0008', 'D003', 'Director'),
		('0009', 'A001', 'Actor'),
		('0009', 'D001', 'Director'),
		('0010', 'A003', 'Actor'),
		('0010', 'D001', 'Director'),
		('0011', 'A003', 'Actor'),
		('0011', 'D003', 'Director'),
		('0012', 'D002', 'Director'),
		('0012', 'A001', 'Actor'),
		('0013', 'D003', 'Director'),
		('0013', 'A001', 'Actor'),
		('0014', 'D001', 'Director'),
		('0014', 'D002', 'Director'),
		('0014', 'D003', 'Director'),
		('0014', 'A002', 'Actor'),
		('0015', 'D001', 'Director'),
		('0015', 'D002', 'Director'),
		('0015', 'D003', 'Director'),
		('0015', 'A001', 'Actor'),
		('0015', 'A002', 'Actor'),
		('0015', 'A003', 'Actor')