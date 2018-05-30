USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DV_DANGKYDICHVU_KHACHHANG]    Script Date: 05/30/2018 16:52:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DV_DANGKYDICHVU_KHACHHANG]
@tt nvarchar(50)
as
if exists (select 1 from KHACHHANG where CMND = @tt)
select * from KHACHHANG where CMND = @tt
else if exists (select 1 from KHACHHANG where DIENTHOAI1 = @tt)
select * from KHACHHANG where DIENTHOAI1 = @tt
else if exists (select 1 from KHACHHANG where MAKH = @tt)
select * from KHACHHANG where MAKH = @tt
else
select * from KHACHHANG where GPDK = @tt

