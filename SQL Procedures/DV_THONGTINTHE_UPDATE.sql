use CRM;
go
create procedure dbo.dv_thongtinthe_update
@id int,
@sothe nvarchar(20),
@ngaynhan Date,
@ngaygiao Date
as
update THEODOITHE set
SOTHE = @sothe,
NGAYNHANTHE = @ngaynhan,
NGAYGIAOTHE = @ngaygiao
where ID = @id