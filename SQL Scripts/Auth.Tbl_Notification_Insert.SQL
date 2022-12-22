
CREATE TABLE Global.Tbl_Notification_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	Description nvarchar(300) NOT NULL,
	EventUrl nvarchar(100) NULL,
	EventUser int NULL,
	EventUserOld uniqueidentifier NULL,
	IsOpened bit NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL,
);

INSERT INTO Global.Tbl_Notification_NTA (IdOld,Description,EventUrl,EventUser,EventUserOld,IsOpened,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,Description,EventUrl,NULL,EventUser,IsOpened,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_Notification;

  updaTE x  
SET x.EventUser = y.id
from Global.Tbl_Notification_NTA x
join  Auth.Tbl_User_NTA y on x.EventUserOld = y.idold