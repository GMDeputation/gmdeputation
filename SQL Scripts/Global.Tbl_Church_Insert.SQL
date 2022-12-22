
CREATE TABLE Global.Tbl_Church_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	ChurchIdNo nvarchar(50) NULL,
	ChurchName nvarchar(150) NOT NULL,
	Address nvarchar(200) NULL,
	Directory nvarchar(300) NULL,
	MailAddress nvarchar(200) NULL,
	ChurchType nvarchar(50) NULL,
	AccountNo nvarchar(50) NULL,
	lat nvarchar(50) NULL,
	lon nvarchar(50) NULL,
	DistrictId int NOT NULL,
	SectionId int NOT NULL,
	DistrictIdOld uniqueidentifier NOT NULL,
	SectionIdOld uniqueidentifier NOT NULL,
	Phone nvarchar(50) NULL,
	Phone2 nvarchar(50) NULL,
	Email nvarchar(50) NULL,
	WebSite nvarchar(50) NULL,
	Status nvarchar(50) NULL,
	IsDelete bit NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);


INSERT INTO Global.Tbl_Church_NTA (IdOld,ChurchIdNo,ChurchName,Address,Directory,MailAddress,ChurchType,AccountNo,lat,lon,DistrictId,SectionId,DistrictIdOld,SectionIdOld,Phone,Phone2,Email,WebSite,Status,IsDelete,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT Id,ChurchIdNo,ChurchName,Address,Directory,MailAddress,ChurchType,AccountNo,lat,lon,1,1,DistrictId,SectionId,Phone,Phone2,Email,WebSite,Status,IsDelete,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Global.Tbl_Church;

updaTE x  
SET x.districtid = y.id
from Global.Tbl_Church_NTA x
join  Global.Tbl_District_NTA y on x.districtidold = y.idold

updaTE x  
SET x.sectionid = y.id
from Global.Tbl_Church_NTA x
join  Global.Tbl_Section_NTA y on x.SectionIdOld = y.idold

ALTER TABLE Global.Tbl_Church_NTA
   ADD CONSTRAINT FK_Tbl_Church_NTA_DistrictID_Tbl_District_NTA_ID FOREIGN KEY (DistrictID)
      REFERENCES Global.Tbl_District_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

ALTER TABLE Global.Tbl_Church_NTA
   ADD CONSTRAINT FK_Tbl_Church_NTA_SectionID_Tbl_Section_NTA_ID FOREIGN KEY (SectionID)
      REFERENCES Global.Tbl_Section_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;