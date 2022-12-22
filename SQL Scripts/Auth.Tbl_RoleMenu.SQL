drop table Auth.Tbl_RoleMenu_NTA

CREATE TABLE Auth.Tbl_RoleMenu_NTA(
	ID int IDENTITY(1,1) PRIMARY KEY,
	RoleId int not null,
	RoleIdOld uniqueidentifier NOT NULL,
	MenuID int NOT NULL,
	MenuIdOld uniqueidentifier NOT NULL,
	HasReadAccess bit NOT NULL,
	HasWriteAccess bit NOT NULL,
	HasFullAccess bit NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
);


INSERT INTO Auth.Tbl_RoleMenu_NTA (RoleId,RoleIdOld,MenuID,MenuIdOld,HasReadAccess,HasWriteAccess,HasFullAccess,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT 1,RoleId,1,MenuID,HasReadAccess,HasWriteAccess,HasFullAccess,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Auth.Tbl_RoleMenu;


updaTE x  
SET x.menuID = y.id
from auth.Tbl_RoleMenu_NTA x
join  auth.Tbl_Menu_NTA y on x.menuidold = y.MenuOldID

updaTE x  
SET x.roleid = y.id
from auth.Tbl_RoleMenu_NTA x
join  auth.Tbl_Role_NTA y on x.RoleIdOld = y.IdOld

ALTER TABLE Auth.Tbl_RoleMenu_NTA
   ADD CONSTRAINT FK_Tbl_RoleMenu_NTA_RoleID_Tbl_Menu_Role_NTA_ID FOREIGN KEY (RoleID)
      REFERENCES Auth.Tbl_Role_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

ALTER TABLE Auth.Tbl_RoleMenu_NTA
   ADD CONSTRAINT FK_Tbl_RoleMenu_NTA_MenuID_Tbl_Menu_NTA_ID FOREIGN KEY (MenuID)
      REFERENCES Auth.Tbl_Menu_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;