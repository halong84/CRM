use crm
go
create procedure [dbo].[INSERT_SYSTEM_DETAIL]
@manv nvarchar(50),
@COL_ID nvarchar(50),
@SYS_ID nvarchar(50),
@VALUE nvarchar(100)
as
insert SYSDETAIL (manv, COL_ID, SYS_ID, VALUE) values (@manv, @COL_ID, @SYS_ID, @VALUE)
