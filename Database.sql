use master
GO
IF EXISTS (SELECT NAME FROM MASTER.SYS.DATABASES WHERE NAME ='QUANLYGIAYDEP' )
DROP DATABASE QUANLYGIAYDEP
GO
CREATE DATABASE QUANLYGIAYDEP
GO
USE QUANLYGIAYDEP
GO

--T?O B?NG QU?N TR? H? TH?NG
CREATE TABLE QuanTriHeThong 
(
MaQT nvarchar(10) NOT NULL,
HoTen nvarchar(50), 
NgaySinh datetime,
SoDienThoai nvarchar (20)
);

--T?O B?NG HOA DON
CREATE TABLE HoaDon
(
MaHD nvarchar(10) NOT NULL,
MaQT nvarchar(10), 
NgayLap datetime
);

--T?O B?NG S?N PH?M
CREATE TABLE SanPham
(
MaSP nvarchar(10) not null,
MaQT nvarchar(10),
TenSP nvarchar(50), 
MaCL nvarchar(10), 
MaXX nvarchar(10),
Gia money
);



--T?O B?NG HD_SP
CREATE TABLE HD_SP
(
MaHD nvarchar(10) NOT NULL,
MaSP nvarchar(10) NOT NULL,
SoLuong int,
Gia money
);
--T?O B?NG Xuat Xu
CREATE TABLE XuatXu
(
MaXX nvarchar(10) NOT NULL,
MoTa nvarchar(50) NOT NULL
);

--T?O B?NG ChungLoai
CREATE TABLE ChungLoai
(
MaCL nvarchar(10) NOT NULL,
MoTa nvarchar(50)
);

-- T?o Khóa Chính Cho B?ng Qu?n Tr?
ALTER TABLE QuanTriHeThong 
ADD CONSTRAINT PK_MaQT Primary key(MaQT)
GO

-- T?o Khóa Chính Cho B?ng ??n Hàng
ALTER TABLE HoaDon 
ADD CONSTRAINT PK_MaHD Primary key(MaHD)
GO

-- T?o Khóa Chính Cho B?ng S?n Ph?m
ALTER TABLE SanPham 
ADD CONSTRAINT PK_MaSP Primary key(MaSP)
GO

-- T?o Khóa Chính Cho B?ng HD_SP
ALTER TABLE HD_SP 
ADD CONSTRAINT PK_HD_SP Primary key(MaHD,MaSP)
GO

-- T?o Khóa Chính Cho B?ng Xuat Xu
ALTER TABLE XuatXu 
ADD CONSTRAINT PK_MaXX Primary key(MaXX)
GO

-- T?o Khóa Chính Cho B?ng Chung Loai
ALTER TABLE ChungLoai 
ADD CONSTRAINT PK_MaCL Primary key(MaCL)
GO

-- bang Don Hang  -> Quan tri he thong
ALTER TABLE HoaDon
ADD CONSTRAINT FK_MaQT_HoaDon FOREIGN KEY (MaQT) REFERENCES QuanTriHeThong (MaQT)
ON DELETE CASCADE   
 ON UPDATE CASCADE
GO

-- San Pham -> quan tri he thong
ALTER TABLE SanPham
ADD CONSTRAINT FK_MaQT_SanPham FOREIGN KEY (MaQT) REFERENCES QuanTriHeThong (MaQT)
ON DELETE CASCADE   
 ON UPDATE CASCADE
GO

-- HD_SP -> San pham
ALTER TABLE HD_SP
ADD CONSTRAINT FK_MaSP_HD_SP FOREIGN KEY (MaSP) REFERENCES SanPham (MaSP)
ON DELETE CASCADE   
 ON UPDATE CASCADE
GO

-- HD_SP -> Hoa Don
ALTER TABLE HD_SP
ADD CONSTRAINT FK_MaHD_HD_SP FOREIGN KEY (MaHD) REFERENCES HoaDon (MaHD)
--ON DELETE CASCADE   
--ON UPDATE CASCADE
GO

--- SP -> ChungLoai
ALTER TABLE SanPham
ADD CONSTRAINT FK_MaCL_SanPham FOREIGN KEY (MaCL) REFERENCES ChungLoai (MaCL)
ON DELETE CASCADE   
 ON UPDATE CASCADE
GO

--- SP -> XuatXu
ALTER TABLE SanPham
ADD CONSTRAINT FK_MaXX_SanPham FOREIGN KEY (MaXX) REFERENCES XuatXu (MaXX)
ON DELETE CASCADE   
 ON UPDATE CASCADE
GO
--=========== nhap du lieu =======================
insert into XuatXu (MaXX,MoTa) Values
	('VN','Viet Nam'),
	('CN','Trung Quoc')
