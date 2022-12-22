
CREATE TABLE Global.Tbl_StateDistrict_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	DistrictId int NOT NULL,
	DistrictIdOld uniqueidentifier NOT NULL,
	StateId int NOT NULL,
	StateIdOld uniqueidentifier NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Global.Tbl_StateDistrict_NTA (DistrictId,DistrictIdOld,StateId,StateIdOld,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT 1,DistrictId,1,StateId,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_StateDistrict;


updaTE x  
SET x.DistrictId = y.id
from Global.Tbl_StateDistrict_NTA x
join  Global.Tbl_District_NTA y on x.DistrictIdOld = y.idold

updaTE x  
SET x.StateId = y.id
from Global.Tbl_StateDistrict_NTA x
join  Global.Tbl_State_NTA y on x.StateIdOld = y.idold


ALTER TABLE Global.Tbl_StateDistrict_NTA
   ADD CONSTRAINT FK_Tbl_StateDistrict_NTA_DistrictId_Tbl_District_NTA_ID FOREIGN KEY (DistrictId)
      REFERENCES Global.Tbl_District_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

ALTER TABLE Global.Tbl_StateDistrict_NTA
   ADD CONSTRAINT FK_Tbl_StateDistrict_NTA_StateId_Tbl_State_NTA_ID FOREIGN KEY (StateId)
      REFERENCES Global.Tbl_State_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;