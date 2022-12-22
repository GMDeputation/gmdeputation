
CREATE TABLE Global.Tbl_Country_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	Code  AS ('C'+format(CodeVal,'000')),
	CodeVal int NOT NULL,
	Name nvarchar(100) NOT NULL,
	FrenchName nvarchar(100) NULL,
	Alpha2Code nvarchar(10) NULL,
	Alpha3Code nvarchar(10) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);


INSERT INTO Global.Tbl_Country_NTA (IdOld,CodeVal,Name,FrenchName,Alpha2Code,Alpha3Code,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT Id,CodeVal,Name,FrenchName,Alpha2Code,Alpha3Code,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_Country;