use FlippedSupermarket
go

--向管理员角色表插入测试数据
Exec p_Role_Insert '超级管理员',null
Exec p_Role_Insert '销售人员',null
Exec p_Role_Insert '管理员',null
Exec p_Role_Insert '仓库管理员',null
Exec p_Role_Insert '技术人员',null
go

--向管理员表插入测试数据
Exec p_AdminInfo_Insert 1,'顾殳安','admin','admin'
Exec p_AdminInfo_Insert 2,'In柠檬','123456','123456'
Exec p_AdminInfo_Insert 3,'安好','133496','133496'
Exec p_AdminInfo_Insert 4,'Hickey','603201','603201'
Exec p_AdminInfo_Insert 1,'顾殳成','Hickey','admin'
Exec p_AdminInfo_Insert 2,'冰凝','ning258','123456'
Exec p_AdminInfo_Insert 3,'菲娜','987654','133496'
Exec p_AdminInfo_Insert 4,'张子安','an2486','603201'
Exec p_AdminInfo_Insert 1,'星海','100258','admin'
Exec p_AdminInfo_Insert 2,'茶老','ta0487','123456'
Exec p_AdminInfo_Insert 3,'孙晓梦','579846','133496'
Exec p_AdminInfo_Insert 4,'Rush','Rush01','603201'
go

--向商品类别表插入测试数据
Exec p_Type_Insert  '电器'
Exec p_Type_Insert	'礼品'
Exec p_Type_Insert	'日用'
Exec p_Type_Insert	'蔬果'
Exec p_Type_Insert	'食品'
Exec p_Type_Insert	'生鲜'
go


--商品单位表插入测试数据
Exec p_Unit_Insert '个'
Exec p_Unit_Insert 'kg'
Exec p_Unit_Insert '张'
Exec p_Unit_Insert '套'
Exec p_Unit_Insert '双'
Exec p_Unit_Insert '瓶'
Exec p_Unit_Insert '箱'
Exec p_Unit_Insert '盒'
Exec p_Unit_Insert '张'
Exec p_Unit_Insert '吨'
Exec p_Unit_Insert '只'
Exec p_Unit_Insert '件'
go


--向商品进货表插入测试数据
Exec p_Product_Insert '大又甜无籽西瓜','281351489','大又甜','精心培育的大又甜西瓜，夏季消暑的必备产品，触动你的心跳，找回活力无限的你。',6.25,5.48,4,2
Exec p_Product_Insert '阿根廷红虾野生船冻 日料海虾 ','425814569','阿根廷红虾','海底捕捞后，船上急速冻，锁住新鲜，肉质鲜嫩Q弹，可做刺身。',25.80,22.50,6,2
Exec p_Product_Insert '佳沛新西兰绿奇异果','584967486','佳沛','超乎想象的丰沛多汁，多层次的清新口感，金典纯甜，甜度高达16，果香浓郁，，每一口都能感受到甜蜜清香。',14.50,14.00,4,7
Exec p_Product_Insert '乔治卡罗尔男士洗护套装','584962459','乔治卡罗尔','男士专属洗护套装，给你不一样的体验，心动不如行动。',159.00,108.00,3,4
Exec p_Product_Insert '大又甜无籽西瓜','281351487','大又甜','精心培育的大又甜西瓜，夏季消暑的必备产品，触动你的心跳，找回活力无限的你。',6.25,6.02,4,2
Exec p_Product_Insert '阿根廷红虾野生船冻 日料海虾 ','425815563','阿根廷红虾','海底捕捞后，船上急速冻，锁住新鲜，肉质鲜嫩Q弹，可做刺身。',25.80,22.4,6,2
Exec p_Product_Insert '佳沛新西兰绿奇异果','584962487','佳沛','超乎想象的丰沛多汁，多层次的清新口感，金典纯甜，甜度高达16，果香浓郁，，每一口都能感受到甜蜜清香。',14.50,12.4,4,7
Exec p_Product_Insert '乔治卡罗尔男士洗护套装','584962460','乔治卡罗尔','男士专属洗护套装，给你不一样的体验，心动不如行动。',159.00,134.00,3,4

