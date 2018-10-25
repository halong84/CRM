USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[STK_MAT_HONG_THEO_NGAY_KH_BAO]    Script Date: 06/05/2018 17:21:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[STK_MAT_HONG_THEO_NGAY_KH_BAO]
 @ngay_kh_bao nvarchar(10)
     
AS
BEGIN
      SET NOCOUNT ON;
      SELECT --,ID
			TK
			,SERIAL
			,DV_CAP_STK
			,CN_CAP_STK
			,SO_DU
			,NGAY_KH_BAO
			--,NGAY_CN_LOAI2_BAO
			--,NGAY_CN_LOAI1_BAO
			--,NGAY_TIM_THAY
			,TINH_TRANG
			--,CN_LOAI2_GUI
			--,CN_LOAI1_GUI
			,NGAY_CAP_SO
		FROM THONGBAOSTK
		WHERE Convert(Date, NGAY_KH_BAO) = @ngay_kh_bao
END




GO


