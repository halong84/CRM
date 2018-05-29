USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DV_TATCATHE]    Script Date: 05/29/2018 16:23:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[DV_TATCATHE]
@tungay datetime,
@denngay datetime,
@mapb nvarchar(50)
AS
SELECT
--ROW_NUMBER() over (ORDER BY HOTEN) as STT,
ID,
HOTEN,
THEODOITHE.SOTK,
SOTHE,
LOAITHE,
NGAYDANGKY,
NGAYNHANTHE,
NGAYGIAOTHE

FROM DBO.KHACHHANG, dbo.THEODOITHE,dbo.TAIKHOAN
WHERE 
THEODOITHE.MAPB = @mapb and
TAIKHOAN.SOTK = THEODOITHE.SOTK and
KHACHHANG.MAKH = TAIKHOAN.MAKH and
CAST(NGAYDANGKY as DATE) >= CAST(@tungay as DATE) and
CAST(NGAYDANGKY as DATE) <= CAST(@denngay as DATE)