
CREATE TABLE Global.Tbl_MacroScheduleDetails_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	MacroScheduleId int NOT NULL,
	MacroScheduleIdOld uniqueidentifier NOT NULL,
	DistrictId int NOT NULL,
	DistrictIdOld uniqueidentifier NOT NULL,
	UserId int NULL,
	UserIdOld uniqueidentifier NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	IsApproved bit NOT NULL,
	IsRejected bit NOT NULL,
	Notes nvarchar(100) NULL,
	ApprovedRejectRemarks nvarchar(100) NULL,
	ApprovedRejectBy int NULL,
	ApprovedRejectByOld uniqueidentifier NULL,
	ApprovedRejectOn datetime NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL,
);

INSERT INTO Global.Tbl_MacroScheduleDetails_NTA (IdOld,MacroScheduleId,MacroScheduleIdOld,DistrictId,DistrictIdOld,UserId,UserIdOld,StartDate,EndDate,IsApproved,IsRejected,Notes,ApprovedRejectRemarks,ApprovedRejectBy,ApprovedRejectByOld,ApprovedRejectOn,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,1,MacroScheduleId,1,DistrictId,1,UserId,StartDate,EndDate,IsApproved,IsRejected,Notes,ApprovedRejectRemarks,1,ApprovedRejectBy,ApprovedRejectOn,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_MacroScheduleDetails;


   updaTE x  
SET x.MacroScheduleId = y.id
from Global.Tbl_MacroScheduleDetails_NTA x
join  Global.Tbl_MacroSchedule_NTA y on x.MacroScheduleIdOld = y.idold

   updaTE x  
SET x.DistrictId = y.id
from Global.Tbl_MacroScheduleDetails_NTA x
join  Global.Tbl_District_NTA y on x.DistrictIdOld = y.idold

   updaTE x  
SET x.UserId = y.id
from Global.Tbl_MacroScheduleDetails_NTA x
join  Auth.Tbl_User_NTA y on x.UserIdOld = y.idold

   updaTE x  
SET x.ApprovedRejectBy = y.id
from Global.Tbl_MacroScheduleDetails_NTA x
join  Auth.Tbl_User_NTA y on x.ApprovedRejectByOld = y.idold

  update Global.Tbl_MacroScheduleDetails_NTA
  set ApprovedRejectBy = null where ApprovedRejectByOld is null
  
  
         ALTER TABLE  Global.Tbl_MacroScheduleDetails_NTA
   ADD CONSTRAINT FK_Tbl_MacroScheduleDetails_NTA_UserId_Tbl_User_NTA_ID FOREIGN KEY (UserId)
      REFERENCES Auth.Tbl_User_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;


       ALTER TABLE  Global.Tbl_MacroScheduleDetails_NTA
   ADD CONSTRAINT FK_Tbl_MacroScheduleDetails_NTA_MacroScheduleId_Tbl_MacroSchedule_NTA_ID FOREIGN KEY (MacroScheduleId)
      REFERENCES Global.Tbl_MacroSchedule_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;


       ALTER TABLE  Global.Tbl_MacroScheduleDetails_NTA
   ADD CONSTRAINT FK_Tbl_MacroScheduleDetails_NTA_DistrictId_Tbl_District_NTA_ID FOREIGN KEY (DistrictId)
      REFERENCES Global.Tbl_District_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;


         ALTER TABLE  Global.Tbl_MacroScheduleDetails_NTA
   ADD CONSTRAINT FK_Tbl_MacroScheduleDetails_NTA_ApprovedRejectBy_Tbl_User_NTA_ID FOREIGN KEY (ApprovedRejectBy)
      REFERENCES Auth.Tbl_User_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
  updaTE x  
SET x.EventUser = y.id
from Global.Tbl_Notification_NTA x
join  Auth.Tbl_User_NTA y on x.EventUserOld = y.idold