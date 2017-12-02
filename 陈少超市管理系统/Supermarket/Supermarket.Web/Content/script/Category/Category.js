$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();
})

//行内触发编辑
function Edit(value) {
    $("#myModalLabel").text("编辑商品类别");
    $("#txt_TypeName").val($(value).parent().parent().find("td:eq(2)").text());
    $("#txt_TypeID").val($(value).parent().parent().find("td:eq(1)").text());
    $('#myModal').modal();
}

//行内触发删除
function Delete(value) {
    var TypeIDs = new Array();
    TypeIDs[0] = $(value).parent().parent().find("td:eq(1)").text();
    confirm_ = confirm('确定要删除吗?');
    if (confirm_) {
        $.ajax({
            type: "post",
            url: "/Category/Delete",
            data: { "TypeIDs": TypeIDs },
            success: function (data) {
                if (data.state == "success") {
                    window.alert(data.message);
                } else {
                    window.alert("网络延迟，请稍后再试");
                }
            },
            error: function () {
                window.alert("操作失败,请稍后再试！");
            },
            complete: function () {
                $("#Category").bootstrapTable('refresh');
            }
        });
    }
}

var TableInit = function () {
    var oTableInit = new Object();

    //初始化Table
    oTableInit.Init = function () {
        $('#Category').bootstrapTable({
            url: '/Category/GetInfo',         //请求后台的URL（*）
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
            uniqueId: "TypeID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                checkbox: true                    //显示勾选框
            }, {
                field: 'TypeID',                   //实际元素            
                title: '类别编号'                   //显示文本                    
            },
            {
                field: 'TypeName',
                title: '类别名称'
            }, {
                field: 'abs',
                title: '操作',
                width: 300,
                formatter: formatOperation
            }
            ]
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset  //页码
        };
        return temp;
    };
    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};
    oInit.Init = function () {

        //点击新增触发事件
        $("#btn_add").click(function () {
            //模态框属性设置并显示
            $("#myModalLabel").text("添加商品类别");
            $("#myModal").find(".form-control").val("");
            $('#myModal').modal();
        });

        //点击编辑触发事件
        $("#btn_edit").click(function () {
            //获取当前选中行数据
            var arrselections = $("#Category").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                alert('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                alert('请选择有效数据');
                return;
            }
            $("#myModalLabel").text("编辑商品类别");
            $("#txt_TypeName").val(arrselections[0].TypeName);
            $("#txt_TypeID").val(arrselections[0].TypeID);
            $('#myModal').modal();
        });

        //商品类别删除
        $("#btn_delete").click(function () {

            //获取当前选中行数据
            var arrselections = $("#Category").bootstrapTable('getSelections');
            var info = JSON.stringify(arrselections);
            var TypeIDs = new Array();

            if (arrselections.length <= 0) {
                alert("请至少选择一列！");
                return;
            }
            else {
                //数组遍历选中项数据
                $.each(eval(info), function (index, item) {
                    TypeIDs[index] = item.TypeID;
                });
            }
            //友情提示，是否操作？
            confirm_ = confirm('确定要删除吗?');
            if (confirm_) {
                $.ajax({
                    type: "post",
                    url: "/Category/Delete",
                    data: { "TypeIDs": TypeIDs },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                        }
                        else {
                            window.alert("网络延迟，请稍后再试");
                        }
                    },
                    error: function () {
                        window.alert("操作失败,请稍后再试！");
                    },
                    complete: function () {
                        $("#Category").bootstrapTable('refresh');
                    }
                });
            }
        });

        //对话框提交触发事件
        $("#btn_submit").click(function () {

            var data = $('#form').serialize();
            //序列化获得表单数据，
            var submitData = decodeURIComponent(data, true);
            if ($("#myModalLabel").text() == "添加商品类别") {
                $.ajax({
                    type: 'post',
                    url: "/Category/AddType",
                    data: submitData,
                    beforeSend: function () {
                        return true;
                    },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                            $('#myModal').modal('hide');
                            $("#Category").bootstrapTable('refresh');
                        } else {
                            window.alert("网络延迟，请稍后再试");
                        }
                    },
                    error: function () {
                        window.alert("操作失败，请稍后再试！");
                    }
                })
            }
            else {
                $.ajax({
                    type: 'post',
                    url: "/Category/Edit",
                    data: submitData,
                    beforeSend: function () {
                        return true;
                    },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                            $('#myModal').modal('hide');
                            $("#Category").bootstrapTable('refresh');
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
    };
    return oInit;
};