insert into ChungLoai(MaCL,MoTa) Values
	('Gi','Giay'),
	('De','Dep')
insert into QuanTriHeThong(MaQT,HoTen,NgaySinh,SoDienThoai) Values
	('QT','Tran Quan Tri','4/4/1984','1937456276'),
	('HT','Pham He Thong','5/5/1987','2883653794')
insert into SanPham(MaSP,MaQT,TenSP,MaCL,MaXX,Gia) Values
	('SP1','QT','Aqua Sportswear','Gi','CN',259000),
	('SP2','QT','Thuong Dinh Aria','Gi','VN',259000),
	('SP3','QT','Convert CN','Gi','CN',259000),
	('SP4','HT','To Ong','De','VN',259000),
	('SP5','HT','To Ong Nghin Lo','De','VN',259000),
	('SP6','QT','Nike Fake','Gi','CN',259000)
insert into HoaDon(MaHD,MaQT,NgayLap) Values
	('HD1','QT','3/3/2013'),
	('HD2','QT','4/4/2014')
insert into HD_SP(MaHD,MaSP,SoLuong,Gia) Values
	('HD1','SP1',10,259000),
	('HD1','SP2',10,259000)

--=========== Tạo View =======================
-- View hóa đơn
GO
create view vHoaDon as
	select MaHD as [Mã Hóa Đơn], HoTen as[Họ tên quản trị], NgayLap as [Ngày Lập], HoaDon.MaQT as [Mã Quản Trị]
		from HoaDon
		inner join QuanTriHeThong on HoaDon.MaQT = QuanTriHeThong.MaQT
-- View San pham
GO
create view vSanPham as
	select 
	MaSP as [Mã Sản Phẩm],
	TenSP as [Tên Sản Phẩm],
	Gia as [Giá],
	HoTen as [Tên Quản Trị],
	ChungLoai.MoTa as [Chủng Loại],
	XuatXu.MoTa as [Xuất Xứ],
	SanPham.MaQT as [Mã Quản Trị],
	SanPham.MaCL as [Mã Chủng Loại],
	SanPham.MaXX as [Mã Xuất Xứ]
	from SanPham
	inner join QuanTriHeThong on SanPham.MaQT = QuanTriHeThong.MaQT
	inner join ChungLoai on SanPham.MaCL = ChungLoai.MaCL
	inner join XuatXu on SanPham.MaXX = XuatXu.MaXX
-- View Hoa Don San Pham
go
create view vHoaDonSanPham as
	select
	MaHD as [Mã Hóa Đơn],
	HD_SP.MaSP as [Mã Sản Phẩm],
	TenSP as [Tên Sản Phẩm],
	SoLuong as [Số Lượng],
	HD_SP.Gia as [Giá],
	[Tổng Tiền] = SoLuong * HD_SP.Gia
	from HD_SP
	inner join SanPham on HD_SP.MaSP = SanPham.MaSP
-- view báo cáo hóa đơn
go 
create view vBaoCaoHoaDon as
select 
	HD_SP.MaHD as [Mã Hóa Đơn],
	TenSP as [Tên Sản Phẩm],
	SoLuong as [Số Lượng],
	ChungLoai.MoTa as [Chủng Loại],
	XuatXu.MoTa as [Xuất Xứ],
	NgayLap as [Ngày Lập],
	HoTen as [Tên Quản Trị],
	HD_SP.Gia [Giá],
	[Thành Tiền] = SoLuong * HD_SP.Gia,
	HD_SP.MaSP,
	HoaDon.MaQT,
	SanPham.MaCL,
	SanPham.MaXX
from HD_SP
	inner join SanPham on HD_SP.MaSP = SanPham.MaSP
	inner join ChungLoai on SanPham.MaCL = ChungLoai.MaCL
	inner join XuatXu on SanPham.MaXX = XuatXu.MaXX
	inner join HoaDon on HD_SP.MaHD = HoaDon.MaHD
	inner join QuanTriHeThong on HoaDon.MaQT = QuanTriHeThong.MaQT

-- view báo cáo san pham
go 
create view vBaoCaoSanPham as
select 
	MaSP,
	TenSP as [Tên Sản Phẩm],
	ChungLoai.MoTa as [Chủng Loại],
	XuatXu.MoTa as [Xuất Xứ],
	Gia as [Giá],
	HoTen as [Tên Quản Trị],
	SanPham.MaQT,
	SanPham.MaCL,
	SanPham.MaXX
from SanPham
	inner join ChungLoai on SanPham.MaCL = ChungLoai.MaCL
	inner join XuatXu on SanPham.MaXX = XuatXu.MaXX
	inner join QuanTriHeThong on SanPham.MaQT = QuanTriHeThong.MaQT
--update HoaDon set MaHD = '',MaQT = '',NgayLap = '' where MaHD = ''