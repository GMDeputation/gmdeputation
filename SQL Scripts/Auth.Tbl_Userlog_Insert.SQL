CREATE TABLE Auth.Tbl_UserLog_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdOld uniqueidentifier NOT NULL,
	UserId int NOT NULL,
	UserIdOld uniqueidentifier NOT NULL,
	LoginTime datetime NOT NULL,
	LogoutTime datetime NULL,
)

INSERT INTO Auth.Tbl_UserLog_NTA (IdOld,UserId,UserIdOld,LoginTime,LogoutTime)
SELECT id,1,UserId,LoginTime,LogoutTime
FROM Auth.Tbl_UserLog;

     updaTE x  
SET x.userid = y.id
from Auth.Tbl_UserLog_NTA x
join  Auth.Tbl_User_NTA y on x.useridold = y.idold


    ALTER TABLE Auth.Tbl_UserLog_NTA
   ADD CONSTRAINT FK_Tbl_UserLog_NTA_UserId_Tbl_User_NTA_ID FOREIGN KEY (UserId)
      REFERENCES Auth.Tbl_User_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;
