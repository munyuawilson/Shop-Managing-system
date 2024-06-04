-- Create the database if it does not already exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Shopping System')
BEGIN
    CREATE DATABASE [Shopping System];
END
GO

-- Use the newly created database
USE [Shopping System];
GO

-- Create the User_Info table
CREATE TABLE User_Info (
  UserID int NOT NULL PRIMARY KEY,
  Picture nvarchar(max) NULL,
  Name nvarchar(100) NOT NULL,
  Email nvarchar(100) NOT NULL,
  Address nvarchar(255) NULL,
  PasswordHash nvarchar(100) NOT NULL,
  Gender bit NOT NULL,
  User_Type bit NOT NULL,
  Pocket_Money decimal(18, 2) NOT NULL
);
CREATE TABLE Category (
  ID int PRIMARY KEY NOT NULL,
  Name nvarchar(50) NOT NULL
);
-- Create the Product table
CREATE TABLE Product (
  ID int PRIMARY KEY NOT NULL,
  Name nvarchar(100) NOT NULL,
  Category_ID int NULL,
  FOREIGN KEY (ID) REFERENCES Category(ID),
  Publisher_ID int NULL,
  Picture nvarchar(255) NULL,
  Minimum_Amount int NULL,
  Date_Of_Expiring datetime NULL,
  Date_Of_Production datetime NULL,
  Description nvarchar(max) NULL,
  Price money NULL,
  Status nvarchar(50) NULL,
  BuyerID int NULL,
  PurchaseDate datetime NULL,
  PaymentWay nvarchar(50) NULL
);




-- Create the Buying_Product table
CREATE TABLE Buying_Product (
  UserID int NOT NULL,
  Product_ID int NOT NULL,
  FOREIGN KEY (UserID) REFERENCES User_Info(UserID),
  FOREIGN KEY (Product_ID) REFERENCES Product(ID)
);

