

CREATE TABLE Auth.Tbl_UserAttribute_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	UserId int NOT NULL,
	UserIdOld uniqueidentifier NOT NULL,
	AttributeId int NOT NULL,
	AttributeIdOld uniqueidentifier NOT NULL,
	AttributeValue numeric(18, 2) NULL,
	Notes nvarchar(150) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Auth.Tbl_UserAttribute_NTA (IdOld,UserId,UserIdOld,AttributeId,AttributeIdOld,AttributeValue,Notes,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,1,UserId,1,AttributeId,AttributeValue,Notes,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Auth.Tbl_UserAttribute;


   updaTE x  
SET x.userid = y.id
from Auth.Tbl_UserAttribute_NTA x
join  Auth.Tbl_User_NTA y on x.useridold = y.idold

   updaTE x  
SET x.attributeid = y.id
from Auth.Tbl_UserAttribute_NTA x
join  Global.Tbl_Attribute_NTA y on x.attributeidold = y.idold

  ALTER TABLE Auth.Tbl_UserAttribute_NTA
   ADD CONSTRAINT FK_Tbl_UserAttribute_NTA_UserId_Tbl_User_NTA_ID FOREIGN KEY (UserID)
      REFERENCES Auth.Tbl_User_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

  ALTER TABLE Auth.Tbl_UserAttribute_NTA
   ADD CONSTRAINT FK_Tbl_UserAttribute_NTA_AttributeId_Tbl_Attribute_NTA_ID FOREIGN KEY (AttributeId)
      REFERENCES Global.Tbl_Attribute_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;


