drop table Auth.Tbl_Menu_NTA

CREATE TABLE Auth.Tbl_Menu_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	MenuOldID uniqueidentifier NOT NULL,
	Name nvarchar(100) NOT NULL,
	Icon nvarchar(20) NOT NULL,
	Target nvarchar(50) NULL,
	MenuGroupId int NOT NULL,
	MenuGroupIdOld uniqueidentifier NOT NULL,
	StartingPath nvarchar(100) NOT NULL,
	Position int NOT NULL,
	IsActive bit NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 	
);

INSERT INTO Auth.Tbl_Menu_NTA (MenuOldID,Name,Icon,Target,MenuGroupId,MenuGroupIdOld,StartingPath,Position,IsActive,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME)
SELECT id,Name,Icon,Target,1,MenuGroupId,StartingPath,Position,IsActive,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
FROM Auth.Tbl_Menu;


ALTER TABLE Auth.Tbl_MENU_nta
   ADD CONSTRAINT FK_Tbl_Menu_NTA_MenuGroupID_Tbl_Menu_Group_NTA_ID FOREIGN KEY (MENUGROUPID)
      REFERENCES Auth.Tbl_MENUGROUP_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

INSERT INTO [Auth].[Tbl_Menu_NTA]
           ([MenuOldID]
           ,[Name]
           ,[Icon]
           ,[Target]
           ,[MenuGroupId]
           ,[MenuGroupIdOld]
           ,[StartingPath]
           ,[Position]
           ,[IsActive])
     VALUES
           ('F710189F-E82B-4A1A-8C39-080654EC9CF2'
           ,'Macro Schedule'
           ,'ti-world'
           ,'/macroSchedule'
           ,8
           ,'F2EFF949-9BCA-47B8-9EC4-7D1053B134AB'
           ,'/macroSchedule'
           ,4
           ,1)
