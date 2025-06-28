Create Database MusicianDB
Use MusicianDB

-- Musician DB

Create Table Musician
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
Ph_Number Varchar(20),
City Varchar(20),
Street Varchar(20)
)

Create Table Instrument
(
[Name] Varchar(20) Primary Key,
[Key] Int Not Null
)

Create Table Album
(
Id Int Primary Key Identity(1,1),
Title Varchar(20) Not Null,
[Date] Date,
Mus_Id Int References Musician(Id) 
)

Create Table Song
(
Title Varchar(20) Primary Key,
Author Varchar(20) Not Null
)

Create Table Album_Song
(
Album_Id Int References Album(Id),
Song_Title Varchar(20) Primary Key References Song(Title)
)

Create Table Mus_Song
(
Mus_Id Int References Musician(Id),
Song_Title Varchar(20) References Song(Title)
Primary Key(Mus_Id, Song_Title)
)

Create Table Mus_Instrument
(
Mus_Id Int References Musician(Id),
Inst_Name Varchar(20) References Instrument([Name])
Primary Key(Mus_Id, Inst_Name)
)