
CREATE TABLE Global.Tbl_Attribute_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	AttributeName nvarchar(100) NOT NULL,
	AttributeTypeId int NOT NULL,
	AttributeTypeIdOld uniqueidentifier NOT NULL,
	AttributeNotes nvarchar(150) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Global.Tbl_Attribute_NTA (IdOld,AttributeName,AttributeTypeId,AttributeTypeIdOld,AttributeNotes,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,AttributeName,1,AttributeTypeId,AttributeNotes,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_Attribute;

updaTE x  
SET x.attributetypeid = y.id
from Global.Tbl_Attribute_NTA x
join  Global.Tbl_AttributeType_NTA y on x.AttributeTypeIdOld = y.idold

ALTER TABLE Global.Tbl_Attribute_NTA
   ADD CONSTRAINT FK_Tbl_Attribute_NTA_AttributeTypeId_Tbl_AttributeType_NTA_ID FOREIGN KEY (AttributeTypeId)
      REFERENCES Global.Tbl_AttributeType_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;