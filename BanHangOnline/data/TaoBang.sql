﻿CREATE DATABASE BANHANGONLINE
GO

USE BANHANGONLINE
GO


-- Nhân Viên
CREATE TABLE NHANVIEN
(
	MANV INT PRIMARY KEY IDENTITY(15150001,1),
	HOTEN NVARCHAR(250),
	NGAYSINH DATE,
	GIOITINH NCHAR(5),
	DIENTHOAI NCHAR(20),
	MAIL NVARCHAR(250),
	DIACHI NVARCHAR(250),
	NGAYLAM DATE,
	LUONG MONEY,
	USERNAMES NVARCHAR(100),
	PASSWORDS NVARCHAR(100),
	TRANGTHAI INT
)
GO



-- Khách Hàng
CREATE TABLE KHACHHANG
(	
	MAKH INT PRIMARY KEY IDENTITY(12120001,1),
	HOTEN NVARCHAR(250),
	NGAYSINH DATE,
	GIOITINH NCHAR(5),
	DIENTHOAI NCHAR(20),
	MAIL NVARCHAR(250),
	DIACHI NVARCHAR(250),
	NGAYDK DATE
)
GO

---- Nhà Cung Cấp
--CREATE TABLE NHACUNGCAP
--(
--	MACC INT PRIMARY KEY IDENTITY(13130001,1),
--	TENCC NVARCHAR(250),
--	DIACHI NVARCHAR(250),
--	DIENTHOAI NCHAR(20)
--)
--GO

-- Nhà Sản Xuất
CREATE TABLE NHASANXUAT
(
	MASX INT PRIMARY KEY IDENTITY(14140001,1),
	TENSX NVARCHAR(250),
	QUOCGIA NVARCHAR(250)
)
GO

-- Loại Sản Phẩm
CREATE TABLE LOAISP
(
	MALOAI INT PRIMARY KEY IDENTITY(16160001,1),
	TENLOAI NVARCHAR(250),
	DVT NVARCHAR(100)						-- Đơn vị tính
)
GO

-- Sản PHẩm
CREATE TABLE SANPHAM
(
	MASP INT PRIMARY KEY IDENTITY(17170001,1),
	TENSP NVARCHAR(250),
	GIA MONEY DEFAULT 0,
	CHITIET NVARCHAR(MAX),			-- Chi Tiết
	IMAGES NVARCHAR(250),
	TRANGTHAI INT,
	GIAMGIA MONEY,
	SL INT,						-- Số lượng
	MASX INT,
	MALOAI INT
)
GO

-- Đơn Đặt Hàng
CREATE TABLE DONDH
(
	MADH INT PRIMARY KEY IDENTITY(18180001,1),
	MASP INT,
	MAKH INT,
	MANV INT,
	SLD INT DEFAULT 0,
	THANHTIEN MONEY DEFAULT 0,
	NGAYDH DATETIME
)
GO

-- Phiếu Xuất
CREATE TABLE PHIEUXUAT
(
	MAPX INT PRIMARY KEY IDENTITY(19190001,1),
	MAKH INT,
	MASP INT,
	MANV INT,
	MADH INT,
	SLX INT DEFAULT 0,
	THANHTIEN MONEY DEFAULT 0,
	NGAYXUAT DATETIME
)
GO


-- Tạo Khóa Ngoại

-- Sản Phẩm


ALTER TABLE dbo.SANPHAM
	ADD CONSTRAINT FRK_SANPHAM_NHASANXUAT
	FOREIGN KEY(MASX)
	REFERENCES dbo.NHASANXUAT(MASX)
GO

ALTER TABLE dbo.SANPHAM
	ADD CONSTRAINT FRK_SANPHAM_LOAISP
	FOREIGN KEY(MALOAI)
	REFERENCES dbo.LOAISP(MALOAI)
GO

-- Đơn Đặt Hàng
ALTER TABLE dbo.DONDH
	ADD CONSTRAINT FRK_DONDH_SANPHAM
	FOREIGN KEY(MASP)
	REFERENCES dbo.SANPHAM(MASP)
GO

ALTER TABLE dbo.DONDH
	ADD CONSTRAINT FRK_DONDH_KHACHHANG
	FOREIGN KEY(MAKH)
	REFERENCES dbo.KhachHang(MaKH)
GO

ALTER TABLE dbo.DONDH
	ADD CONSTRAINT FRK_DONDH_NHANVIEN
	FOREIGN KEY(MANV)
	REFERENCES dbo.NHANVIEN(MANV)
GO

-- Phiếu Xuất
ALTER TABLE dbo.PHIEUXUAT
	ADD CONSTRAINT FRK_PHIEUXUAT_NHANVIEN
	FOREIGN KEY(MANV)
	REFERENCES dbo.NHANVIEN(MANV)
GO

ALTER TABLE dbo.PHIEUXUAT
	ADD CONSTRAINT FRK_PHIEUXUAT_KHACHHANG
	FOREIGN KEY(MAKH)
	REFERENCES dbo.KhachHang(MaKH)
GO

