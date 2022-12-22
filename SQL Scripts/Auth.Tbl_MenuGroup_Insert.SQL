DROP TABLE Auth.Tbl_MenuGroup_NTA

CREATE TABLE Auth.Tbl_MenuGroup_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(50) NOT NULL,
	DisplayPosition int NOT NULL,
	Sequence int NULL,
	Category nvarchar(100) NULL,
	Target nvarchar(100) NULL,
	Icon nvarchar(20) NULL,
	IsActive bit NOT NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
	old_key nvarchar(100) NULL,
);

INSERT INTO Auth.Tbl_MenuGroup_NTA (Name,DisplayPosition,Sequence,Category,Target,Icon,IsActive,INSERT_USER,UPDATE_USER,INSERT_DATETIME,UPDATE_DATETIME,old_key)
SELECT Name,DisplayPosition,Sequence,Category,Target,Icon,IsActive,'NTA_CREATIVE','NTA_CREATIVE',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,id
FROM Auth.Tbl_MenuGroup;


select x.menugroupid,y.old_key,y.id from 
auth.Tbl_Menu_NTA x
join auth.Tbl_MenuGroup_NTA y on x.menugroupid = y.old_key


updaTE x  
SET x.menugroupid = y.id
from auth.Tbl_Menu_NTA x
join  auth.Tbl_MenuGroup_NTA y on x.menugroupidold = y.old_key


ALTER TABLE [SFADB].[Auth].[Tbl_Menu_NTA]
DROP COLUMN Menugroupidold;