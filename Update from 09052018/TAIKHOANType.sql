USE [CRM]
GO

/****** Object:  UserDefinedTableType [dbo].[CHUYENLUONGType]    Script Date: 05/09/2018 21:45:08 ******/
CREATE TYPE [dbo].[TAIKHOANType] AS TABLE(
	[MAKH] [nvarchar](50) NULL,
	[SOTK] [nvarchar](50) NOT NULL,
	[CCY] [nvarchar](10) NULL,
	[NGAYMO] [nvarchar](50) NULL,
	[NGAYDENHAN] [nvarchar](50) NULL,
	[NGAYDONG] [nvarchar](50) NULL,
	[HOATDONG] [bit] NULL
)
GO


