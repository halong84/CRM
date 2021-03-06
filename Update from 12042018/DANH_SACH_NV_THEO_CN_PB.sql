USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DANH_SACH_NV_THEO_CN_CV]    Script Date: 04/14/2018 06:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DANH_SACH_NV_THEO_CN_PB] 
@macn nvarchar(50),
@mapb nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT NV.*,
	       PB.TENPB 
	FROM NHANVIEN NV INNER JOIN PHONGBAN PB ON (NV.MAPB = PB.MAPB AND NV.MACN=PB.MACN)
    WHERE NV.MACN= @macn AND NV.MaPB=@mapb
    ORDER BY nv.MACN
END







