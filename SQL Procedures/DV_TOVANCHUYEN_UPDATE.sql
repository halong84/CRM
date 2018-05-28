use CRM;
go
create procedure dbo.DV_TOVANCHUYEN_UPDATE
@mapb nvarchar(10),
@matotruong nvarchar(10),
@magiamsat1 nvarchar(10),
@magiamsat2 nvarchar(10),
@mabaove nvarchar(10),
@malaixe nvarchar(10),
@bangso nvarchar(20),
@noiden nvarchar(max),
@phuongtien nvarchar(100)
as
update tovanchuyen set
matotruong = @matotruong,
magiamsat1 = @magiamsat1,
magiamsat2 = @magiamsat2,
mabaove = @mabaove,
malaixe = @malaixe,
bangso = @bangso,
noiden = @noiden,
phuongtien = @phuongtien
where
mapb = @mapb