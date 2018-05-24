USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[DANH_SACH_NV_THEO_PB_CV]    Script Date: 04/14/2018 23:29:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[NHAN_VIEN_THEO_MANV] 
@manv nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[NHANVIEN] WHERE MANV = @manv
END







GO


