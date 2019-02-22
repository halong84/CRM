use crm
go
create procedure [dbo].[UPDATE_SYSTEM_DETAIL]
@manv nvarchar(50),
@COL_ID nvarchar(50),
@SYS_ID nvarchar(50),
@VALUE nvarchar(100)
as
update SYSDETAIL set
VALUE = @VALUE
where
MANV = @manv and
COL_ID = @COL_ID and
SYS_ID = @SYS_ID