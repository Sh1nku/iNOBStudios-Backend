function createJwt(url, form) {
    let username = $("#username").val();
    let password = $('#password').val();

    let json = '{ "username" : "' + username + '", "password" : "' + password + '" }';

    $.ajax({
        url: url,
        type: 'POST',
        data: json,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log("Success");
            localStorage.setItem("jwt", data);
        }
    });
    document.getElementById("account").submit();

}

$(document).ready(function () {
    let pwdField = document.getElementById("password");
    let usernameField = document.getElementById("username");
    let button = document.getElementById("button");
    pwdField.addEventListener("keydown", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            button.click();
        }
    });
    usernameField.addEventListener("keydown", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            button.click();
        }
    });

});