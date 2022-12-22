
CREATE TABLE Global.Tbl_District_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	Code  AS ('D'+format(CodeVal,'000')),
	CodeVal int NOT NULL,
	Name nvarchar(100) NOT NULL,
	Alias nvarchar(50) NULL,
	Website nvarchar(50) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);


INSERT INTO Global.Tbl_District_NTA (IdOld,CodeVal,Name,Alias,Website,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT Id,CodeVal,Name,Alias,Website,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_District;