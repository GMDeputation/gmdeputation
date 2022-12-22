CREATE TABLE Global.Tbl_ChurchAccommodation_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	ChurchId int NOT NULL,
	ChurchIdOld uniqueidentifier NOT NULL,
	AccomType nvarchar(50) NULL,
	AccomNotes nvarchar(250) NULL,
	EQAccomAddress nvarchar(250) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Global.Tbl_ChurchAccommodation_NTA (IdOld,ChurchId,ChurchIdOld,AccomType,AccomNotes,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,1,ChurchId,AccomType,AccomNotes,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_ChurchAccommodation;

updaTE x  
SET x.churchID = y.id
from Global.Tbl_ChurchAccommodation_NTA x
join  Global.Tbl_Church_NTA y on x.churchidold = y.idold

ALTER TABLE Global.Tbl_ChurchAccommodation_NTA
   ADD CONSTRAINT FK_Tbl_ChurchAccommodation_NTA_ChurchID_Tbl_Church_NTA_ID FOREIGN KEY (ChurchID)
      REFERENCES Global.Tbl_Church_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;