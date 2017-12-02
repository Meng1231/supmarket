use FlippedSupermarket
go

--��ӹ���Ա��ɫ��Ϣ
if exists (select * from sysobjects where name ='p_Role_Insert')
drop proc p_Role_Insert
go
create proc p_Role_Insert
@RoleName varchar(50), --��ɫ����
@RolePurview nvarchar(50) --��ɫȨ��
as 
	insert into Role values(@RoleName,@RolePurview)
go

--��ӹ���Ա��Ϣ
if exists (select * from sysobjects where name ='p_AdminInfo_Insert')
drop proc p_AdminInfo_Insert
go
create proc p_AdminInfo_Insert
@RoleID int, --��ɫID
@UserName nvarchar(50), --����Ա����
@Account varchar(20), --����Ա�˺�
@PassWord varchar(50) --����Ա����
as 
	insert into AdminInfo values(@RoleID,@UserName,@Account,@PassWord)
go


--�����Ʒ�����Ϣ
if exists (select * from sysobjects where name ='p_Type_Insert')
drop proc p_Type_Insert
go
create proc p_Type_Insert
@TypeName varchar(50) --��Ʒ�������
as 
	insert into Type values(@TypeName)
go

--�����Ʒ��λ��Ϣ
if exists (select * from sysobjects where name ='p_Unit_Insert')
drop proc p_Unit_Insert
go
create proc p_Unit_Insert
@UnitName varchar(20) --��Ʒ��λ����
as 
	insert into Unit values(@UnitName)
go


--�����Ʒ������Ϣ
if exists (select * from sysobjects where name ='p_Purchases_Insert')
drop proc p_Purchases_Insert
go
create proc p_Purchases_Insert
@PurchasesTotal money  ,--�����ܼ�
@AdminID int ,--����������
@Reamrk varchar(50) -- ��ע
as 
	insert into Purchases values(@PurchasesTotal,@AdminID,@Reamrk,getdate())
go


--��ӽ���������Ϣ
if exists (select * from sysobjects where name ='p_DetailPurchases_Insert')
drop proc p_DetailPurchases_Insert
go
create proc p_DetailPurchases_Insert
@PurchasesID int  ,--����ID
@ProductID int ,--��ƷID
@ProductAmount decimal,--��������
@Reamrk varchar(50) -- ��ע
as 
	insert into DetailePurchases values(@PurchasesID,@ProductID,@ProductAmount,@Reamrk)
go


--�����Ʒ��Ϣ
if exists (select * from sysobjects where name ='p_Product_Insert')
drop proc p_Product_Insert
go
create proc p_Product_Insert
@ProductName varchar(50),--��Ʒ����
@BarCode varchar(50), --��Ʒ������
@Manufacturer varchar(100), --��������
@CommodityDepict varchar(250)=null, --��Ʒ���
@CommodityPriceOut money, --��Ʒ���ۼ�
@CommodityPriceIn money ,--��Ʒ������
@TypeID int,--��Ʒ���ID
@UnitID int--��Ʒ��λ
as 
	insert into Product values(@ProductName,@BarCode,@Manufacturer,@CommodityDepict,@CommodityPriceOut,@CommodityPriceIn,@TypeID,@UnitID,0)
go

--����˻���Ϣ
if exists (select * from sysobjects where name ='p_ReturnGoods_Insert')
drop proc p_ReturnGoods_Insert
go
create proc p_ReturnGoods_Insert
@ProductID int,--��ƷID
@ReturnAmount decimal, --�˻�����
@Remark varchar(200)=null--�˻�ԭ��
as 
	insert into ReturnGoods values(@ProductID,@ReturnAmount,@Remark,getdate())
go

--��ӿ����Ϣ
if exists (select * from sysobjects where name ='p_Stock_Insert')
drop proc p_Stock_Insert
go
create proc p_Stock_Insert
@ProductID int,--��ƷID
@CommodityStockAmount decimal,--���п������
@StockUp decimal,--�������
@StockDown decimal--�������
as 
	insert into Stock values(@ProductID,getdate(),@CommodityStockAmount,@StockUp,@StockDown,getdate())
go

--��ӻ�Ա����Ϣ
if exists (select * from sysobjects where name ='p_Card_Insert')
drop proc p_Card_Insert
go
create proc p_Card_Insert
@TotalCost money,--�ۼ����ѽ��
@Score int--����
as 
	insert into Card values(@TotalCost,@Score)
go

--��ӻ�Ա��Ϣ
if exists (select * from sysobjects where name ='p_User_Insert')
drop proc p_User_Insert
go
create proc p_User_Insert
@CardID int,--��Ա����
@CustomName varchar(50),--�˿�����
@IDNumber varchar(18) ,--���֤��
@Sex int=null,--�Ա�(0:�� 1:Ů)
@Age int=null,--����
@CustomPassWord varchar(50)=null--��������
as 
	insert into [User] values(@CardID,@CustomName,@IDNumber,@Sex,@Age,@CustomPassWord,getdate(),0)
go

--���������Ϣ
if exists (select * from sysobjects where name ='p_Orders_Insert')
drop proc p_Orders_Insert
go
create proc p_Orders_Insert
@CustomsID int,--�˿ͱ��
@SunPrice money,--��Ʒ�ܼ�
@Way varchar(20),--֧����ʽ
@AdminID int  --������Ա
as 
	insert into Orders values(@CustomsID,@SunPrice,getdate(),@Way,@AdminID)
go

--��Ӷ�������
if exists (select * from sysobjects where name ='p_DetailOrder_Insert')
drop proc p_DetailOrder_Insert
go
create proc p_DetailOrder_Insert
@ProductID int,--��Ʒ���
@OrderFormID int,--�������
@Amount decimal,--��Ʒ����
@subtotal money--��ƷС��
as 
	insert into DetailOrder values(@ProductID,@OrderFormID,@Amount,@subtotal)
go

if(exists(select * from sys.objects where name='proc_LogDic_Insert')) -- �����־�ֵ�
drop  proc proc_LogDic_Insert	
go
create proc proc_LogDic_Insert(
@TypeName varchar(20)
)
as
	insert into LogDic values(@TypeName)
go

if(exists(select * from sys.objects where name='proc_SysLog_AddSysLog')) --���ϵͳ��־
drop  proc proc_SysLog_AddSysLog	
go
create proc proc_SysLog_AddSysLog
@Behavior varchar(500) ,
@FK_TypeID int,
@FK_UserID int,
@Parameters varchar(max),
@IP varchar(20),
@CheckInTime datetime ,
@Exception varchar(max)='',
@IsException tinyint=0
as
		insert into SysLog values(
				@Behavior,
				@FK_TypeID,
				@FK_UserID,
				@Parameters,
				@IP,
				@CheckInTime,
				@Exception,
				@IsException
		)
go
