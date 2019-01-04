USE [CRM]
GO

/****** Object:  Table [dbo].[THONGBAOSTK]    Script Date: 06/08/2018 15:39:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[THONGBAOSTK](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TK] [nvarchar](50) NULL,
	[SERIAL] [nvarchar](50) NULL,
	[DV_CAP_STK] [nvarchar](300) NULL,
	[CN_CAP_STK] [nvarchar](200) NULL,
	[SO_DU] [decimal](19, 4) NULL,
	[NGAY_KH_BAO] [datetime] NULL,
	[NGAY_TIM_THAY] [date] NULL,
	[TINH_TRANG] [nvarchar](50) NULL,
	[CN_LOAI2_GUI] [bit] NULL,
	[CN_LOAI1_GUI] [bit] NULL,
	[NGAY_CAP_SO] [date] NULL,
	[SO_TB_MAT_CN_LOAI2] [nvarchar](50) NULL,
	[NGAY_BAO_MAT_CN_LOAI2] [date] NULL,
	[SO_TB_THAY_CN_LOAI2] [nvarchar](50) NULL,
	[NGAY_BAO_THAY_CN_LOAI2] [date] NULL,
	[SO_TB_MAT_CN_LOAI1] [nvarchar](50) NULL,
	[NGAY_BAO_MAT_CN_LOAI1] [date] NULL,
	[SO_TB_THAY_CN_LOAI1] [nvarchar](50) NULL,
	[NGAY_BAO_THAY_CN_LOAI1] [date] NULL,
 CONSTRAINT [PK_THONGBAOSTK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[THONGBAOSTK] ADD  CONSTRAINT [DF_THONGBAOSTK_CN_LOAI2_GUI]  DEFAULT ((0)) FOR [CN_LOAI2_GUI]
GO

ALTER TABLE [dbo].[THONGBAOSTK] ADD  CONSTRAINT [DF_THONGBAOSTK_CN_LOAI1_GUI]  DEFAULT ((0)) FOR [CN_LOAI1_GUI]
GO

