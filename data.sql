create database QuanLyTrangPhim
go
use QuanLyTrangPhim
go

create table Users
( IDUser int IDENTITY(1,1) primary key not null,
  Email nvarchar(50) unique not null,
  Name nvarchar(50) not null,
  Password nvarchar(20),
  Role int not null,
  PurchaseInfo nvarchar(50) not null,
 );

create table Categories
( IDCategory int IDENTITY(1,1) primary key not null,
  CategoryName nvarchar(50) unique not null
);

create table Movies
( 
  IDMovie int IDENTITY(1,1) primary key not null,
  MovieName nvarchar(50) not null,
  MoviePath nvarchar(50) not null,
  PosterPath IMAGE not null,
  DesccriptionMovie nvarchar(200) not null,
  IDCategory int not null,
  constraint fk_Category_Movies
  foreign key (IDCategory)
  references Categories (IDCategory),
);

create table Profiles
( IDProfile int IDENTITY(1,1) primary key not null,
  ProfileName nvarchar(50) not null,
  IDUser int not null,
  constraint fk_User_Profile
  foreign key (IDUser)
  references Users (IDUser)
)

create table Favorites
( IDProfile int not null,
  IDMovie int not null,
  constraint fk_Movie_Fav
  foreign key (IDMovie)
  references Movies (IDMovie),
  constraint fk_Profile_Fav
  foreign key (IDProfile)
  references Profiles (IDProfile)
);

create table Playlists
( IDPlaylist int IDENTITY(1,1) primary key not null,
  PlaylistName nvarchar(50) not null,
  IDProfile int not null,
  constraint fk_Profile_Playlist
  foreign key (IDProfile)
  references Profiles (IDProfile)
);

create table PlaylistMovie
( IDPlaylist int not null,
  IDMovie int not null,
  constraint fk_Playlist_PM
  foreign key (IDPlaylist)
  references Playlists (IDPlaylist),
  constraint fk_Movie_PM
  foreign key (IDMovie)
  references Movies (IDMovie)
);

create table Purchases
( IDPurchase int IDENTITY(1,1) primary key not null,
  DatePurchase datetime not null,
  DateExpire datetime not null,
  Cost int not null,
  IDUser int not null,
  constraint fk_User_Purchase
  foreign key (IDUser)
  references Users (IDUser)
)

create table UserMovies (
	IDMovie int not null,
	IDUser int not null,

	constraint PK_UserMovies primary key(IDUser, IDMovie)
)

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Tình cảm')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Hành động')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Viễn tưởng')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Phiêu lưu')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Âm nhạc')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Hài hước')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Lịch sử')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Hoạt hình')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Kinh dị')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Thần thoại')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Hình sự')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           (N'Huyền bí')
GO

USE [QuanLyTrangPhim]
GO

INSERT INTO [dbo].[Users]
           ([Email]
           ,[Name]
           ,[Password]
           ,[Role]
           ,[PurchaseInfo])
     VALUES
           (N'admin@gmail.com', N'admin', N'admin123', 1, N'874395797349')
GO

