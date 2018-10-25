USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[TAI_KHOAN_THEO_STK]    Script Date: 06/08/2018 15:38:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[TAI_KHOAN_THEO_STK] 
@sotk nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TK.MAKH AS MAKH
	       ,TK.SOTK AS TAIKHOAN
	       ,TK.CCY AS CCY
	       ,KH.HOTEN AS HOTEN
	       ,KH.CMND AS CMND
	       ,KH.NOICAP AS NOICAP
	       ,KH.NGAYCAP AS NGAYCAP
	       ,KH.DIACHI1 AS DIACHI1
	       FROM [dbo].[TAIKHOAN] AS TK 
	       LEFT JOIN 
	       (SELECT KH1.MAKH
				   ,KH1.HOTEN
	               ,KH1.CMND
	               ,NC.NOICAP
	               ,KH1.NGAYCAP
	               ,KH1.DIACHI1
	         FROM KHACHHANG AS KH1
	         LEFT JOIN [dbo].NOICAPCMND AS NC
	         ON KH1.NOICAP = NC.MA_NOICAP
	        ) AS KH
	       ON TK.MAKH = KH.MAKH
	       WHERE TK.SOTK = @sotk
END

