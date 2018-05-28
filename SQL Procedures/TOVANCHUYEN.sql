USE [CRM]
GO

/****** Object:  Table [dbo].[TOVANCHUYEN]    Script Date: 05/28/2018 17:56:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TOVANCHUYEN](
	[MAPB] [nvarchar](10) NULL,
	[MATOTRUONG] [nvarchar](20) NULL,
	[MAGIAMSAT1] [nvarchar](20) NULL,
	[MAGIAMSAT2] [nvarchar](20) NULL,
	[MABAOVE] [nvarchar](20) NULL,
	[MALAIXE] [nvarchar](20) NULL,
	[LOAIHANG] [nvarchar](max) NULL,
	[BANGSO] [nvarchar](20) NULL,
	[NOIDEN] [nvarchar](max) NULL,
	[PHUONGTIEN] [nvarchar](200) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


