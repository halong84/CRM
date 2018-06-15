USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[DV_TOVANCHUYEN_UPDATE]    Script Date: 06/14/2018 15:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DV_TOVANCHUYEN_UPDATE]
@mapb nvarchar(10),
@quyetdinh nvarchar(max),
@gttt bit,
@gtgs1 bit,
@gtgs2 bit,
@gtbv bit,
@gtlx bit,
@tentt nvarchar(50),
@tengs1 nvarchar(50),
@tengs2 nvarchar(50),
@tenbv nvarchar(50),
@tenlx nvarchar(50),
@chucvutt nvarchar(100),
@chucvugs1 nvarchar(100),
@chucvugs2 nvarchar(100),
@chucvubv nvarchar(100),
@chucvulx nvarchar(100),
@cmndtt	nvarchar(20),
@cmndgs1 nvarchar(20),
@cmndgs2 nvarchar(20),
@cmndbv nvarchar(20),
@cmndlx nvarchar(20),
@ngaycaptt nvarchar(20),
@ngaycapgs1 nvarchar(20),
@ngaycapgs2 nvarchar(20),
@ngaycapbv nvarchar(20),
@ngaycaplx nvarchar(20),
@noicaptt nvarchar(20),
@noicapgs1 nvarchar(20),
@noicapgs2 nvarchar(20),
@noicapbv nvarchar(20),
@noicaplx nvarchar(20),
@loaihang nvarchar(100),
@noiden nvarchar(100),
@phuongtien nvarchar(100)
as
if exists (select 1 from TOVANCHUYEN where MAPB = @mapb)
begin
	update TOVANCHUYEN set
	quyetdinh = @quyetdinh,
	GTTT = @gttt,
	GTGS1 = @gtgs1,
	GTGS2 = @gtgs2,
	GTBV = @gtbv,
	GTLX = @gtlx,
	TENTT = @tentt,
	TENGS1 = @tengs1,
	TENGS2 = @tengs2,
	TENBV = @tenbv,
	TENLX = @tenlx,
	CHUCVUTT = @chucvutt,
	CHUCVUGS1 = @chucvugs1,
	CHUCVUGS2 = @chucvugs2,
	CHUCVUBV = @chucvubv,
	CHUCVULX = @chucvulx,
	CMNDTT = @cmndtt,
	CMNDGS1 = @cmndgs1,
	CMNDGS2 = @cmndgs2,
	CMNDBV = @cmndbv,
	CMNDLX = @cmndlx,
	NGAYCAPTT = @ngaycaptt,
	NGAYCAPGS1 = @ngaycapgs1,
	NGAYCAPGS2 = @ngaycapgs2,
	NGAYCAPBV = @ngaycapbv,
	NGAYCAPLX = @ngaycaplx,
	NOICAPTT = @noicaptt,
	NOICAPGS1 = @noicapgs1,
	NOICAPGS2 = @noicapgs2,
	NOICAPBV = @noicapbv,
	NOICAPLX = @noicaplx,
	LOAIHANG = @loaihang,
	NOIDEN = @noiden,
	PHUONGTIEN = @phuongtien
	WHERE
	MAPB = @mapb
end
else
begin
	INSERT INTO TOVANCHUYEN
	(MAPB,
	QUYETDINH,
	GTTT,
	GTGS1,
	GTGS2,
	GTBV,
	GTLX,
	TENTT,
	TENGS1,
	TENGS2,
	TENBV,
	TENLX,
	CHUCVUTT,
	CHUCVUGS1,
	CHUCVUGS2,
	CHUCVUBV,
	CHUCVULX,
	CMNDTT,
	CMNDGS1,
	CMNDGS2,
	CMNDBV,
	CMNDLX,
	NGAYCAPTT,
	NGAYCAPGS1,
	NGAYCAPGS2,
	NGAYCAPBV,
	NGAYCAPLX,
	NOICAPTT,
	NOICAPGS1,
	NOICAPGS2,
	NOICAPBV,
	NOICAPLX,
	LOAIHANG,
	NOIDEN,
	PHUONGTIEN)
	VALUES
	(@mapb,
	@quyetdinh,
	@gttt,
	@gtgs1,
	@gtgs2,
	@gtbv,
	@gtlx,
	@tentt,
	@tengs1,
	@tengs2,
	@tenbv,
	@tenlx,
	@chucvutt,
	@chucvugs1,
	@chucvugs2,
	@chucvubv,
	@chucvulx,
	@cmndtt,
	@cmndgs1,
	@cmndgs2,
	@cmndbv,
	@cmndlx,
	@ngaycaptt,
	@ngaycapgs1,
	@ngaycapgs2,
	@ngaycapbv,
	@ngaycaplx,
	@noicaptt,
	@noicapgs1,
	@noicapgs2,
	@noicapbv,
	@noicaplx,
	@loaihang,
	@noiden,
	@phuongtien)
end