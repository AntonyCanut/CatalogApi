USE CatalogDB;
GO

CREATE OR ALTER PROCEDURE InsertProduct
@Code VARCHAR(10),
@Name VARCHAR(50),
@StartDate DATE,
@EndDate DATE
AS
	INSERT INTO PRODUCT(Code, Name, StartDate, EndDate)
	VALUES (@Code, @Name, @StartDate, @EndDate);
GO