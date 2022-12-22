CREATE TABLE Auth.Tbl_User_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	FirstName nvarchar(50) NULL,
	MiddleName nvarchar(50) NULL,
	LastName nvarchar(50) NULL,
	Phone nvarchar(50) NULL,
	UserName nvarchar(50) NOT NULL,
	Email nvarchar(75) NOT NULL,
	Gender nvarchar(20) NOT NULL,
	Address nvarchar(200) NULL,
	Zipcode nvarchar(50) NOT NULL,
	Lat nvarchar(50) NULL,
	Long nvarchar(50) NULL,
	SectionId int NULL,
	SectionIdOld uniqueidentifier NULL,
	DistrictId int NULL,
	DistrictIdOld uniqueidentifier NULL,
	StateId int NULL,
	StateIdOld uniqueidentifier NULL,
	CountryId int NULL,
	CountryIdOld uniqueidentifier NULL,
	RoleId int NOT NULL,
	RoleIdOld uniqueidentifier NOT NULL,
	IsEmailVerify bit NOT NULL,
	VerifiedOn datetime NULL,
	Otp int NULL,
	ImageFile nvarchar(100) NULL,
	ImageSequence nvarchar(50) NULL,
	IsSuperAdmin bit NOT NULL,
	IsActive bit NOT NULL,
	City nvarchar(50) NULL,
	WorkPhoneNo nvarchar(50) NULL,
	TelePhoneNo nvarchar(50) NULL,
	Status nvarchar(100) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
 );
 
 
 
 INSERT INTO Auth.Tbl_User_NTA (IdOld,FirstName,MiddleName,LastName,Phone,UserName,Email,Gender,Address,Zipcode,Lat,Long,SectionId,SectionIdOld,DistrictId,DistrictIdOld,StateId,StateIdOld,CountryId,CountryIdOld,RoleId,RoleIdOld,IsEmailVerify,VerifiedOn,Otp,ImageFile,ImageSequence,IsSuperAdmin,IsActive,City,WorkPhoneNo,TelePhoneNo,Status,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,FirstName,MiddleName,LastName,Phone,UserName,Email,Gender,Address,Pincode,Lat,Long,1,SectionId,1,DistrictId,1,StateId,1,CountryId,1,RoleId,IsEmailVerify,VerifiedOn,Otp,ImageFile,ImageSequence,IsSuperAdmin,IsActive,City,WorkPhoneNo,TelePhoneNo,Status,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Auth.Tbl_User;


 updaTE x  
SET x.SectionID = y.id
from Auth.Tbl_User_NTA x
join  Global.Tbl_Section_NTA y on x.Sectionidold = y.idold

  updaTE x  
SET x.SectionID = NULL
from Auth.Tbl_User_NTA x
where SectionIDOld is NULL

 updaTE x  
SET x.DistrictId = y.id
from Auth.Tbl_User_NTA x
join  Global.Tbl_District_NTA y on x.districtidold = y.idold

  updaTE x  
SET x.DistrictId = NULL
from Auth.Tbl_User_NTA x
where districtidold is NULL


 updaTE x  
SET x.stateid = y.id
from Auth.Tbl_User_NTA x
join  Global.Tbl_state_NTA y on x.stateidold = y.idold


  updaTE x  
SET x.stateid = NULL
from Auth.Tbl_User_NTA x
where stateidold is NULL

 updaTE x  
SET x.countryid = y.id
from Auth.Tbl_User_NTA x
join  Global.Tbl_country_NTA y on x.countryidold = y.idold


  updaTE x  
SET x.countryid = NULL
from Auth.Tbl_User_NTA x
where countryidold is NULL

 updaTE x  
SET x.roleid = y.id
from Auth.Tbl_User_NTA x
join  Auth.Tbl_Role_NTA y on x.roleidold = y.idold


    ALTER TABLE Auth.Tbl_User_NTA
   ADD CONSTRAINT FK_Tbl_User_NTA_AttributeId_Tbl_Section_NTA_ID FOREIGN KEY (SectionId)
      REFERENCES Global.Tbl_Section_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

    ALTER TABLE Auth.Tbl_User_NTA
   ADD CONSTRAINT FK_Tbl_User_NTA_DistrictId_Tbl_Distrcit_NTA_ID FOREIGN KEY (DistrictId)
      REFERENCES Global.Tbl_District_NTA (ID)
      ON DELETE No ACtion
      ON UPDATE No Action
;


    ALTER TABLE Auth.Tbl_User_NTA
   ADD CONSTRAINT FK_Tbl_User_NTA_StateId_Tbl_State_NTA_ID FOREIGN KEY (StateId)
      REFERENCES Global.Tbl_State_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;


    ALTER TABLE Auth.Tbl_User_NTA
   ADD CONSTRAINT FK_Tbl_User_NTA_CountryId_Tbl_Country_NTA_ID FOREIGN KEY (CountryId)
      REFERENCES Global.Tbl_Country_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;


    ALTER TABLE Auth.Tbl_User_NTA
   ADD CONSTRAINT FK_Tbl_User_NTA_RoleId_Tbl_Role_NTA_ID FOREIGN KEY (RoleId)
      REFERENCES Auth.Tbl_Role_NTA (ID)
	  ON DELETE CASCADE
      ON UPDATE CASCADE
;


