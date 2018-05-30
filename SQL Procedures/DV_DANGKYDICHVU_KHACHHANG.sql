use CRM;
go
alter procedure dbo.DV_DANGKYDICHVU_KHACHHANG
@tt nvarchar(50)
as
if exists (select 1 from KHACHHANG where CMND = @tt)
select * from KHACHHANG where CMND = @tt
else if exists (select 1 from KHACHHANG where DIENTHOAI1 = @tt)
select * from KHACHHANG where DIENTHOAI1 = @tt
else 
select * from KHACHHANG where MAKH = @tt
