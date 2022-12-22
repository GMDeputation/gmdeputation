
CREATE TABLE Global.Tbl_Section_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	Name nvarchar(100) NOT NULL,
	DistrictId int NOT NULL,
	DistrictIdOld uniqueidentifier NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);


INSERT INTO Global.Tbl_Section_NTA (IdOld,Name,DistrictId,DistrictIdOld,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT Id,Name,1,DistrictId,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_Section;

updaTE x  
SET x.districtid = y.id
from Global.Tbl_Section_NTA x
join  Global.Tbl_District_NTA y on x.districtidold = y.idold

ALTER TABLE Global.Tbl_Section_NTA
   ADD CONSTRAINT FK_Tbl_Section_NTA_DistrictID_Tbl_District_NTA_ID FOREIGN KEY (DistrictID)
      REFERENCES Global.Tbl_District_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;