ALTER TABLE dbo.PHIEUXUAT
	ADD CONSTRAINT FRK_PHIEUXUAT_SANPHAM
	FOREIGN KEY(MASP)
	REFERENCES dbo.SANPHAM(MASP)
GO

ALTER TABLE dbo.PHIEUXUAT 
	ADD CONSTRAINT FRK_PHIEUXUAT_DONDH
	FOREIGN KEY(MADH)
	REFERENCES dbo.DONDH(MADH)
GO


--- Store procedure
CREATE PROCEDURE sp_ThemNhanVien
@HOTEN NVARCHAR(250),
@NGAYSINH DATE,
@GIOITINH NCHAR(5),
@DIENTHOAI NCHAR(20),
@MAIL NVARCHAR(250),
@DIACHI NVARCHAR(250),
@NGAYLAM DATE,
@LUONG MONEY,
@USERNAMES NVARCHAR(100),
@PASSWORDS NVARCHAR(100),
@TRANGTHAI INT
AS
BEGIN
    INSERT INTO dbo.NHANVIEN VALUES(@HOTEN,@NGAYSINH,@GIOITINH,@DIENTHOAI,@MAIL,@DIACHI,@NGAYLAM,@LUONG,@USERNAMES,@PASSWORDS,@TRANGTHAI)
END
GO


CREATE PROCEDURE sp_ThemKhachHang
@HOTEN NVARCHAR(250),
@NGAYSINH DATE,
@GIOITINH NCHAR(5),
@DIENTHOAI NCHAR(20),
@MAIL NVARCHAR(250),
@DIACHI NVARCHAR(250),
@NGAYDK DATE
AS
BEGIN
    INSERT INTO dbo.KHACHHANG VALUES(@HOTEN,@NGAYSINH,@GIOITINH,@DIENTHOAI,@MAIL,@DIACHI,@NGAYDK)
END
GO

CREATE PROCEDURE sp_ThemNhaSanXuat
@TENSX NVARCHAR(250),
@QUOCGIA NVARCHAR(250)
AS
BEGIN
    INSERT INTO dbo.NHASANXUAT VALUES(@TENSX,@QUOCGIA)
END
GO


CREATE PROCEDURE sp_ThemLoaiSanPham
@TENLOAI NVARCHAR(100),
@DVT NVARCHAR(100)
AS
BEGIN
    INSERT INTO dbo.LOAISP VALUES(@TENLOAI,@DVT)
END
GO


CREATE PROCEDURE sp_ThemSanPham
@TENSP NVARCHAR(250),
@GIA MONEY,
@CHITIET NVARCHAR(MAX),
@IMAGES NVARCHAR(250),
@TRANGTHAI INT,
@GIAMGIA MONEY,
@SL INT,
@MASX INT,
@MALOAI INT
AS
BEGIN
    INSERT INTO dbo.SANPHAM VALUES (@TENSP,@GIA,@CHITIET,@IMAGES,@TRANGTHAI,@GIAMGIA,@SL,@MASX,@MALOAI)
END
GO


CREATE PROCEDURE sp_ThemDonDatHang
@MASP INT,
@MAKH INT,
@MANV INT,
@SLD INT,
@THANHTIEN MONEY,
@NGAYDH DATETIME 
AS
BEGIN
    INSERT INTO dbo.DONDH VALUES(@MASP,@MAKH,@MANV,@SLD,@THANHTIEN,@NGAYDH)
END
GO


CREATE PROCEDURE sp_ThemPhieuXuat
@MAKH INT,
@MASP INT,
@MANV INT,
@MADH INT,
@SLX INT,
@THANHTIEN MONEY,
@NGAYXUAT DATETIME
AS
BEGIN
    INSERT INTO dbo.PHIEUXUAT VALUES(@MAKH, @MASP,@MANV,@MADH,@SLX,@THANHTIEN,@NGAYXUAT)
END
GO


-- Update
CREATE PROCEDURE sp_SuaNhanVien
@HOTEN NVARCHAR(250),
@NGAYSINH DATE,
@GIOITINH NCHAR(5),
@DIENTHOAI NCHAR(20),
@MAIL NVARCHAR(250),
@DIACHI NVARCHAR(250),
@NGAYLAM DATE,
@LUONG MONEY,
@USERNAMES NVARCHAR(100),
@PASSWORDS NVARCHAR(100),
@TRANGTHAI INT,
@MANV INT
AS
BEGIN
	IF(EXISTS(SELECT *FROM dbo.NHANVIEN WHERE MANV=@MANV))
	BEGIN
	    UPDATE dbo.NHANVIEN 
			SET HOTEN=@HOTEN,NGAYSINH=@NGAYSINH,GIOITINH=@GIOITINH,DIENTHOAI=@DIENTHOAI,MAIL=@MAIL,DIACHI=@DIACHI,NGAYLAM=@NGAYLAM,LUONG=@LUONG,USERNAMES=@USERNAMES,PASSWORDS=@PASSWORDS,TRANGTHAI=@TRANGTHAI
			WHERE MANV=@MANV	    
	END

END
GO

