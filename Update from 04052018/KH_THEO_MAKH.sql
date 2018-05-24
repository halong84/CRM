USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[DANH_SACH_CHI_NHANH]    Script Date: 05/09/2018 13:41:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[KH_THEO_MAKH] 
 @makh nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT KH.MAKH	       ,KH.HOTEN	       ,KH.CMND	       ,KH.NGAYCAP	       ,KH.DIACHI1	       ,KH.GIOITINH	       ,NC.NOICAP	       ,NC.NOICAP_EN	        FROM KHACHHANG AS KH	        LEFT JOIN NOICAPCMND as NC on KH.NOICAP = NC.MA_NOICAP
	        WHERE KH.MAKH = @makh AND KH.LOAIKH = '1'
            ORDER BY KH.MAKH, KH.HOTEN
   
END
GO


