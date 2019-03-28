USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DT_IPCAS_GET_INFO]    Script Date: 29/03/2019 12:27:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DT_IPCAS_GET_INFO]
@userid nvarchar(50)
as
select max(ipcas.MODIFY_ID) as MODIFY_ID, ipcas.USER_ID, ipcas.MAC, ipcas.MENU_ID, ipcas.CHUCNANG, ipcas.MAPB from (select * from DT_IPCAS where user_id = @userid and STATUS_CONFIRM = '1') ipcas
group by ipcas.USER_ID, ipcas.MAC, ipcas.MENU_ID, ipcas.CHUCNANG, ipcas.MAPB