CREATE PROCEDURE sp_SuaKhachHang
@HOTEN NVARCHAR(250),
@NGAYSINH DATE,
@GIOITINH NCHAR(5),
@DIENTHOAI NCHAR(20),
@MAIL NVARCHAR(250),
@DIACHI NVARCHAR(250),
@NGAYDK DATE,
@MAKH INT
AS
BEGIN
	IF(EXISTS(SELECT *FROM dbo.KHACHHANG WHERE MAKH=@MAKH))
	BEGIN
	    UPDATE dbo.KHACHHANG 
			SET HOTEN=@HOTEN,NGAYSINH=@NGAYSINH,GIOITINH=@GIOITINH,DIENTHOAI=@DIENTHOAI,MAIL=@MAIL,DIACHI=@DIACHI,NGAYDK=@NGAYDK
			WHERE MAKH = @MAKH	    
	END

END
GO

CREATE PROCEDURE sp_SuaNhaSanXuat
@TENSX NVARCHAR(250),
@QUOCGIA NVARCHAR(250),
@MASX INT
AS
BEGIN
    IF(EXISTS(SELECT *FROM dbo.NHASANXUAT WHERE MASX = @MASX))
	BEGIN
	    UPDATE dbo.NHASANXUAT
			SET TENSX=@TENSX,QUOCGIA=@QUOCGIA
			WHERE MASX = @MASX
	END
END
GO

CREATE PROCEDURE sp_SuaLoaiSanPham
@TENLOAI NVARCHAR(100),
@DVT NVARCHAR(100),
@MALOAI INT
AS
BEGIN
    IF(EXISTS(SELECT *FROM dbo.LOAISP WHERE MALOAI=@MALOAI))
	BEGIN
	    UPDATE dbo.LOAISP 
			SET TENLOAI=@TENLOAI, DVT=@DVT
			WHERE MALOAI = @MALOAI
	END
END
GO

CREATE PROCEDURE sp_SuaSanPham
@TENSP NVARCHAR(250),
@GIA MONEY,
@CHITIET NVARCHAR(MAX),
@IMAGES NVARCHAR(250),
@TRANGTHAI INT,
@GIAMGIA MONEY,
@SL INT,
@MALOAI INT,
@MASP INT
AS
BEGIN
	IF (EXISTS(SELECT *FROM dbo.SANPHAM WHERE MASP=@MASP))
	BEGIN
		UPDATE dbo.SANPHAM
			SET TENSP=@TENSP,GIA=@GIA,CHITIET=@CHITIET,IMAGES=@IMAGES,TRANGTHAI=@TRANGTHAI,GIAMGIA=@GIAMGIA,SL=@SL,MALOAI=@MALOAI
			WHERE MASP = @MASP	    
	END

END
GO


CREATE PROCEDURE sp_SuaDonDathang
@MASP INT,
@MAKH INT,
@MANV INT,
@SLD INT,
@THANHTIEN MONEY,
@NGAYDH DATETIME,
@MADH INT
AS
BEGIN
    IF(EXISTS(SELECT *FROM dbo.DONDH WHERE MADH=@MADH))
	BEGIN
	    UPDATE dbo.DONDH 
			SET MASP=@MASP,MAKH=@MAKH,MANV=@MANV,SLD=@SLD,THANHTIEN=@THANHTIEN, NGAYDH=@NGAYDH
			WHERE MADH=@MADH
	END
END
GO

CREATE PROCEDURE sp_SuaPhieuXuat
@MASP INT,
@MAKH INT,
@MANV INT,
@MADH INT,
@SLX INT,
@THANHTIEN MONEY,
@NGAYXUAT DATETIME,
@MAPX INT
AS
BEGIN
    IF(EXISTS(SELECT *FROM dbo.PHIEUXUAT WHERE MAPX=@MAPX))
	BEGIN
	    UPDATE dbo.PHIEUXUAT 
			SET MASP=@MASP,MAKH=@MAKH,MANV=@MANV, MADH = @MADH, SLX=@SLX,THANHTIEN=@THANHTIEN,NGAYXUAT=@NGAYXUAT
			WHERE MAPX=@MAPX
	END
END
GO

INSERT INTO dbo.NHASANXUAT
(
    TENSX,
    QUOCGIA
)
VALUES
(   N'APPLE', -- TENSX - nvarchar(250)
    N'US'  -- QUOCGIA - nvarchar(250)
    )


	INSERT INTO dbo.LOAISP
	(
	    TENLOAI,
	    DVT
	)
	VALUES
	(   N'Smart Phone', -- TENLOAI - nvarchar(250)
	    N'chiếc'  -- DVT - nvarchar(100)
	    )

SELECT *FROM dbo.SANPHAM

EXEC dbo.sp_ThemPhieuXuat @MAKH = 12120001,                        -- int
                          @MASP = 17170001,                        -- int
                          @MANV = 15150001,                        -- int
                          @MADH = 18180001,                        -- int
                          @SLX = 1,                         -- int
                          @THANHTIEN = 35691000,                -- money
                          @NGAYXUAT = '2018-06-15 10:18:08' -- datetime






SELECT *FROM dbo.KHACHHANG
SELECT *FROM dbo.SANPHAM
SELECT *FROM dbo.NHANVIEN
SELECT *FROM dbo.DONDH
