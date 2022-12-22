

CREATE TABLE Global.Tbl_UserChurch_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	UserId int NOT NULL,
	ChurchId int NOT NULL,
	UserIdOld uniqueidentifier NOT NULL,
	ChurchIdOld uniqueidentifier NOT NULL,
	RelationType nvarchar(50) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Global.Tbl_UserChurch_NTA (UserId,ChurchId,UserIdOld,ChurchIdOld,RelationType,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT 1,1,UserId,ChurchId,RelationType,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_UserChurch;