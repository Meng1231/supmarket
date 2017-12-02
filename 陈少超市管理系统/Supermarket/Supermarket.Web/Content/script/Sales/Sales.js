$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    var oTable = new NewTableInit();
    oTable.Init();

    GetSalesName();

    $("#SalesName").change(function () {
        $("#Sales").bootstrapTable('refresh');
    })

});

//获取销售员
function GetSalesName() {
    $("#SalesName").html("<option value='0'>全部</option>");
    $.ajax({
        type: 'post',
        url: "/Sales/GetSalesName",
        dataType: "json",
        success: function (data) {
            $.each(data, function (index, admin) {
                $("#SalesName").append("<option value=" + admin.AdminID + ">" + admin.UserName + "</option>");
            });
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.alert("获取销售员失败！");
        }
    });
}

var DetailID = 0;
function Detail(value) {
    $("#DetailModal").modal();
    DetailID = $(value).parent().parent().find("td:eq(0)").text();
    $("#Detail").bootstrapTable('refresh');
}

var NewTableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#Detail').bootstrapTable({
            url: '/Sales/GetDetail',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 5,                       //每页的记录行数（*）
            pageList: [5,10, 15, 20, 50],        //可供选择的每页的行数（*）
            showColumns: false,                  //是否显示所有的列
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行                       //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "DetailOrderID",                     //每一行的唯一标识，一般为主键列
            columns: [{
                field: 'avc',
                title: '',
                formatter: function (value, row, index) {
                    return index + 1;
                }
            }, {
                field: 'ProductName',
                title: '商品名称'
            }, {
                field: 'Amount',
                title: '数量'
            }, {
                field: 'subtotal',
                title: '小计'
            }
            ]
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset, //页码
            DetailID: DetailID
        };
        return temp;
    };
    return oTableInit;
};

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#Sales').bootstrapTable({
            url: '/Sales/GetInfo',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 15, 20, 50],        //可供选择的每页的行数（*）
            search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,                  //严格搜索
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行                       //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "OrderFormID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                field: 'OrderFormID',                   //实际元素            
                title: '销售编号'                   //显示文本                    
            },
            {
                field: 'UserName',
                title: '顾客姓名'
            }, {
                field: 'SunPrice',
                title: '总价',

            }, {
                field: 'Way',
                title: '支付方式'
            }, {
                field: 'AdminName',
                title: '收银员'
            }, {
                field: 'CheckInTime',
                title: '销售时间',
                formatter: formatDatebox

            }, {
                field: 'abc',
                title: '操作',
                formatter: formatDetail
            }]
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            SalesID: $("#SalesName").val()
        };
        return temp;
    };
    return oTableInit;
};