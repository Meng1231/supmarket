$(function () {
    $("#all").click(function () {
        var docElm = document.documentElement;
        //W3C  
        if (docElm.requestFullscreen) {
            docElm.requestFullscreen();
        }
            //FireFox  
        else if (docElm.mozRequestFullScreen) {
            docElm.mozRequestFullScreen();
        }
            //Chrome  
        else if (docElm.webkitRequestFullScreen) {
            docElm.webkitRequestFullScreen();
        }
            //IE11
        else if (elem.msRequestFullscreen) {
            elem.msRequestFullscreen();
        }
    })
})