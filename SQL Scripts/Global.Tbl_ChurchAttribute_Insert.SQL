
CREATE TABLE Global.Tbl_ChurchAttribute_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	ChurchId int NOT NULL,
	ChurchIdOld uniqueidentifier NOT NULL,
	AttributeId int NOT NULL,
	AttributeIdOld uniqueidentifier NOT NULL,
	AttributeValue numeric(18, 2) NULL,
	Notes nvarchar(150) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Global.Tbl_ChurchAttribute_NTA (IdOld,ChurchId,ChurchIdOld,AttributeId,AttributeIdOld,AttributeValue,Notes,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,1,ChurchId,1,AttributeId,AttributeValue,Notes,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_ChurchAttribute;

  updaTE x  
SET x.churchid = y.id
from Global.Tbl_ChurchAttribute_NTA x
join  Global.Tbl_church_NTA y on x.churchidold = y.idold

  updaTE x  
SET x.AttributeId = y.id
from Global.Tbl_ChurchAttribute_NTA x
join  Global.Tbl_attribute_NTA y on x.AttributeIdOld = y.idold

  ALTER TABLE Global.Tbl_ChurchAttribute_NTA
   ADD CONSTRAINT FK_Tbl_ChurchAttribute_NTA_ChurchId_Tbl_Church_NTA_ID FOREIGN KEY (ChurchId)
      REFERENCES Global.Tbl_Church_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

  ALTER TABLE Global.Tbl_ChurchAttribute_NTA
   ADD CONSTRAINT FK_Tbl_ChurchAttribute_NTA_AttributeId_Tbl_Attribute_NTA_ID FOREIGN KEY (AttributeId)
      REFERENCES Global.Tbl_Attribute_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;