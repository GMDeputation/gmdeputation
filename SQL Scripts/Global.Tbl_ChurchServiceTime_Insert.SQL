
CREATE TABLE Global.Tbl_ChurchServiceTime_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	ChurchId int NOT NULL,
	ChurchIdOld uniqueidentifier NOT NULL,
	WeekDay nvarchar(30) NOT NULL,
	ServiceTime time(7) NOT NULL,
	ServiceTypeId int NOT NULL,
	ServiceTypeIdOld uniqueidentifier NOT NULL,
	Preferencelevel int NOT NULL,
	Notes nvarchar(100) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Global.Tbl_ChurchServiceTime_NTA (IdOld,ChurchId,ChurchIdOld,WeekDay,ServiceTime,ServiceTypeId,ServiceTypeIdOld,Preferencelevel,Notes,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,1,ChurchId,WeekDay,ServiceTime,1,ServiceTypeId,Preferencelevel,Notes,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_ChurchServiceTime;

updaTE x  
SET x.servicetypeid = y.id
from Global.Tbl_ChurchServiceTime_NTA x
join  Global.Tbl_ServiceType_NTA y on x.servicetypeidold = y.idold


updaTE x  
SET x.churchID = y.id
from Global.Tbl_ChurchServiceTime_NTA x
join  Global.Tbl_Church_NTA y on x.churchidold = y.idold

  ALTER TABLE Global.Tbl_ChurchServiceTime_NTA
   ADD CONSTRAINT FK_Tbl_ChurchServiceTime_NTA_ChurchId_Tbl_Church_NTA_ID FOREIGN KEY (ChurchId)
      REFERENCES Global.Tbl_Church_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;


  ALTER TABLE Global.Tbl_ChurchServiceTime_NTA
   ADD CONSTRAINT FK_Tbl_ChurchServiceTime_NTA_ServiceTypeId_Tbl_ServiceTypeId_NTA_ID FOREIGN KEY (ServiceTypeId)
      REFERENCES Global.Tbl_ServiceType_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;