Exec p_Product_Insert '大又甜无籽西瓜','281301489','大又甜','精心培育的大又甜西瓜，夏季消暑的必备产品，触动你的心跳，找回活力无限的你。',6.25,5.48,4,2
Exec p_Product_Insert '阿根廷红虾野生船冻 日料海虾 ','420814569','阿根廷红虾','海底捕捞后，船上急速冻，锁住新鲜，肉质鲜嫩Q弹，可做刺身。',25.80,22.50,6,2
Exec p_Product_Insert '佳沛新西兰绿奇异果','580967486','佳沛','超乎想象的丰沛多汁，多层次的清新口感，金典纯甜，甜度高达16，果香浓郁，，每一口都能感受到甜蜜清香。',14.50,14.00,4,7
Exec p_Product_Insert '乔治卡罗尔男士洗护套装','584062459','乔治卡罗尔','男士专属洗护套装，给你不一样的体验，心动不如行动。',159.00,108.00,3,4
Exec p_Product_Insert '大又甜无籽西瓜','281351587','大又甜','精心培育的大又甜西瓜，夏季消暑的必备产品，触动你的心跳，找回活力无限的你。',6.25,6.02,4,2
Exec p_Product_Insert '阿根廷红虾野生船冻 日料海虾 ','425805563','阿根廷红虾','海底捕捞后，船上急速冻，锁住新鲜，肉质鲜嫩Q弹，可做刺身。',25.80,22.4,6,2
Exec p_Product_Insert '佳沛新西兰绿奇异果','584952487','佳沛','超乎想象的丰沛多汁，多层次的清新口感，金典纯甜，甜度高达16，果香浓郁，，每一口都能感受到甜蜜清香。',14.50,14.00,4,7
Exec p_Product_Insert '乔治卡罗尔男士洗护套装','586962460','乔治卡罗尔','男士专属洗护套装，给你不一样的体验，心动不如行动。',159.00,134.00,3,4
go

--向商品进货表插入测试数据
Exec p_Purchases_Insert 6250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 11250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 5880,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 10800,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 6250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 11250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 5880,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 10800,4,"时光匆匆独白，将颠沛磨成卡带"

Exec p_Purchases_Insert 6250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 11250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 5880,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 10800,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 6250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 11250,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 5880,4,"时光匆匆独白，将颠沛磨成卡带"
Exec p_Purchases_Insert 10800,4,"时光匆匆独白，将颠沛磨成卡带"
go


--向进货详情表插入测试数据
Exec p_DetailPurchases_Insert 1,1,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 1,3,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 2,3,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 2,2,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 3,5,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 3,2,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 4,7,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 4,4,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 5,2,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 5,9,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 6,8,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 6,5,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 7,5,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 7,8,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 8,9,158,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 8,3,99,'这是一批精良的货物'


Exec p_DetailPurchases_Insert 9,1,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 9,3,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 10,3,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 10,2,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 11,5,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 11,2,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 12,7,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 12,4,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 13,2,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 13,9,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 14,8,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 14,5,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 15,5,108,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 15,8,99,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 16,9,158,'这是一批精良的货物'
Exec p_DetailPurchases_Insert 16,3,99,'这是一批精良的货物'


--向商品退货表插入测试数据
Exec p_ReturnGoods_Insert 1,12,'运输造成的损坏'
Exec p_ReturnGoods_Insert 2,45.6,'质量不过关'
Exec p_ReturnGoods_Insert 3,14.56,'运输造成的损坏'
Exec p_ReturnGoods_Insert 4,2,'销量不好'

Exec p_ReturnGoods_Insert 5,12,'运输造成的损坏'
Exec p_ReturnGoods_Insert 6,45.6,'质量不过关'
Exec p_ReturnGoods_Insert 7,14.56,'运输造成的损坏'
Exec p_ReturnGoods_Insert 8,2,'销量不好'

Exec p_ReturnGoods_Insert 9,12,'运输造成的损坏'
Exec p_ReturnGoods_Insert 10,45.6,'质量不过关'
Exec p_ReturnGoods_Insert 11,14.56,'运输造成的损坏'
Exec p_ReturnGoods_Insert 12,2,'销量不好'
go

--想库存表中插入测试数据
Exec p_Stock_Insert 1,1583,2480,500
Exec p_Stock_Insert 2,1058,2480,500
Exec p_Stock_Insert 3,1243,2480,500
Exec p_Stock_Insert 4,248,2480,500

Exec p_Stock_Insert 5,1583,2480,500
Exec p_Stock_Insert 6,1058,2480,500
Exec p_Stock_Insert 7,1243,2480,500
Exec p_Stock_Insert 8,248,2480,500
go

--向会员卡表中插入测试数据
Exec p_Card_Insert 1258.00,125
Exec p_Card_Insert 2580.00,258
Exec p_Card_Insert 485.00,48
Exec p_Card_Insert 682.00,68
Exec p_Card_Insert 0,0

Exec p_Card_Insert 1258.00,125
Exec p_Card_Insert 2580.00,258
Exec p_Card_Insert 485.00,48
Exec p_Card_Insert 682.00,68
Exec p_Card_Insert 0,0

