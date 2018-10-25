USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[UPDATE_THONGBAOSTK_KH]    Script Date: 06/08/2018 15:41:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UPDATE_THONGBAOSTK_KH]
      @tblTHONGBAOSTK THONGBAOSTKType READONLY
AS
BEGIN
      SET NOCOUNT ON;
      MERGE INTO THONGBAOSTK t1
      USING @tblTHONGBAOSTK t2
      ON t1.TK = t2.TK AND t1.SERIAL = t2.SERIAL AND Convert(date,t1.NGAY_KH_BAO) = Convert(date,t2.NGAY_KH_BAO)
      WHEN MATCHED THEN
      UPDATE SET --t1.TK = t2.TK
				--t1.SERIAL = t2.SERIAL
				--t1.DV_CAP_STK = t2.DV_CAP_STK
				--,t1.CN_CAP_STK = t2.CN_CAP_STK
				--,t1.SO_DU = t2.SO_DU
				--t1.NGAY_KH_BAO = t2.NGAY_KH_BAO
				t1.NGAY_TIM_THAY = t2.NGAY_TIM_THAY
				,t1.TINH_TRANG = t2.TINH_TRANG
				--,t1.CN_LOAI2_GUI = 0
				--,t1.CN_LOAI1_GUI = 0
				--,t1.NGAY_CAP_SO = t2.NGAY_CAP_SO
				--,t1.SO_TB_MAT_CN_LOAI2 = t2.SO_TB_MAT_CN_LOAI2
				--,t1.NGAY_BAO_MAT_CN_LOAI2 = t2.NGAY_BAO_MAT_CN_LOAI2
				--,t1.SO_TB_THAY_CN_LOAI2 = t2.SO_TB_THAY_CN_LOAI2
				--,t1.NGAY_BAO_THAY_CN_LOAI2 = t2.NGAY_BAO_THAY_CN_LOAI2
				--,t1.SO_TB_MAT_CN_LOAI1 = t2.SO_TB_MAT_CN_LOAI1
				--,t1.NGAY_BAO_MAT_CN_LOAI1 = t2.NGAY_BAO_MAT_CN_LOAI1
				--,t1.SO_TB_THAY_CN_LOAI1 = t2.SO_TB_THAY_CN_LOAI1
				--,t1.NGAY_BAO_THAY_CN_LOAI1 = t2.NGAY_BAO_THAY_CN_LOAI1
      WHEN NOT MATCHED THEN
      INSERT (TK
			,SERIAL
			,DV_CAP_STK
			,CN_CAP_STK
			,SO_DU
			,NGAY_KH_BAO
			,NGAY_TIM_THAY
			,TINH_TRANG
			,CN_LOAI2_GUI
			,CN_LOAI1_GUI
			,NGAY_CAP_SO
			,SO_TB_MAT_CN_LOAI2
			,NGAY_BAO_MAT_CN_LOAI2
			,SO_TB_THAY_CN_LOAI2
			,NGAY_BAO_THAY_CN_LOAI2
			,SO_TB_MAT_CN_LOAI1
			,NGAY_BAO_MAT_CN_LOAI1
			,SO_TB_THAY_CN_LOAI1
			,NGAY_BAO_THAY_CN_LOAI1
			) 
	  VALUES (t2.TK
			,t2.SERIAL
			,t2.DV_CAP_STK
			,t2.CN_CAP_STK
			,t2.SO_DU
			,t2.NGAY_KH_BAO
			,t2.NGAY_TIM_THAY
			,t2.TINH_TRANG
			,t2.CN_LOAI2_GUI
			,t2.CN_LOAI1_GUI
			,t2.NGAY_CAP_SO
			,t2.SO_TB_MAT_CN_LOAI2
			,t2.NGAY_BAO_MAT_CN_LOAI2
			,t2.SO_TB_THAY_CN_LOAI2
			,t2.NGAY_BAO_THAY_CN_LOAI2
			,t2.SO_TB_MAT_CN_LOAI1
			,t2.NGAY_BAO_MAT_CN_LOAI1
			,t2.SO_TB_THAY_CN_LOAI1
			,t2.NGAY_BAO_THAY_CN_LOAI1  
			);
END





GO


