use master
go

--�жϸ����ݿ��Ƿ���ڣ�������ɾ��
if exists (select * from sysdatabases where name ='FlippedSupermarket')
drop database  FlippedSupermarket
go

--���������ݿ�
create database  FlippedSupermarket
go
--ʹ�ø����ݿ�
use  FlippedSupermarket
go

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Role' )
drop table Role 
go
--������ɫ��
create table Role 
(
   RoleID int primary key identity(1,1) not null, --��ɫ���
   RoleName nvarchar(50) null,  --��ɫ����
   RolePurview nvarchar(50) null  --��ɫȨ��
)
go
--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='AdminInfo' )
drop table AdminInfo 
go
--��������Ա��Ϣ��
create table AdminInfo 
(
AdminID int  primary key identity(1,1),--����Ա�˺�ID
RoleID int references Role(RoleID) not null,--��ɫȨ�ޱ��
UserName varchar(50) not null,--����Ա�û���
Account varchar(20) unique not null, --����Ա�˺�
PassWord varchar(50) not null --����Ա����
)
go

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Type' )
drop table Type 
go
--������Ʒ����
create table Type 
(
TypeID int primary key identity(1,1),--�����ID
TypeName varchar(50) not null --�������
)
go

--������Ʒ��λ��
create table Unit
(
	UnitID int identity(1,1) primary key,--ID
	UnitName varchar(20) not null --��λ����
)

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Purchases' )
drop table Purchases  
go

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Product' )
drop table Product 
go
--������Ʒ��
create table Product 
(
ProductID int primary key identity(1,1), --��Ʒ���ID
ProductName varchar(50) not null, --��Ʒ����
BarCode varchar(50) unique not null,--������(Ψһ)
Manufacturer varchar(100) not null,--��������
CommodityDepict varchar(250) null,--��Ʒ����
CommodityPriceOut money not null,--��Ʒ���ۼ�
CommodityPriceIn money not null ,--��Ʒ������
TypeID int references Type(TypeID) not null,--���ID
UnitID int references Unit(UnitID),--��λ
IsDelete int default(0) --�ϼ��¼�
)
go

--����������
create table Purchases 
(
PurchasesID int Primary key identity(1,1) ,--�������
PurchasesTotal money not null ,--�����ܼ�
AdminID int references AdminInfo(AdminID) not null,--����������
Reamrk varchar(50) null, -- ��ע
PurchasesTime datetime not null  --����ʱ��
)
go

--�������������
create table DetailePurchases 
(
DetailePurchasesID int Primary key identity(1,1) ,--�������
PurchasesID  int references Purchases(PurchasesID) not null,--����ID
ProductID int references Product(ProductID) not null,--��ƷID
ProductAmount decimal not null,--��������
Reamrk varchar(50) null -- ��ע
)
go

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='ReturnGoods' )
drop table ReturnGoods  
go
--�����˻���
create table ReturnGoods 
(
ReturnGoodsID int Primary key identity(1,1) ,--�˻����
ProductID int references Product(ProductID) not null,--��Ʒ���ID
ReturnAmount decimal not null ,--�˻�����
Remark varchar(200) null ,--�˻�ԭ��
CheckInTime datetime default(getdate()) not  null --�˻�ʱ��
)
go

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Stock' )
drop table Stock 
go
--��������
create table Stock 
(
StockID int primary key identity(1,1),--�����ID
ProductID int references Product(ProductID) not null ,--��Ʒ���
StorageTime  datetime not null, --���ʱ��
CommodityStockAmount decimal not null,--���п������
StockUp decimal not null,--�������
StockDown decimal not null,--�������
AlterTime datetime default(getdate()) not null --�޸�ʱ��
)
go


--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Card' )
drop table Card 
go
--������Ա����
create table Card 
(
CardID int primary key identity(10000,1), --��Ա����
TotalCost money not null,--�ۼ����ѽ��
Score int not null --����
)
go

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Customs' )
drop table Customs 
go
--�����˿ͱ�
create table [User] 
(
UserID int primary key identity(1,1), --�˿ͱ��
CardID int references Card(CardID) not null,--��Ա����
UserName varchar(50) not null ,--�˿�����
IDNumber varchar(18) not null ,--���֤��
Sex int null,--�Ա�(0:�� 1:Ů)
Age int null,--����
UserPassWord varchar(50)  null ,--��������
AlterInTime datetime default(getdate()), -- �޸�ʱ��
IsDelete int not null, --�Ƿ�����ʹ��(0:�� 1:��)
)
--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='Orders' )
drop table Orders 
go
--�������۱�
create table Orders 
(
OrderFormID int primary key identity(1,1), --���۱��
CustomsID int references [User](UserID) not null,--�˿ͱ��
SunPrice money not null ,--��Ʒ�ܼ�
CheckInTime datetime default(getdate()), --����ʱ��
Way varchar(20) not null,--֧����ʽ
AdminID int references AdminInfo(AdminID) --������Ա
)
go

--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='DetailOrder' )
drop table DetailOrder
go
--�������������
create table DetailOrder 
(
DetailOrderID int primary key identity(1,1), --������
ProductID int references Product(ProductID) not null,--��ƷID
OrderFormID int references Orders(OrderFormID) not null, --���۱��
Amount decimal not null,--��Ʒ����
subtotal money not null--��ƷС��
)
go


--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='LogDic' )
drop table LogDic 
go
--������־�ֵ��
create table LogDic    
(
TypeID int primary key identity(1,1) not null,  --����ID  
TypeName varchar(20) null  --������
)
go


--�жϸñ��Ƿ���ڣ�������ɾ��
if exists(select * from sysobjects where name ='SysLog' )
drop table SysLog 
go
--������־�ֵ��
 create table SysLog  --ϵͳ��־��
 (
 LogID int primary key identity(1,1) not null,  --��־��� 
 Behavior varchar(500) null,  --������Ϊ
 FK_TypeID int references LogDic(TypeID) null,   --��Ϊ����
 FK_AdminID int references AdminInfo(AdminID) null,   --����ԱID
 Parameters varchar(max) null,  --����
 IP varchar(20) null,            --��¼IP
 CheckInTime datetime null,      --д��ʱ��
 Exception varchar(max) null,    --�쳣��Ϣ����  
 IsException tinyint null        --0������  1.�쳣
 )
 go