Exec p_Card_Insert 1258.00,125
Exec p_Card_Insert 2580.00,258
Exec p_Card_Insert 485.00,48
Exec p_Card_Insert 682.00,68
Exec p_Card_Insert 0,0
go

--向会员表中插入测试数据
Exec p_User_Insert 10000,韩航,412827199510213608,0,36,'123456'
Exec p_User_Insert 10001,莫云,412827199510216059,1,23,null
Exec p_User_Insert 10002,白梦中,412827199510212548,1,22,'123456'
Exec p_User_Insert 10003,尚云,412827199510211485,0,18,null
Exec p_User_Insert 10004,成风,412827199510210025,1,42,'123456'

Exec p_User_Insert 10005,李白,412825199510213608,0,36,'123456'
Exec p_User_Insert 10006,杜浦,412825199510216059,0,23,null
Exec p_User_Insert 10007,李煜,412825199510212548,1,22,'123456'
Exec p_User_Insert 10008,曹操,412825199510211485,0,18,null
Exec p_User_Insert 10009,白居易,412825199510210025,0,42,'123456'

Exec p_User_Insert 10010,孙尚香,412824199510213608,1,36,'123456'
Exec p_User_Insert 10011,哈心,412824199510216059,1,23,null
Exec p_User_Insert 10012,大老师,412824199510212548,0,22,'123456'
Exec p_User_Insert 10013,黄渤,412824199510211485,0,18,null
Exec p_User_Insert 10014,黄宗泽,412824199510210025,0,42,'123456'
go

--向订单表中插入测试数据
Exec p_Orders_Insert 1,125.00,'微信',2
Exec p_Orders_Insert 2,258.00,'支付宝',6
Exec p_Orders_Insert 3,98.00,'微信',10
Exec p_Orders_Insert 4,256.00,'现金',6
Exec p_Orders_Insert 5,148.00,'支付宝',10
Exec p_Orders_Insert 6,306.00,'微信',10
Exec p_Orders_Insert 7,119.5,'支付宝',2
Exec p_Orders_Insert 8,1028.00,'微信',6
Exec p_Orders_Insert 9,97.00,'微信',2
Exec p_Orders_Insert 10,108.00,'支付宝',10
Exec p_Orders_Insert 1,214.00,'现金',2
Exec p_Orders_Insert 2,305.00,'支付宝',6
Exec p_Orders_Insert 3,125.00,'微信',2
Exec p_Orders_Insert 4,125.00,'现金',6
Exec p_Orders_Insert 5,157.00,'支付宝',10
Exec p_Orders_Insert 6,168.00,'微信',2
go

--向订单详情中插入测试数据
Exec p_DetailOrder_Insert 1,1,10.25,24.31
Exec p_DetailOrder_Insert 4,1,1,159.00
Exec p_DetailOrder_Insert 2,1,10.25,24.31
Exec p_DetailOrder_Insert 8,1,1,159.00
Exec p_DetailOrder_Insert 1,1,10.25,24.31
Exec p_DetailOrder_Insert 4,1,1,159.00
Exec p_DetailOrder_Insert 2,1,10.25,24.31
Exec p_DetailOrder_Insert 8,1,1,159.00

Exec p_DetailOrder_Insert 1,2,10.25,24.31
Exec p_DetailOrder_Insert 4,2,1,159.00
Exec p_DetailOrder_Insert 2,2,10.25,24.31
Exec p_DetailOrder_Insert 8,3,1,159.00
Exec p_DetailOrder_Insert 1,3,10.25,24.31
Exec p_DetailOrder_Insert 4,4,1,159.00
Exec p_DetailOrder_Insert 2,4,10.25,24.31
Exec p_DetailOrder_Insert 8,5,1,159.00

Exec p_DetailOrder_Insert 1,6,10.25,24.31
Exec p_DetailOrder_Insert 4,6,1,159.00
Exec p_DetailOrder_Insert 2,6,10.25,24.31
Exec p_DetailOrder_Insert 8,6,1,159.00
Exec p_DetailOrder_Insert 1,7,10.25,24.31
Exec p_DetailOrder_Insert 4,7,1,159.00
Exec p_DetailOrder_Insert 2,7,10.25,24.31
Exec p_DetailOrder_Insert 8,8,1,159.00

Exec p_DetailOrder_Insert 1,9,10.25,24.31
Exec p_DetailOrder_Insert 4,9,1,159.00
Exec p_DetailOrder_Insert 2,10,10.25,24.31
Exec p_DetailOrder_Insert 8,11,1,159.00
Exec p_DetailOrder_Insert 1,11,10.25,24.31
Exec p_DetailOrder_Insert 4,12,1,159.00
Exec p_DetailOrder_Insert 2,13,10.25,24.31
Exec p_DetailOrder_Insert 8,13,1,159.00

