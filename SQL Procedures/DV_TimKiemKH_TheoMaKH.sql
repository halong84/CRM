USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DV_TimKiemKH_TheoMaKH]    Script Date: 05/30/2018 16:38:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DV_TimKiemKH_TheoMaKH]
	@makh nvarchar(50)
as
	select * from dbo.KHACHHANG where KHACHHANG.MAKH = @makh and LOAIKH = '1'
