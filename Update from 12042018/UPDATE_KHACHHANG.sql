USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_KHACHHANG]    Script Date: 04/12/2018 16:19:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[UPDATE_KHACHHANG]
@nguoicapnhat nvarchar(50)
AS

BEGIN		
      SET NOCOUNT ON;
      MERGE INTO KHACHHANG t1
      USING KHACHHANG_TEMP t2
      ON t1.MAKH = t2.MAKH
      WHEN MATCHED THEN
      UPDATE SET t1.MAKH = t2.MAKH
				,t1.HOTEN = t2.HOTEN
				,t1.DIACHI1 = t2.DIACHI1
				,t1.DIACHI2 = t2.DIACHI2
				,t1.DIENTHOAI1 = t2.DIENTHOAI1
				,t1.DIENTHOAI2 = t2.DIENTHOAI2
				,t1.EMAIL = t2.EMAIL
				,t1.CMND = t2.CMND
				,t1.NGAYCAP = t2.NGAYCAP
				,t1.NOICAP = t2.NOICAP
				,t1.NGAYSINH = t2.NGAYSINH
				,t1.GIOITINH = t2.GIOITINH
				,t1.LINHVUC = t2.LINHVUC
				,t1.WEBSITE = t2.WEBSITE
				,t1.GPDK = t2.GPDK
				,t1.QDTL = t2.QDTL
				,t1.MST = t2.MST
				,t1.LOAIKH = t2.LOAIKH
				,t1.THUNHAP = t2.THUNHAP
				,t1.SOTHICH = t2.SOTHICH
				,t1.MANV = t2.MANV
				,t1.NHGIAODICH = t2.NHGIAODICH
				,t1.GHICHU = t2.GHICHU
				,t1.MACN = t2.MACN
				,t1.TINHTRANG = t2.TINHTRANG
				,t1.CTLOAIKH = t2.CTLOAIKH
				,t1.TINH = t2.TINH
				,t1.HUYEN = t2.HUYEN
				,t1.XA = t2.XA
				,t1.LOAIKH_IPCAS = t2.LOAIKH_IPCAS
				,t1.NGAYKETHON = t2.NGAYKETHON
				,t1.NGAYTHANHLAP = t2.NGAYTHANHLAP
				,t1.NGAYTAO = t2.NGAYTAO
				,t1.DOITUONGKH = t2.DOITUONGKH
				,t1.DOITUONGDN = t2.DOITUONGDN
				,t1.VONDAUTU = t2.VONDAUTU
				,t1.SOLAODONG = t2.SOLAODONG
				,t1.DSXNK = t2.DSXNK
				,t1.NGAYTLNGANH = t2.NGAYTLNGANH
				,t1.NGAYCAPNHAT = GETDATE()
				,t1.NGUOICAPNHAT = @nguoicapnhat
      WHEN NOT MATCHED THEN
      INSERT (
			 MAKH
			 ,HOTEN
			 ,DIACHI1
			 ,DIACHI2
			 ,DIENTHOAI1
			 ,DIENTHOAI2
			 ,EMAIL
			 ,CMND
			 ,NGAYCAP
			 ,NOICAP
			 ,NGAYSINH
			 ,GIOITINH
			 ,LINHVUC
			 ,WEBSITE
			 ,GPDK
			 ,QDTL
			 ,MST
			 ,LOAIKH
			 ,THUNHAP
			 ,SOTHICH
			 ,MANV
			 ,NHGIAODICH
			 ,GHICHU
			 ,MACN
			 ,TINHTRANG
			 ,CTLOAIKH
			 ,TINH
			 ,HUYEN
			 ,XA
			 ,LOAIKH_IPCAS
			 ,NGAYKETHON
			 ,NGAYTHANHLAP
			 ,NGAYTAO
			 ,DOITUONGKH
			 ,DOITUONGDN
			 ,VONDAUTU
			 ,SOLAODONG
			 ,DSXNK
			 ,NGAYTLNGANH
			 ,NGAYCAPNHAT
			 ,NGUOICAPNHAT
			 )
	  VALUES (
	         t2.MAKH
			 ,t2.HOTEN
			 ,t2.DIACHI1
			 ,t2.DIACHI2
			 ,t2.DIENTHOAI1
			 ,t2.DIENTHOAI2
			 ,t2.EMAIL
			 ,t2.CMND
			 ,t2.NGAYCAP
			 ,t2.NOICAP
			 ,t2.NGAYSINH
			 ,t2.GIOITINH
			 ,t2.LINHVUC
			 ,t2.WEBSITE
			 ,t2.GPDK
			 ,t2.QDTL
			 ,t2.MST
			 ,t2.LOAIKH
			 ,t2.THUNHAP
			 ,t2.SOTHICH
			 ,t2.MANV
			 ,t2.NHGIAODICH
			 ,t2.GHICHU
			 ,t2.MACN
			 ,t2.TINHTRANG
			 ,t2.CTLOAIKH
			 ,t2.TINH
			 ,t2.HUYEN
			 ,t2.XA
			 ,t2.LOAIKH_IPCAS
			 ,t2.NGAYKETHON
			 ,t2.NGAYTHANHLAP
			 ,t2.NGAYTAO
			 ,t2.DOITUONGKH
			 ,t2.DOITUONGDN
			 ,t2.VONDAUTU
			 ,t2.SOLAODONG
			 ,t2.DSXNK
			 ,t2.NGAYTLNGANH
			 ,GETDATE()
			 ,@nguoicapnhat
			 );	 
END
