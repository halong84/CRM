USE [CRM]
GO

/****** Object:  Table [dbo].[NHANVIEN_TEMP]    Script Date: 04/16/2018 11:01:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NHANVIEN_TEMP](
	[MANV] [nvarchar](50) NOT NULL,
	[HOTEN] [nvarchar](100) NULL,
	[CHUCVU] [nvarchar](50) NULL,
	[MAPB] [nvarchar](50) NULL,
	[MACN] [nvarchar](50) NULL,
	[UYQUYEN] [nvarchar](max) NULL,
	[GIOITINH] [bit] NULL,
	[NGAYSINH] [nvarchar](50) NULL,
	[HOATDONG] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


