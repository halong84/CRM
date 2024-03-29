USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[XAC_THUC_USER]    Script Date: 04/27/2018 17:09:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[XAC_THUC_USER] 
	-- Add the parameters for the stored procedure here
	@user_id nvarchar(50),
	@user_pass nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT USER_ID
		   ,USER_PASS
		   ,GROUP_LIST
		   ,MANV
		   ,TENNV
		   ,CHUCVU
		   ,_USER.MACN
		   ,GHICHU
		   ,_USER.MAPB
		   ,CHINHANH.TENCN AS TENCN
		   ,CHINHANH.TENCN_EN AS TENCN_EN
		   ,CHINHANH.SDT AS SDT_HS
		   ,CHINHANH.FAX AS FAX_HS
		   ,CHINHANH.DIACHI AS DIACHICN
		   ,PHONGBAN.TENPB AS TENPB
		   ,PHONGBAN.TENPB_EN AS TENPB_EN
		   ,PHONGBAN.SDT AS SDT_PB
		   ,PHONGBAN.FAX AS FAX_PB
		   ,PHONGBAN.DIACHI AS DIACHIPB
		   ,PHONGBAN.HS
	FROM	[dbo].[_USER]
	LEFT JOIN CHINHANH ON CHINHANH.MACN = _USER.MACN
	LEFT JOIN PHONGBAN ON PHONGBAN.MAPB = _USER.MAPB 
	WHERE USER_ID = @user_id AND USER_PASS = @user_pass AND KICHHOAT = '1'
END
