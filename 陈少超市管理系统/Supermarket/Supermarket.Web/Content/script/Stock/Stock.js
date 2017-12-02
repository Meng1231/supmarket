$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    GetProductType();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();
});

//获取商品类别信息
function GetProductType() {
    $("#ProductType").html("<option value='0'>全部</option>");
    $.ajax({
        type: 'post',
        url: "/Product/GetProductType",
        dataType: "json",
        success: function (data) {
            $.each(data, function (index, type) {
                $("#ProductType").append("<option value=" + type.TypeID + ">" + type.TypeName + "</option>");
            });
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.alert("获取商品类型失败！");
        }
    });
}

//行内触发编辑
function Edit(value) {
    $("#txt_CommodityStockAmount").val($(value).parent().parent().find("td:eq(5)").text());
    $("#txt_StockID").val($(value).parent().parent().find("td:eq(1)").text());
    $('#myModal').modal();
}

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#Stock').bootstrapTable({
            url: '/Stock/GetInfo',         //请求后台的URL（*）
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
            uniqueId: "StockID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                checkbox: true                    //显示勾选框
            }, {
                field: 'StockID',                   //实际元素            
                title: '库存编号'                   //显示文本                    
            },
            {
                field: 'ProductName',
                title: '商品名称'
            }, {
                field: 'UnitName',
                title: '单位',

            }, {
                field: 'TypeName',
                title: '类型'
            }, {
                field: 'CommodityStockAmount',
                title: '库存量',
                formatter: formatStockAmout
            }, {
                field: 'StockDown',
                title: '库存下限'
            }, {
                field: 'StockUp',
                title: '存库上限'

            }, {
                field: 'StorageTime',
                title: '入库时间',
                formatter: formatDatebox
            }, {
                field: 'AlterTime',
                title: '修改时间',
                formatter: formatDatebox
            }, {
                field: 'abc',
                title: '操作',
                formatter: function (value, row, index) {
                    return '<i class="btn btn-success fa fa-pencil-square-o" onclick="Edit(this)" title="点击进行编辑"> 编辑</i>';
                }
            }
            ]

        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            ProductName: $("#ProductNmae").val(),
            TypeID: $("#ProductType").val(),
            StorageStarTime: $("#StorageStarTime").val(),
            StorageEndTime: $("#StorageEndTime").val()
        };
        return temp;
    };
    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};
    oInit.Init = function () {

        //点击编辑触发事件
        $("#btn_edit").click(function () {
            $("#lbl_error").html();
            //获取当前选中行数据
            var arrselections = $("#Stock").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                alert('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                alert('请选择有效数据');
                return;
            }
            //模态框属性设置并显示
            $("#txt_CommodityStockAmount").val(arrselections[0].CommodityStockAmount);
            $("#txt_StockID").val(arrselections[0].StockID);
            $('#myModal').modal();
        });

        //对话框提交触发事件
        $("#btn_submit").click(function () {

            $.lbl_error = {
                formMessage: function (msg) {
                    $("#lbl_error").append(msg + "<br/>");
                }
            }
            var data = $('#form').serialize();
            //序列化获得表单数据，
            var submitData = decodeURIComponent(data, true);
            $.ajax({
                type: 'post',
                url: "/Stock/Edit",
                data: submitData,
                beforeSend: function () {
                    var state = true;
                    $("#lbl_error").html("");
                    if (!IsNumber($("#txt_CommodityStockAmount").val())) {
                        $.lbl_error.formMessage("请填写正确的库存量");
                        state = false;
                    }
                    return state;
                },
                success: function (data) {
                    if (data.state == "success") {
                        window.alert(data.message);
                        $('#myModal').modal('hide');
                        $("#Stock").bootstrapTable('refresh');
                    } else {
                        window.alert("网络延迟，请稍后再试");
                    }
                },
                error: function () {
                    window.alert("操作失败，请稍后再试！");
                }
            })
        });

        //查询触发事件
        $("#btn_query").click(function () {
            $("#Stock").bootstrapTable('refresh');
        });

    };
    return oInit;
};