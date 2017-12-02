$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();   
});
//激活会员的方法
function activate(value) {
    if (confirm("确定要激活该会员吗？")) {
        var UserID = $(value).parent().parent().find("td:eq(1)").text();
        $.ajax({
            type: "post",
            url: "/User/Activate",
            data: { "UserID": UserID },
            success: function (data) {
                if (data.state == "success") {
                    window.alert(data.message);
                    $("#User").bootstrapTable('refresh');
                }
            },
            error: function () {
                alert("操作失败,请稍后再试！");

            },
            complete: function () {
                $("#User").bootstrapTable('refresh');
            }
        });
    }
}

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#User').bootstrapTable({
            url: '/User/GetInfo',         //请求后台的URL（*）
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
            uniqueId: "UserID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            editable: true,
            columns: [{
                checkbox: true                    //显示勾选框
            }, {
                field: 'UserID',                   //实际元素            
                title: '会员编号'                   //显示文本                    
            },
            {
                field: 'UserName',
                title: '会员名称'
            }, {
                field: 'IDNumber',
                title: '身份证号',

            }, {
                field: 'CardID',
                title: '会员卡号'
            }, {
                field: 'TotalCost',
                title: '消费金额'
            }, {
                field: 'Score',
                title: '积分'

            }, {
                field: 'Sex',
                title: '性别',
                formatter: formatSex

            }, {
                field: 'Age',
                title: '年龄'
            }, {
                field: 'UserPassWord',
                title: '消费密码'
            }, {
                field: 'AlterInTime',
                title: '修改时间',
                formatter: formatDatebox
            }, {
                field: 'IsDelete',
                title: '数据状态',
                formatter: formatIsUserDelete
            }
            ]
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            UserName:$("#searchName").val(),
            UserCard:$("#searchCard").val()
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
            $("#myModalLabel").text("添加会员信息");
            $("#myModal").find(".form-control").val("");
            $("#lbl_error").html("");
            $('#myModal').modal();
        });

        //点击编辑触发事件
        $("#btn_edit").click(function () {
            $("#lbl_error").html("");
            //获取当前选中行数据
            var arrselections = $("#User").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                alert('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                alert('请选择有效数据');
                return;
            }
            //模态框属性设置并显示
            $("#myModalLabel").text("编辑会员信息");
            $("#txt_UserName").val(arrselections[0].UserName);
            $("#txt_IDNumber").val(arrselections[0].IDNumber);
            if (arrselections[0].Sex == 0) {
                $("#rd_Sex0").attr("checked","checked");
            } else {
                $("#rd_Sex1").attr("checked", "checked");
            }
            $("#txt_Age").val(arrselections[0].Age);
            $("#txt_UserPassWord").val(arrselections[0].UserPassWord);
            $("#txt_UserID").val(arrselections[0].UserID);
            $("#txt_CardID").val(arrselections[0].CardID);
            $('#myModal').modal();
        });

        //点击删除触发事件
        $("#btn_delete").click(function () {
            //获取当前选中行数据
            var arrselections = $("#User").bootstrapTable('getSelections');
            var info = JSON.stringify(arrselections);
            var UserIDs = new Array();
            if (arrselections.length <= 0) {
                alert("请至少选择一列！");
                return;
            }
            else {
                //数组遍历选中项数据
                $.each(eval(info), function (index, item) {
                    UserIDs[index] = item.UserID;
                });           
            }
            //友情提示，是否删除？
            confirm_ = confirm('你确定要进行删除吗?');
            if (confirm_) {
                $.ajax({
                    type: "post",
                    url: "/User/Delete",
                    data: { "UserIDs": UserIDs },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                            $("#User").bootstrapTable('refresh');
                        } else {
                            window.alert("网络延迟，请稍后再试");
                        }
                    },
                    error: function () {
                        alert("操作失败,请稍后再试！");

                    },
                    complete: function () {
                        $("#User").bootstrapTable('refresh');
                    }

                });
            }

        });

        //对话框提交触发事件
        $("#btn_submit").click(function () {

            $.lbl_error = {
                formMessage: function (msg) {
                    $("#lbl_error").append(msg+"<br/>");
                }
            }

            if ($("#myModalLabel").text() == "添加会员信息") {
                var data = $('#form').serialize();
                //序列化获得表单数据，
                var submitData = decodeURIComponent(data, true);
                //submitData是解码后的表单数据，结果同上            
                //执行添加
                $.ajax({
                    type:'post',
                    url: "/User/AddUser",
                    data: submitData,
                    beforeSend: function () {
                        var state = true;
                        $("#lbl_error").html("");
                        if (!IsDBNull($("#txt_UserName").val())) {
                            $.lbl_error.formMessage("用户名不能为空");
                            state = false;
                        }
                        if (!IDCardValidate($("#txt_IDNumber").val())) {
                            $.lbl_error.formMessage("身份证号不符合要求");
                            state = false;
                        }
                        if (IsDBNull($("#txt_Age").val()) && !valiDateAge($("#txt_Age").val())) {
                            $.lbl_error.formMessage("请填写正确的年龄或不填");
                            state = false;
                        }
                        if (IsDBNull($("#txt_UserPassWord").val()) && !valiDatePassword($("#txt_UserPassWord").val())) {
                            $.lbl_error.formMessage("请填写正确的消费密码或不填");
                            state = false;
                        }
                        return state;
                    },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                            $('#myModal').modal('hide');
                            $("#User").bootstrapTable('refresh');
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
                var data = $('#form').serialize();
                //序列化获得表单数据，
                var submitData = decodeURIComponent(data, true);
                $.ajax({
                    type:'post',
                    url: "/User/EditUser",
                    data: submitData,
                    beforeSend: function () {
                        var state = true;
                        $("#lbl_error").html("");
                        if (!IsDBNull($("#txt_UserName").val())) {
                            $.lbl_error.formMessage("用户名不能为空");
                            state = false;
                        }
                        if (!IDCardValidate($("#txt_IDNumber").val())) {
                            $.lbl_error.formMessage("身份证号不符合要求");
                            state = false;
                        }
                        if (IsDBNull($("#txt_Age").val()) && !valiDateAge($("#txt_Age").val())) {
                            $.lbl_error.formMessage("请填写正确的年龄或不填");
                            state = false;
                        }
                        if (IsDBNull($("#txt_UserPassWord").val()) && !valiDatePassword($("#txt_UserPassWord").val())) {
                            $.lbl_error.formMessage("请填写正确的消费密码或不填");
                            state = false;
                        }
                        return state;
                    },
                    success: function (data) {
                        if (data.state == "success") {
                            window.alert(data.message);
                            $('#myModal').modal('hide');
                            $("#User").bootstrapTable('refresh');
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
            $("#User").bootstrapTable('refresh');
        });
        
    };
    return oInit;
};