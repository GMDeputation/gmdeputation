
<!-- There was oonly one record in the old table so we did not move data over. --> 
CREATE TABLE Global.Tbl_AccomodationBooking_NTA(
	Id int IDENTITY(1,1) PRIMARY KEY,	
	RequestedUserId int NOT NULL,	
	DistrictId int NOT NULL,	
	ChurchId int NOT NULL,	
	AccomodationId int NOT NULL,	
	AdultNo int NULL,
	ChildNo int NULL,
	CheckinDate datetime NOT NULL,
	CheckoutDate datetime NOT NULL,
	ArrivalTime time(7) NULL,
	DepartureTime time(7) NULL,
	Reason nvarchar(100) NULL,
	IsFeedBack bit NOT NULL,
	FeedBack nvarchar(200) NULL,
	IsSubmit bit NOT NULL,
	IsApproved bit NOT NULL,
	ApprovedBy int NULL,	
	ApprovedOn datetime NULL,
	Remarks nvarchar(200) NULL,
	INSERT_USER nvarchar(100) NULL,
	UPDATE_USER nvarchar(100) NULL,
	INSERT_DATETIME DATETIME NULL,
	UPDATE_DATETIME DATETIME NULL, 
 );
 
 
     ALTER TABLE  Global.Tbl_AccomodationBooking_NTA
   ADD CONSTRAINT FK_Tbl_AccomodationBooking_NTA_UserId_Tbl_User_NTA_ID FOREIGN KEY (RequestedUserId)
      REFERENCES Auth.Tbl_User_NTA (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;

    ALTER TABLE  Global.Tbl_AccomodationBooking_NTA
   ADD CONSTRAINT FK_Tbl_AccomodationBooking_NTA_ApprovedBy_Tbl_User_NTA_ID FOREIGN KEY (ApprovedBy)
      REFERENCES Auth.Tbl_User_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;

    ALTER TABLE  Global.Tbl_AccomodationBooking_NTA
   ADD CONSTRAINT FK_Tbl_AccomodationBooking_NTA_DistrictId_Tbl_District_NTA_ID FOREIGN KEY (DistrictId)
      REFERENCES Global.Tbl_District_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
 
     ALTER TABLE  Global.Tbl_AccomodationBooking_NTA
   ADD CONSTRAINT FK_Tbl_AccomodationBooking_NTA_ChurchId_Tbl_Church_NTA_ID FOREIGN KEY (ChurchId)
      REFERENCES Global.Tbl_Church_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;

    ALTER TABLE  Global.Tbl_AccomodationBooking_NTA
   ADD CONSTRAINT FK_Tbl_AccomodationBooking_NTA_AccomodationId_Tbl_ChurchAccommodation_NTA_ID FOREIGN KEY (AccomodationId)
      REFERENCES Global.Tbl_ChurchAccommodation_NTA (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;