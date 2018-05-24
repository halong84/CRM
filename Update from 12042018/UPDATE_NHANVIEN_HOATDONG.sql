USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[UPDATE_NHANVIEN_HOATDONG]    Script Date: 04/16/2018 10:58:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[UPDATE_NHANVIEN_HOATDONG]
      @tblNHANVIEN NHANVIENType READONLY
AS

BEGIN		
      SET NOCOUNT ON;
      UPDATE NHANVIEN SET HOATDONG = 0
      WHERE NOT EXISTS (SELECT MANV FROM @tblNHANVIEN nv1 WHERE NHANVIEN.MANV = nv1.MANV)
	  
END



GO


