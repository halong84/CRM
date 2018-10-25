USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[STK_MAT_THEO_NGAY_KH_BAO_MACN]    Script Date: 06/18/2018 15:57:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [dbo].[STK_MAT_THEO_NGAY_KH_BAO_MACN]
 @ngay_kh_bao nvarchar(10),
 @macn nvarchar(10)
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
			,NGAY_TIM_THAY
			,TINH_TRANG
			--,CN_LOAI2_GUI
			--,CN_LOAI1_GUI
			,NGAY_CAP_SO
			,SO_TB_MAT_CN_LOAI2
			,NGAY_BAO_MAT_CN_LOAI2
			,SO_TB_THAY_CN_LOAI2
			,NGAY_BAO_THAY_CN_LOAI2
			,SO_TB_MAT_CN_LOAI1
			,NGAY_BAO_MAT_CN_LOAI1
			,SO_TB_THAY_CN_LOAI1
			,NGAY_BAO_THAY_CN_LOAI1
			,XU_LY
		FROM THONGBAOSTK
		WHERE Convert(Date, NGAY_KH_BAO) = @ngay_kh_bao AND LEFT(TK,4) = @macn
END





