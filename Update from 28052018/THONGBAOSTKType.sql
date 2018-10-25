USE [CRM]
GO

/****** Object:  UserDefinedTableType [dbo].[THONGBAOSTKType]    Script Date: 06/08/2018 15:40:15 ******/
CREATE TYPE [dbo].[THONGBAOSTKType] AS TABLE(
	[TK] [nvarchar](50) NULL,
	[SERIAL] [nvarchar](50) NULL,
	[DV_CAP_STK] [nvarchar](300) NULL,
	[CN_CAP_STK] [nvarchar](200) NULL,
	[SO_DU] [decimal](19, 4) NULL,
	[NGAY_KH_BAO] [nvarchar](50) NULL,
	[NGAY_TIM_THAY] [nvarchar](10) NULL,
	[TINH_TRANG] [nvarchar](50) NULL,
	[CN_LOAI2_GUI] [bit] NULL,
	[CN_LOAI1_GUI] [bit] NULL,
	[NGAY_CAP_SO] [nvarchar](10) NULL,
	[SO_TB_MAT_CN_LOAI2] [nvarchar](50) NULL,
	[NGAY_BAO_MAT_CN_LOAI2] [nvarchar](10) NULL,
	[SO_TB_THAY_CN_LOAI2] [nvarchar](50) NULL,
	[NGAY_BAO_THAY_CN_LOAI2] [nvarchar](10) NULL,
	[SO_TB_MAT_CN_LOAI1] [nvarchar](50) NULL,
	[NGAY_BAO_MAT_CN_LOAI1] [nvarchar](10) NULL,
	[SO_TB_THAY_CN_LOAI1] [nvarchar](50) NULL,
	[NGAY_BAO_THAY_CN_LOAI1] [nvarchar](10) NULL
)
GO


