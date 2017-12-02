$(function () {
    $("#Exit").click(function () {
        if (confirm("你确定要退出吗？")) {
            $.ajax({
                type:'post',
                url: '/Login/Exit',
                success:function(data){
                    if (data.state == "success") {
                        window.location.replace("/Login/Index");
                    }
                }
            })
        }
    })
});