
$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

    //隐藏RoleID列
    $('#tb_departments').bootstrapTable('hideColumn', 'RoleID');

    //初始化select/selectpicker
    $('#order_status2').selectpicker({
    });
    //change触发事件（可删）
    $('#order_status2').change(function () {
        var i = $(this).val();
        //alert(i);
    }
        );
    GetRoels();
    
});

//填充角色信息
function GetRoels() {
    $("#RoleName").html("<option value='0'>全部</option>");
    $.ajax({
        url: "/AdminInfo/GetRoles",
        dataType: "json",
        success: function (data) {
            $.each(data, function (index, role) {
                $("#RoleName").append("<option value=" + role.RoleID + ">" + role.RoleName + "</option>");
            });
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.alert("获取角色信息失败！");
        }
    });
}

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_departments').bootstrapTable({
            url: '/AdminInfo/GetInfo',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
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
            clickToSelect: true,                //是否启用点击选中行
            uniqueId: "AdminID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                checkbox: true                    //显示勾选框
            }, {
                field: 'AdminID',                   //实际元素            
                title: '管理编号'                   //显示文本                    
            },
            {
                field: 'RoleID',
                title: '角色ID'
            }, {
                field: 'RoleName',
                title: '角色名称'

            }, {
                field: 'UserName',
                title: '用户名',
                //启用编辑（暂不可用）
                //editable: {
                //    type: 'text',
                //    title: '用户名',                   
                //    validate: function (v) {
                //        if (!v) return '用户名不能为空';

                //    }
                //}

            }, {
                field: 'Account',
                title: '账号'
            }, {
                field: 'PassWord',
                title: '密码'
            }, ]
            //确认保存编辑（暂不可用）
            //onEditableSave: function (field, row, oldValue, $el) {
            //    $.ajax({
            //        type: "post",
            //        url: "/AdminInfo/Edit",
            //        data: row,
            //        dataType: 'JSON',
            //        success: function (data, status) {
            //            if (status == "success") {
            //                alert('提交数据成功');
            //            }
            //        },
            //        error: function () {
            //            alert('编辑失败');
            //        },
            //        complete: function () {

            //        }

            //    });
            //}
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            rolename: $("#RoleName").val(),//角色名称
            username: $("#text_UserName").val() //用户名
        };
        return temp;
    };
    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};
    oInit.Init = function () {
        GetRoels1();
        //点击新增触发事件
        $("#btn_add").click(function () {
            $("#order_status2").val(0); //回到初始状态
            $("#order_status2").selectpicker('refresh');//对searchPayState这个下拉框进行重置刷新 
            //模态框属性设置并显示
            $("#myModalLabel").text("新增");
            $("#myModal").find(".form-control").val("");
            $('#myModal').modal();
        });

        //点击编辑触发事件
        $("#btn_edit").click(function () {
            //获取当前选中行数据
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                alert('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                alert('请选择有效数据');
                return;
            }
            //模态框属性设置并显示
            $("#myModalLabel").text("编辑");
            $("#txt_UserName").val(arrselections[0].UserName);
            $("#txt_Account").val(arrselections[0].Account);
            $("#txt_PassWord").val(arrselections[0].PassWord);
            $("#txt_AdminID").val(arrselections[0].AdminID);
            $("#order_status2").val(arrselections[0].RoleID);
            $("#order_status2").selectpicker('refresh');//对searchPayState这个下拉框进行重置刷新                          
            $('#myModal').modal();
        });

        //点击删除触发事件
        $("#btn_delete").click(function () {
            //获取当前选中行数据
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            var info = JSON.stringify(arrselections);
            var AdminIDs = new Array();
            if (arrselections.length <= 0) {
                alert("请至少选择一列！");
                return;
            }
            else {
                // alert('getSelections: ' + JSON.stringify(arrselections));     
                //alert(info);
                //数组遍历选中项数据
                $.each(eval(info), function (index, item) {
                    AdminIDs[index] = item.AdminID;
                });
                //alert(AdminIDs);                
            }
            //友情提示，是否删除？
            confirm_ = confirm('Are you sure?');
            if (confirm_) {
                $.ajax({
                    type: "post",
                    url: "/AdminInfo/Delete",
                    data: { "AdminIDs": AdminIDs },
                    success: function (data) {
                        if (data.state == "success") {
                            $("#tb_departments").bootstrapTable('refresh');
                            window.alert(data.message);
                        }
                    },
                    error: function () {
                        window.alert("操作失败，请稍后再试");

                    },
                    complete: function () {
                        $("#tb_departments").bootstrapTable('refresh');
                    }

                });
                //alert("Refresh!");
                //$("#tb_departments").bootstrapTable('refresh');
            }

        });

        //对话框提交触发事件
        $("#btn_submit").click(function () {

            if ($("#myModalLabel").text() == "新增") {

                var data = $('#form').serialize();
                //序列化获得表单数据，
                var submitData = decodeURIComponent(data, true);
                //submitData是解码后的表单数据，结果同上            
                //执行添加
                $.ajax({
                    url: "/AdminInfo/Add",
                    data: submitData,
                    beforeSend: function () {
                        //请求前
                    },
                    success: function (data) {
                        //请求成功时
                        if (data.state == "success") {
                            $("#tb_departments").bootstrapTable('refresh');
                            window.alert(data.message);
                        }
                        else {
                            window.alert("网络延迟，请稍后再试！");
                        }
                    },
                    error: function () {
                        window.alert("操作失败，请稍后再试!");
                    }
                })

            }
            else {
                // alert("Edit is loading");
                var data = $('#form').serialize();
                //序列化获得表单数据，
                var submitData = decodeURIComponent(data, true);
                $.ajax({
                    url: "/AdminInfo/Edit",
                    data: submitData,
                    beforeSend: function () {
                        //请求前
                    },
                    success: function (data) {
                        //请求成功时
                        if (data.state == "success") {
                            $("#tb_departments").bootstrapTable('refresh');
                            window.alert(data.message);
                        }
                        else {
                            window.alert("网络延迟，请稍后再试！");
                        }
                    },
                    error: function () {
                        window.alert("操作失败，请稍后再试!");
                    }
                })
            }
        });

        //查询触发事件
        $("#btn_query").click(function () {
            $("#tb_departments").bootstrapTable('refresh');
        });

        //填充角色信息
        function GetRoels1() {
            $("#order_status2").html("<option value='0'>请选择</option>");
            $.ajax({
                url: "/AdminInfo/GetRoles",
                dataType: "json",
                success: function (data) {
                    $.each(data, function (index, role) {
                        $("#order_status2").append("<option value=" + role.RoleID + ">" + role.RoleName + "</option>");
                    });
                }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                    window.alert("获取角色信息失败！");
                }
            });
        }
    };
    return oInit;
};