CREATE DATABASE MediPulseDB;
USE MediPulseDB;

CREATE TABLE Users (
    ID INT IDENTITY (1,1) PRIMARY KEY, 
    FirstName Varchar(100), 
    LastName Varchar(100), 
    Password varchar(100),
    Email varchar(100), 
    Fund Decimal(18,2), 
    Type varchar(100), 
    Status INT, 
    CreatedOn Datetime
);

CREATE TABLE Medicines(
    ID INT IDENTITY (1,1) PRIMARY KEY, 
    Name VARCHAR(100), 
    Manufacturer VARCHAR(100), 
    UnitPrice DECIMAL(18,2),
    Discount DECIMAL(18,2), 
    Quantity INT, 
    ExpDate Datetime, 
    ImageUrl VARCHAR(100), 
    Status INT
);

CREATE TABLE Cart(
    ID INT IDENTITY (1,1) PRIMARY KEY, 
    UserId INT, 
    MedicineId INT, 
    UnitPrice DECIMAL(18,2), 
    Discount DECIMAL(18,2), 
    Quantity INT, 
    TotalPrice DECIMAL(18,2)
);

CREATE TABLE Orders(
    ID INT IDENTITY (1,1) PRIMARY KEY, 
    UserId INT, 
    OrderNo VARCHAR(100), 
    OrderTotal DECIMAL(18,2), 
    OrderStatus VARCHAR(100)
);

CREATE TABLE OrderItems(
    ID INT IDENTITY (1,1) PRIMARY KEY, 
    UserId INT, 
    OrderId INT, 
    MedicineId INT, 
    UnitPrice Decimal(18,2), 
    Discount Decimal(18,2),
    Quantity INT, 
    TotalPrice Decimal(18,2)
);
