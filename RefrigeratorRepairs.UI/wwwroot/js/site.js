$(document).ready(function () {
    LoadCkEditor4();
   
});

function LoadCkEditor4() {
    console.log("LoadCkEditor4");

    if (!document.getElementById("ckEditor4"))
        return;
    console.log("LoadCkEditor42");

    $("body").append("<script src='/lib/ckeditor4/ckeditor/ckeditor.js'></script>");

    CKEDITOR.replace('ckEditor4',
        {
            customConfig: '/lib/ckeditor4/ckeditor/config.js'
        });
}