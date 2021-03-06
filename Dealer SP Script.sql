USE [DB_CRM]
GO
/****** Object:  StoredProcedure [dbo].[SP_DealerEntry]    Script Date: 01-08-2014 14:32:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_DealerEntry]
	@Flag int=null,

	@DealerEntryID nvarchar(MAX),
	@CompanyName nvarchar(MAX),
	@DealerFirstName nvarchar(MAX),
	@DealerLaseName nvarchar(MAX),
	@DateOfBirth datetime,
	@MobileNo nvarchar(MAX),
	@PhoneNo nvarchar(MAX),
	@DealerAddress nvarchar(MAX),
	@City nvarchar(MAX),
	@Zip nvarchar(MAX),
	@DState nvarchar(MAX),
	@Country nvarchar(MAX),
	@S_Status nvarchar(MAX),
	@C_Date datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@Flag = 1)
	begin
	Insert Into tbl_DealerEntry(DealerEntryID, CompanyName, DealerFirstName, DealerLaseName, DateOfBirth, MobileNo, PhoneNo, DealerAddress, City, Zip, DState, Country, S_Status, C_Date) values (@DealerEntryID, @CompanyName, @DealerFirstName, @DealerLaseName, @DateOfBirth, @MobileNo, @PhoneNo, @DealerAddress, @City, @Zip, @DState, @Country, @S_Status, @C_Date) 
	end
END
