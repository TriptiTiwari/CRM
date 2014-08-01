Create Database DB_CRM

use DB_CRM

Create Table tbl_DealerEntry
(
	ID int identity(1,1) constraint pk_DealerEntryID primary key(ID) not null,
	DealerEntryID nvarchar(MAX) not null,
	CompanyName nvarchar(MAX) not null,
	DealerFirstName nvarchar(MAX) not null,
	DealerLaseName nvarchar(MAX) not null,
	DateOfBirth datetime not null,
	MobileNo nvarchar(MAX) not null,
	PhoneNo nvarchar(MAX) not null,
	DealerAddress nvarchar(MAX) not null,
	City nvarchar(MAX) not null,
	Zip nvarchar(MAX) not null,
	DState nvarchar(MAX) not null,
	Country nvarchar(MAX) not null,
	S_Status nvarchar(MAX)not null,
	C_Date DateTime not null,
)
drop table tbl_DealerEntry
select  * from tbl_DealerEntry