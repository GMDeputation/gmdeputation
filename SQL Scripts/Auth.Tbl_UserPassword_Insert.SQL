CREATE TABLE Auth.Tbl_UserPassword_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	UserId int NOT NULL,
	UserIdOld uniqueidentifier NOT NULL,
	Password nvarchar(50) NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);


INSERT INTO Auth.Tbl_UserPassword_NTA (IdOld,UserId,UserIdOld,Password,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,1,UserId,Password,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Auth.Tbl_UserPassword;


   updaTE x  
SET x.userid = y.id
from Auth.Tbl_UserPassword_NTA x
join  Auth.Tbl_User_NTA y on x.useridold = y.idold

    ALTER TABLE Auth.Tbl_UserPassword_NTA
   ADD CONSTRAINT FK_Tbl_UserPassword_NTA_UserId_Tbl_User_NTA_ID FOREIGN KEY (UserId)
      REFERENCES Auth.Tbl_User_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;