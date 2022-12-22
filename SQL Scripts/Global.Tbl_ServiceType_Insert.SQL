

CREATE TABLE Global.Tbl_ServiceType_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	Name nvarchar(100) NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

TODO: Old table had fk on insert usr and updat user. Do we want to do that? I say no. 

INSERT INTO Global.Tbl_ServiceType_NTA (IdOld,Name,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,Name,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_ServiceType;
