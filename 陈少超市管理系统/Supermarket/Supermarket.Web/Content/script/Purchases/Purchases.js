$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    GetPurchasesName();

    var oTable = new NewTableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();
});
function GetPurchasesName() {
    $("#AdminName").html("<option value='0'>全部</option>");
    $.ajax({
        type: 'post',
        url: "/Purchases/GetPurchasesName",
        dataType: "json",
        success: function (data) {
            $.each(data, function (index, admin) {
                $("#AdminName").append("<option value=" + admin.AdminID + ">" + admin.UserName + "</option>");
            });
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.alert("获取创库管理员失败！");
        }
    });
}
var PurID = 0;
function Detail(value) {
    $("#DetailModal").modal();
    PurID = $(value).parent().parent().find("td:eq(1)").text();
    $("#Detail").bootstrapTable('refresh');
}

var NewTableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#Detail').bootstrapTable({
            url: '/Purchases/GetDetail',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 15, 20, 50],        //可供选择的每页的行数（*）
            showColumns: false,                  //是否显示所有的列
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行                       //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "DetailePurchasesID",                     //每一行的唯一标识，一般为主键列
            columns: [{
                field: 'avc',                             
                title: '',
                formatter: function (value,row,index) {
                    return index + 1;
                }
            }, {
                field: 'ProductName',
                title: '商品名称'
            }, {
                field: 'UnitName',
                title: '单位'
            }, {
                field: 'TypeName',
                title: '类型'
            }, {
                field: 'ProductAmount',
                title: '进货数量'
            }, {
                field: 'Reamrk',
                title: '简介'
            }
            ]
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset, //页码
            PurID: PurID
        };
        return temp;
    };
    return oTableInit;
};

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#Purchases').bootstrapTable({
            url: '/Purchases/GetInfo',         //请求后台的URL（*）
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
            uniqueId: "PurchasesID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                checkbox: true                    //显示勾选框
            }, {
                field: 'PurchasesID',                   //实际元素            
                title: '进货编号'                   //显示文本                    
            }, {
                field: 'UserName',
                title: '进货经手人'
            }, {
                field: 'PurchasesTotal',
                title: '总价'
            }, {
                field: 'Reamrk',
                title: '备注'
            }, {
                field: 'PurchasesTime',
                title: '进货时间',
                formatter: formatDatebox
            }, {
                field: 'avs',
                title: '操作',
                formatter: formatDetail
            }
            ]
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            AdminID: $("#AdminName").val(),
            StarTime:$("#StarTime").val(),
            EndTime:$("#EndTime").val()
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
            var arrselections = $("#Purchases").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                alert('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                alert('请选择有效数据');
                return;
            }
            //模态框属性设置并显示
            $("#myModalLabel").text("修改入库信息");
            $("#txt_PurchasesTotal").val(arrselections[0].PurchasesTotal);
            $("#txt_Reamrk").val(arrselections[0].Reamrk);
            $("#txt_PurchasesID").val(arrselections[0].PurchasesID);

            $('#myModal').modal();
        });

        //点击删除触发事件
        $("#btn_delete").click(function () {
            //获取当前选中行数据
            var arrselections = $("#Purchases").bootstrapTable('getSelections');
            var info = JSON.stringify(arrselections);
            var PurIDs = new Array();
            if (arrselections.length <= 0) {
                alert("请至少选择一列！");
                return;
            }
            else {
                //数组遍历选中项数据
                $.each(eval(info), function (index, item) {
                    PurIDs[index] = item.PurchasesID;
                });
            }
            //友情提示，是否删除？
            confirm_ = confirm('你确定要进行删除吗?');
            if (confirm_) {
                $.ajax({
                    type: "post",
                    url: "/Purchases/Delete",
                    data: { "PurIDs": PurIDs },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                        } else {
                            window.alert("网络延迟，请稍后再试");
                        }
                    },
                    error: function () {
                        alert("操作失败,请稍后再试！");

                    },
                    complete: function () {
                        $("#Purchases").bootstrapTable('refresh');
                    }

                });
            }

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
            //submitData是解码后的表单数据，结果同上      
            if ($("#myModalLabel").text() == "添加会员信息") {   

            }
            else {
                $.ajax({
                    type: 'post',
                    url: "/Purchases/EditPurchases",
                    data: submitData,
                    beforeSend: function () {
                        var state = true;
                        $("#lbl_error").html("");
                        if (!IsMoney($("#txt_PurchasesTotal").val())) {
                            $.lbl_error.formMessage("请填写正确的总价");
                            state = false;
                        }
                        return state;
                    },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                            $('#myModal').modal('hide');
                            $("#Purchases").bootstrapTable('refresh');
                        } else {
                            window.alert("网络延迟，请稍后再试");
                        }
                    },
                    error: function () {
                        window.alert("操作失败，请稍后再试！");
                    }
                })
            }
        });

        //查询触发事件
        $("#btn_query").click(function () {
            $("#Purchases").bootstrapTable('refresh');
        });

    };
    return oInit;
};