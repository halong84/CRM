USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DV_TimKiemKH_TheoCMND]    Script Date: 05/30/2018 16:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DV_TimKiemKH_TheoCMND]
	@cmnd nvarchar(50)
as
	select * from dbo.KHACHHANG where CMND = @cmnd and loaikh = '1'
