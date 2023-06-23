function Success(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "موفق";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "success",
        confirmButtonText: "باشه",
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}

function Info(Title, description) {
    if (Title == null || Title == "undefined") {
        Title = "توجه";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "info",
        confirmButtonText: "باشه"
    });
}

function ErrorAlert(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "ناموفق";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "error",
        confirmButtonText: "باشه"
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}

function Warning(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "ناموفق";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "warning",
        confirmButtonText: "باشه"
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return decodeURIComponent(c.substring(name.length, c.length));
        }
    }
    return "";
}



function deleteCookie(cookieName) {
    document.cookie = `${cookieName}=;expires=Thu, 01 Jan 1970;path=/`;
}

$(document).ready(function () 
{

    var result = getCookie("SystemAlert");
    if (result){
        result = JSON.parse(result);

        if (result.Type === 200) 
           {
            Success(result.Title, result.Message, result.isReloadPage);
           }
        else if (result.Type === 10)
           {
            ErrorAlert(result.Title, result.Message);
           }
        else if (result.Type === 404) 
           {
            Warning(result.Title, result.Message);
           }
        deleteCookie("SystemAlert");
    }
});


