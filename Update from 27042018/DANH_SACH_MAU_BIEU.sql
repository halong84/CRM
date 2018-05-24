USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DANH_SACH_MAU_BIEU]    Script Date: 04/27/2018 13:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[DANH_SACH_MAU_BIEU] 
    @nghiepvu nvarchar(50),
    @menu_id nvarchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TEN_MAUBIEU, TEN_FILEMAUBIEU FROM [dbo].[MAUBIEU] WHERE NGHIEPVU = @nghiepvu AND MENU_ID = @menu_id
END



