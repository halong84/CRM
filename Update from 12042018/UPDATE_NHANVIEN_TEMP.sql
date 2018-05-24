USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[UPDATE_NHANVIEN_TEMP]    Script Date: 04/16/2018 10:59:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UPDATE_NHANVIEN_TEMP]
      @tblNHANVIEN NHANVIENType READONLY
AS

BEGIN		
      SET NOCOUNT ON;
      DELETE FROM NHANVIEN_TEMP
      MERGE INTO NHANVIEN_TEMP t1
      USING @tblNHANVIEN t2
      ON t1.MANV = t2.MANV
      WHEN MATCHED THEN
      UPDATE SET --t1.MANV = t2.MANV
				t1.HOTEN = t2.HOTEN
				,t1.CHUCVU = t2.CHUCVU
				,t1.MAPB = t2.MAPB
				,t1.MACN = t2.MACN
				--,t1.UYQUYEN = t2.UYQUYEN
				,t1.GIOITINH = t2.GIOITINH
				,t1.NGAYSINH = t2.NGAYSINH
				--,t1.HOATDONG = t2.HOATDONG
      WHEN NOT MATCHED THEN
      INSERT (
			 MANV
			,HOTEN
			,CHUCVU
			,MAPB
			,MACN
			,UYQUYEN
			,GIOITINH
			,NGAYSINH
			,HOATDONG
			 )
	  VALUES (
	         t2.MANV
			,t2.HOTEN
			,t2.CHUCVU
			,t2.MAPB
			,t2.MACN
			,t2.UYQUYEN
			,t2.GIOITINH
			,t2.NGAYSINH
			,t2.HOATDONG
			 );
	  UPDATE NHANVIEN_TEMP SET NGAYSINH = N'01/01/1900' WHERE ISDATE(NGAYSINH) = 0
END


GO

