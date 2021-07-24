IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CatalogDB')
BEGIN 
	CREATE DATABASE CatalogDB;
END
GO

Use CatalogDB;
GO

DROP TABLE IF EXISTS PRODUCT;

CREATE TABLE PRODUCT (
	Id INT PRIMARY KEY NOT NULL IDENTITY,
	Code VARCHAR(10) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	CONSTRAINT CodeMustBeUnique UNIQUE(Code),
	CONSTRAINT StartDateBeAlwaysBeforeEndDate CHECK (StartDate < EndDate)
)

GO