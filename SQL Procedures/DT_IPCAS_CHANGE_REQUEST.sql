use crm
go
create procedure [dbo].[DT_IPCAS_CHANGE_REQUEST]
@userid nvarchar(50),
@mac nvarchar(50),
@menu nvarchar(50),
@chucnang nvarchar(50),
@hoatdong bit,
@yeucaukhac nvarchar(200),
@mapb nvarchar(50)
as
insert DT_IPCAS
(USER_ID, mac, menu_id, chucnang, hoatdong, DATE_REQUEST, STATUS_CONFIRM, YEUCAUKHAC, MAPB)
VALUES
(@userid, @mac, @menu, @chucnang, @hoatdong, CURRENT_TIMESTAMP, 0, @yeucaukhac, @mapb)