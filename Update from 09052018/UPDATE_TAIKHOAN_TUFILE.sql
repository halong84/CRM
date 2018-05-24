USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[UPDATE_TAIKHOAN]    Script Date: 05/09/2018 22:07:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UPDATE_TAIKHOAN_TUFILE]
      @tblTAIKHOAN TAIKHOANType READONLY
AS
BEGIN
      SET NOCOUNT ON;
      MERGE INTO TAIKHOAN t1
      USING @tblTAIKHOAN t2
      ON t1.SOTK = t2.SOTK
      WHEN MATCHED THEN
      UPDATE SET 
			--t1.MAKH = t2.MAKH
			--,t1.SOTK = t2.SOTK
			--,t1.CCY = t2.CCY
			t1.NGAYMO = t2.NGAYMO
			,t1.NGAYDENHAN = t2.NGAYDENHAN
			,t1.NGAYDONG = t2.NGAYDONG
			,t1.HOATDONG = t2.HOATDONG
      WHEN NOT MATCHED THEN
      INSERT (
			MAKH
			,SOTK
			,CCY
			,NGAYMO
			,NGAYDENHAN
			,NGAYDONG
			,HOATDONG
			) 
		VALUES(
			t2.MAKH
			,t2.SOTK
			,t2.CCY
			,t2.NGAYMO
			,t2.NGAYDENHAN
			,t2.NGAYDONG
			,t2.HOATDONG
			);
END


GO

