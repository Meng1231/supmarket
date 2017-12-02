use FlippedSupermarket
go

--添加管理员角色信息
if exists (select * from sysobjects where name ='p_Role_Insert')
drop proc p_Role_Insert
go
create proc p_Role_Insert
@RoleName varchar(50), --角色名称
@RolePurview nvarchar(50) --角色权限
as 
	insert into Role values(@RoleName,@RolePurview)
go

--添加管理员信息
if exists (select * from sysobjects where name ='p_AdminInfo_Insert')
drop proc p_AdminInfo_Insert
go
create proc p_AdminInfo_Insert
@RoleID int, --角色ID
@UserName nvarchar(50), --管理员名称
@Account varchar(20), --管理员账号
@PassWord varchar(50) --管理员密码
as 
	insert into AdminInfo values(@RoleID,@UserName,@Account,@PassWord)
go


--添加商品类别信息
if exists (select * from sysobjects where name ='p_Type_Insert')
drop proc p_Type_Insert
go
create proc p_Type_Insert
@TypeName varchar(50) --商品类别名称
as 
	insert into Type values(@TypeName)
go

--添加商品单位信息
if exists (select * from sysobjects where name ='p_Unit_Insert')
drop proc p_Unit_Insert
go
create proc p_Unit_Insert
@UnitName varchar(20) --商品单位名称
as 
	insert into Unit values(@UnitName)
go


--添加商品进货信息
if exists (select * from sysobjects where name ='p_Purchases_Insert')
drop proc p_Purchases_Insert
go
create proc p_Purchases_Insert
@PurchasesTotal money  ,--进货总价
@AdminID int ,--进货经手人
@Reamrk varchar(50) -- 备注
as 
	insert into Purchases values(@PurchasesTotal,@AdminID,@Reamrk,getdate())
go


--添加进货详情信息
if exists (select * from sysobjects where name ='p_DetailPurchases_Insert')
drop proc p_DetailPurchases_Insert
go
create proc p_DetailPurchases_Insert
@PurchasesID int  ,--进货ID
@ProductID int ,--商品ID
@ProductAmount decimal,--进货数量
@Reamrk varchar(50) -- 备注
as 
	insert into DetailePurchases values(@PurchasesID,@ProductID,@ProductAmount,@Reamrk)
go


--添加商品信息
if exists (select * from sysobjects where name ='p_Product_Insert')
drop proc p_Product_Insert
go
create proc p_Product_Insert
@ProductName varchar(50),--商品名称
@BarCode varchar(50), --商品条形码
@Manufacturer varchar(100), --生产厂家
@CommodityDepict varchar(250)=null, --商品简介
@CommodityPriceOut money, --商品出售价
@CommodityPriceIn money ,--商品进货价
@TypeID int,--商品类别ID
@UnitID int--商品单位
as 
	insert into Product values(@ProductName,@BarCode,@Manufacturer,@CommodityDepict,@CommodityPriceOut,@CommodityPriceIn,@TypeID,@UnitID,0)
go

--添加退货信息
if exists (select * from sysobjects where name ='p_ReturnGoods_Insert')
drop proc p_ReturnGoods_Insert
go
create proc p_ReturnGoods_Insert
@ProductID int,--商品ID
@ReturnAmount decimal, --退货数量
@Remark varchar(200)=null--退货原因
as 
	insert into ReturnGoods values(@ProductID,@ReturnAmount,@Remark,getdate())
go

--添加库存信息
if exists (select * from sysobjects where name ='p_Stock_Insert')
drop proc p_Stock_Insert
go
create proc p_Stock_Insert
@ProductID int,--商品ID
@CommodityStockAmount decimal,--现有库存数量
@StockUp decimal,--库存上限
@StockDown decimal--库存下限
as 
	insert into Stock values(@ProductID,getdate(),@CommodityStockAmount,@StockUp,@StockDown,getdate())
go

--添加会员卡信息
if exists (select * from sysobjects where name ='p_Card_Insert')
drop proc p_Card_Insert
go
create proc p_Card_Insert
@TotalCost money,--累计消费金额
@Score int--积分
as 
	insert into Card values(@TotalCost,@Score)
go

--添加会员信息
if exists (select * from sysobjects where name ='p_User_Insert')
drop proc p_User_Insert
go
create proc p_User_Insert
@CardID int,--会员卡号
@CustomName varchar(50),--顾客姓名
@IDNumber varchar(18) ,--身份证号
@Sex int=null,--性别(0:男 1:女)
@Age int=null,--年龄
@CustomPassWord varchar(50)=null--消费密码
as 
	insert into [User] values(@CardID,@CustomName,@IDNumber,@Sex,@Age,@CustomPassWord,getdate(),0)
go

--添加销售信息
if exists (select * from sysobjects where name ='p_Orders_Insert')
drop proc p_Orders_Insert
go
create proc p_Orders_Insert
@CustomsID int,--顾客编号
@SunPrice money,--商品总价
@Way varchar(20),--支付方式
@AdminID int  --销售人员
as 
	insert into Orders values(@CustomsID,@SunPrice,getdate(),@Way,@AdminID)
go

--添加订单详情
if exists (select * from sysobjects where name ='p_DetailOrder_Insert')
drop proc p_DetailOrder_Insert
go
create proc p_DetailOrder_Insert
@ProductID int,--商品编号
@OrderFormID int,--订单编号
@Amount decimal,--商品数量
@subtotal money--商品小计
as 
	insert into DetailOrder values(@ProductID,@OrderFormID,@Amount,@subtotal)
go

if(exists(select * from sys.objects where name='proc_LogDic_Insert')) -- 添加日志字典
drop  proc proc_LogDic_Insert	
go
create proc proc_LogDic_Insert(
@TypeName varchar(20)
)
as
	insert into LogDic values(@TypeName)
go

if(exists(select * from sys.objects where name='proc_SysLog_AddSysLog')) --添加系统日志
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
