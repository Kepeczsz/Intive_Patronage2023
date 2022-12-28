CREATE DATABASE Intive_Patronage;
USE Intive_Patronage;

CREATE TABLE Book(
  Id int IDENTITY(1,1) PRIMARY KEY,
  Title NVARCHAR(100) Not Null,
  Description NVARCHAR(MAX) Not Null,
  Rating decimal Not null,
  ISBN NVARCHAR(13) Not Null,
  PublicationDate Datetime2 Not Null
);

CREATE TABLE Author(
Id int IDENTITY(1,1) PRIMARY KEY,
FirstName NVARCHAR(50) Not Null,
LastName NVARCHAR(50) Not Null,
BirthDate Datetime2 Not Null,
Gender bit Not Null
);

CREATE TABLE BookAuthor (
    AuthorId int FOREIGN KEY REFERENCES Author(Id),
    BookId int FOREIGN KEY REFERENCES Book(Id)
);
  