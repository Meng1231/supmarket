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
    $("#ProductType").html("<option value='0'>请选择</option>");
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

//商品上下架的方法
function IsActivate(value) {
    var state = 0;
    var text = $(value).text();
    if ( text== " 下架") {
        state = 1;
    }
    var ProductID = $(value).parent().parent().find("td:eq(1)").text();
    if (confirm("确定要" + text + "商品吗？")) {
        $.ajax({
            type: 'post',
            url: '/Product/IsActivate',
            data: { "ProductID": ProductID, "state": state },
            dataType: 'json',
            success: function (data) {
                if (data.state = "success") {
                    window.alert(text + data.message);
                    $("#Product").bootstrapTable('refresh');
                }
            }, error: function () {
                window.alert("操作失败，请稍后再试！");
                $("#Product").bootstrapTable('refresh');
            }
        });
    }
}

var TableInit = function () {
    var oTableInit = new Object();

    //初始化Table
    oTableInit.Init = function () {
        $('#Product').bootstrapTable({
            url: '/Product/GetInfo',         //请求后台的URL（*）
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
            uniqueId: "ProductID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                checkbox: true                    //显示勾选框
            }, {
                field: 'ProductID',                   //实际元素            
                title: '商品编号'                   //显示文本                    
            },
            {
                field: 'ProductName',
                title: '商品名称'
            }, {
                field: 'CommodityPriceIn',
                title: '进货价'
            }, {
                field: 'CommodityPriceOut',
                title: '出售价'
            }, {
                field: 'TypeName',
                title: '商品类别',

            }, {
                field: 'UnitName',
                title: '单位'
            }, {
                field: 'Manufacturer',
                title: '生产厂家'

            }, {
                field: 'CommodityDepict',
                title: '商品描述',
                formatter: function (value, row, index) {
                    return value.length > 25 ? value.substring(0, 25) + "..." : value;
                }
            }, {
                field: 'IsDelete',
                title: '操作',
                formatter: formatProductIsDelete
            }
            ], onCheck: function (item, element) {
                if (item.IsDelete==1) {
                    $(".ProductIn").css("display", "block");
                    $(".ProductOut").css("display", "none");
                } else {
                    $(".ProductIn").css("display", "none");
                    $(".ProductOut").css("display", "block");
                }
            }
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            ProductName: $("#searchProductNmae").val(),
            type: $("#ProductType").val(),
            IsDel: $("input[name='IsDelete']:checked").val()
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
            $("#lbl_error").html("");
            //获取当前选中行数据
            var arrselections = $("#Product").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                alert('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                alert('请选择有效数据');
                return;
            }
            //模态框属性设置并显示
            $("#myModalLabel").text("编辑商品信息");
            if (arrselections[0].IsDelete == 0) {
                $("#rb_IsDel0").attr("checked", "checked");
            } else {
                $("#rb_IsDel1").attr("checked", "checked");
            }
            $("#txt_ProductName").val(arrselections[0].ProductName);
            $("#txt_CommodityPriceOut").val(arrselections[0].CommodityPriceOut);
            $("#txt_CommodityDepict").val(arrselections[0].CommodityDepict);
            $("#txt_ProductID").val(arrselections[0].ProductID);
            $('#myModal').modal();
        });

        $(".ProductOut").bind('click',InActivate);
        $(".ProductIn").bind('click',InActivate);

        //商品批量上下架
        function InActivate() {

            //获取当前选中行数据
            var arrselections = $("#Product").bootstrapTable('getSelections');
            var info = JSON.stringify(arrselections);
            var ProductIDs = new Array();
            var state = eval(info)[0].IsDelete;

            for (var i = 0; i < eval(info).length; i++) {
                if (eval(info)[i].IsDelete != state) {
                    window.alert("请选择同一状态的商品！");
                    return;
                }
            }

            if (arrselections.length <= 0) {
                alert("请至少选择一列！");
                return;
            }
            else {
                //数组遍历选中项数据
                $.each(eval(info), function (index, item) {
                    ProductIDs[index] = item.ProductID;
                });
            }
            //友情提示，是否操作？
            confirm_ = confirm('确定要进行' + (state == 0 ? "下架" : "上架") + '操作吗?');
            if (confirm_) {
                $.ajax({
                    type: "post",
                    url: "/Product/InActivate",
                    data: { "ProductIDs": ProductIDs,"state":state==1?0:1 },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                        }
                    },
                    error: function () {
                        window.alert("操作失败,请稍后再试！");
                    },
                    complete: function () {
                        $("#Product").bootstrapTable('refresh');
                    }

                });
            }

        };

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
                    url: "/Product/Edit",
                    data: submitData,
                    beforeSend: function () {
                        var state = true;
                        $("#lbl_error").html("");
                        if (!IsMoney($("#txt_CommodityPriceOut").val())) {
                            $.lbl_error.formMessage("请填写正确的出售价");
                            state = false;
                        }
                        return state;
                    },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                            $('#myModal').modal('hide');
                            $("#Product").bootstrapTable('refresh');
                        }
                    },
                    error: function () {
                        window.alert("操作失败，请稍后再试！");
                    }
                })
        });

        //查询触发事件
        $("#btn_query").click(function () {
            $("#Product").bootstrapTable('refresh');
        });
    };
    return oInit;
};