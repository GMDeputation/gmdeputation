
CREATE TABLE Global.Tbl_State_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	Code  AS ('S'+format(CodeVal,'000')),
	CodeVal int NOT NULL,
	Alias nvarchar(50) NULL,
	Name nvarchar(100) NOT NULL,
	CountryId int NOT NULL,
	CountryIdOld uniqueidentifier NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);

INSERT INTO Global.Tbl_State_NTA (IdOld,CodeVal,Alias,Name,CountryId,CountryIdOld,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT Id,CodeVal,Alias,Name,1,CountryId,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_State;

updaTE x  
SET x.countryid = y.id
from Global.Tbl_State_NTA x
join  Global.Tbl_Country_NTA y on x.countryidold = y.idold


ALTER TABLE Global.Tbl_State_NTA
   ADD CONSTRAINT FK_Tbl_State_NTA_CountryID_Tbl_Country_NTA_ID FOREIGN KEY (CountryID)
      REFERENCES Global.Tbl_Country_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;