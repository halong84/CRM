use CRM;
go
create procedure dbo.DV_TOVANCHUYEN_INSERT
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
insert into tovanchuyen
(mapb, matotruong, magiamsat1, magiamsat2, mabaove, malaixe, bangso, noiden, phuongtien) 
values
(@mapb, @matotruong, @magiamsat1, @magiamsat2, @mabaove, @malaixe, @bangso, @noiden, @phuongtien)