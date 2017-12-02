$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    GetAllAdmin();

    $("#AdminName").change(function () {
        $("#SysLog").bootstrapTable('refresh');
    })
});


//获取管理员信息
function GetAllAdmin() {
    $("#AdminName").html("<option value='0'>全部</option>");
    $.ajax({
        type: 'post',
        url: "/SysLog/GetAllAdmin",
        dataType: "json",
        success: function (data) {
            $.each(data, function (index, admin) {
                $("#AdminName").append("<option value=" + admin.AdminID + ">" + admin.UserName + "</option>");
            });
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.alert("获取管理员失败！");
        }
    });
}

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#SysLog').bootstrapTable({
            url: '/SysLog/GetInfo',         //请求后台的URL（*）
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
            uniqueId: "LogID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                field: 'LogID',                   //实际元素            
                title: '日志编号'                   //显示文本                    
            },
            {
                field: 'Behavior',
                title: '操作行为'
            }, {
                field: 'TypeName',
                title: '行为类型',

            }, {
                field: 'UserName',
                title: '操作人'
            }, {
                field: 'Parameters',
                title: '参数'
            }, {
                field: ' IP',
                title: 'IP'
            }, {
                field: 'CheckInTime',
                title: '操作时间',
                formatter: formatDatebox

            },  {
                field: 'Exception',
                title: '异常信息'
            }
            ]

        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            AdminID: $("#AdminName").val()
        };
        return temp;
    };
    return oTableInit;
};