Exec p_DetailOrder_Insert 1,14,10.25,24.31
Exec p_DetailOrder_Insert 4,14,1,159.00
Exec p_DetailOrder_Insert 2,14,10.25,24.31
Exec p_DetailOrder_Insert 8,14,1,159.00
Exec p_DetailOrder_Insert 1,14,10.25,24.31
Exec p_DetailOrder_Insert 4,14,1,159.00
Exec p_DetailOrder_Insert 2,14,10.25,24.31
Exec p_DetailOrder_Insert 8,14,1,159.00

Exec p_DetailOrder_Insert 1,15,10.25,24.31
Exec p_DetailOrder_Insert 4,15,1,159.00
Exec p_DetailOrder_Insert 2,15,10.25,24.31
Exec p_DetailOrder_Insert 8,15,1,159.00
Exec p_DetailOrder_Insert 1,15,10.25,24.31
Exec p_DetailOrder_Insert 4,15,1,159.00
Exec p_DetailOrder_Insert 2,15,10.25,24.31
Exec p_DetailOrder_Insert 8,15,1,159.00

Exec p_DetailOrder_Insert 1,16,10.25,24.31
Exec p_DetailOrder_Insert 4,16,1,159.00
Exec p_DetailOrder_Insert 2,16,10.25,24.31
Exec p_DetailOrder_Insert 8,16,1,159.00
Exec p_DetailOrder_Insert 1,16,10.25,24.31
Exec p_DetailOrder_Insert 4,16,1,159.00
Exec p_DetailOrder_Insert 2,16,10.25,24.31
Exec p_DetailOrder_Insert 8,16,1,159.00

Exec p_DetailOrder_Insert 1,12,10.25,24.31
Exec p_DetailOrder_Insert 4,12,1,159.00
Exec p_DetailOrder_Insert 2,12,10.25,24.31
Exec p_DetailOrder_Insert 8,9,1,159.00
Exec p_DetailOrder_Insert 1,9,10.25,24.31
Exec p_DetailOrder_Insert 4,9,1,159.00
Exec p_DetailOrder_Insert 2,10,10.25,24.31
Exec p_DetailOrder_Insert 8,10,1,159.00

Exec p_DetailOrder_Insert 1,6,10.25,24.31
Exec p_DetailOrder_Insert 4,6,1,159.00
Exec p_DetailOrder_Insert 2,4,10.25,24.31
Exec p_DetailOrder_Insert 8,4,1,159.00
Exec p_DetailOrder_Insert 1,4,10.25,24.31
Exec p_DetailOrder_Insert 4,3,1,159.00
Exec p_DetailOrder_Insert 2,3,10.25,24.31
Exec p_DetailOrder_Insert 8,3,1,159.00
go

--向日志字典插入信息
Exec proc_LogDic_Insert '登录系统'
Exec proc_LogDic_Insert '退出系统'
Exec proc_LogDic_Insert '添加管理员'
Exec proc_LogDic_Insert '删除管理员'
Exec proc_LogDic_Insert '修改管理员'
Exec proc_LogDic_Insert '添加会员'
Exec proc_LogDic_Insert '删除会员'
Exec proc_LogDic_Insert '修改会员'
Exec proc_LogDic_Insert '添加商品类别'
Exec proc_LogDic_Insert '修改商品类别'
Exec proc_LogDic_Insert '删除商品类别'
Exec proc_LogDic_Insert '添加商品信息'
Exec proc_LogDic_Insert '修改商品信息'
Exec proc_LogDic_Insert '上架商品信息'
Exec proc_LogDic_Insert '下架商品信息'
Exec proc_LogDic_Insert '添加入库信息'
Exec proc_LogDic_Insert '修改入库信息'
Exec proc_LogDic_Insert '删除入库信息'
Exec proc_LogDic_Insert '添加出库信息'
Exec proc_LogDic_Insert '修改出库信息'
Exec proc_LogDic_Insert '删除出库信息'
Exec proc_LogDic_Insert '添加库存信息'
Exec proc_LogDic_Insert '修改库存信息'
Exec proc_LogDic_Insert '删除库存信息'
Exec proc_LogDic_Insert '修改密码'
Exec proc_LogDic_Insert '修改用户名'

select * from Product s1,Type s2,Unit s3 where s1.TypeID=s2.TypeID and s1.UnitID = s3.UnitID


