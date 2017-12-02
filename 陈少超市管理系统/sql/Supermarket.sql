use master
go

--判断该数据库是否存在，存在则删除
if exists (select * from sysdatabases where name ='FlippedSupermarket')
drop database  FlippedSupermarket
go

--创建该数据库
create database  FlippedSupermarket
go
--使用该数据库
use  FlippedSupermarket
go

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Role' )
drop table Role 
go
--创建角色表
create table Role 
(
   RoleID int primary key identity(1,1) not null, --角色编号
   RoleName nvarchar(50) null,  --角色名称
   RolePurview nvarchar(50) null  --角色权限
)
go
--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='AdminInfo' )
drop table AdminInfo 
go
--创建管理员信息表
create table AdminInfo 
(
AdminID int  primary key identity(1,1),--管理员账号ID
RoleID int references Role(RoleID) not null,--角色权限编号
UserName varchar(50) not null,--管理员用户名
Account varchar(20) unique not null, --管理员账号
PassWord varchar(50) not null --管理员密码
)
go

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Type' )
drop table Type 
go
--创建商品类别表
create table Type 
(
TypeID int primary key identity(1,1),--类别编号ID
TypeName varchar(50) not null --类别名称
)
go

--创建商品单位表
create table Unit
(
	UnitID int identity(1,1) primary key,--ID
	UnitName varchar(20) not null --单位名称
)

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Purchases' )
drop table Purchases  
go

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Product' )
drop table Product 
go
--创建商品表
create table Product 
(
ProductID int primary key identity(1,1), --商品编号ID
ProductName varchar(50) not null, --商品名称
BarCode varchar(50) unique not null,--条形码(唯一)
Manufacturer varchar(100) not null,--生产厂家
CommodityDepict varchar(250) null,--商品描述
CommodityPriceOut money not null,--商品出售价
CommodityPriceIn money not null ,--商品进货价
TypeID int references Type(TypeID) not null,--类别ID
UnitID int references Unit(UnitID),--单位
IsDelete int default(0) --上架下架
)
go

--创建进货表
create table Purchases 
(
PurchasesID int Primary key identity(1,1) ,--进货编号
PurchasesTotal money not null ,--进货总价
AdminID int references AdminInfo(AdminID) not null,--进货经手人
Reamrk varchar(50) null, -- 备注
PurchasesTime datetime not null  --进货时间
)
go

--创建进货详情表
create table DetailePurchases 
(
DetailePurchasesID int Primary key identity(1,1) ,--进货编号
PurchasesID  int references Purchases(PurchasesID) not null,--进货ID
ProductID int references Product(ProductID) not null,--商品ID
ProductAmount decimal not null,--进货数量
Reamrk varchar(50) null -- 备注
)
go

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='ReturnGoods' )
drop table ReturnGoods  
go
--创建退货表
create table ReturnGoods 
(
ReturnGoodsID int Primary key identity(1,1) ,--退货编号
ProductID int references Product(ProductID) not null,--商品编号ID
ReturnAmount decimal not null ,--退货数量
Remark varchar(200) null ,--退货原因
CheckInTime datetime default(getdate()) not  null --退货时间
)
go

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Stock' )
drop table Stock 
go
--创建库存表
create table Stock 
(
StockID int primary key identity(1,1),--库存编号ID
ProductID int references Product(ProductID) not null ,--商品编号
StorageTime  datetime not null, --入库时间
CommodityStockAmount decimal not null,--现有库存数量
StockUp decimal not null,--库存上限
StockDown decimal not null,--库存下限
AlterTime datetime default(getdate()) not null --修改时间
)
go


--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Card' )
drop table Card 
go
--创建会员卡表
create table Card 
(
CardID int primary key identity(10000,1), --会员卡号
TotalCost money not null,--累计消费金额
Score int not null --积分
)
go

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Customs' )
drop table Customs 
go
--创建顾客表
create table [User] 
(
UserID int primary key identity(1,1), --顾客编号
CardID int references Card(CardID) not null,--会员卡号
UserName varchar(50) not null ,--顾客姓名
IDNumber varchar(18) not null ,--身份证号
Sex int null,--性别(0:男 1:女)
Age int null,--年龄
UserPassWord varchar(50)  null ,--消费密码
AlterInTime datetime default(getdate()), -- 修改时间
IsDelete int not null, --是否正在使用(0:是 1:否)
)
--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='Orders' )
drop table Orders 
go
--创建销售表
create table Orders 
(
OrderFormID int primary key identity(1,1), --销售编号
CustomsID int references [User](UserID) not null,--顾客编号
SunPrice money not null ,--商品总价
CheckInTime datetime default(getdate()), --销售时间
Way varchar(20) not null,--支付方式
AdminID int references AdminInfo(AdminID) --销售人员
)
go

--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='DetailOrder' )
drop table DetailOrder
go
--创建订单详情表
create table DetailOrder 
(
DetailOrderID int primary key identity(1,1), --详情编号
ProductID int references Product(ProductID) not null,--商品ID
OrderFormID int references Orders(OrderFormID) not null, --销售编号
Amount decimal not null,--商品数量
subtotal money not null--商品小计
)
go


--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='LogDic' )
drop table LogDic 
go
--创建日志字典表
create table LogDic    
(
TypeID int primary key identity(1,1) not null,  --类型ID  
TypeName varchar(20) null  --类型名
)
go


--判断该表是否存在，存在则删除
if exists(select * from sysobjects where name ='SysLog' )
drop table SysLog 
go
--创建日志字典表
 create table SysLog  --系统日志表
 (
 LogID int primary key identity(1,1) not null,  --日志编号 
 Behavior varchar(500) null,  --操作行为
 FK_TypeID int references LogDic(TypeID) null,   --行为类型
 FK_AdminID int references AdminInfo(AdminID) null,   --管理员ID
 Parameters varchar(max) null,  --参数
 IP varchar(20) null,            --登录IP
 CheckInTime datetime null,      --写入时间
 Exception varchar(max) null,    --异常信息详情  
 IsException tinyint null        --0：正常  1.异常
 )
 go