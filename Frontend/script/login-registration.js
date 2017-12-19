function registrationSubmitListener() {
    var blockRegistration = $("#block-registration");
    var urlRegistration = urlServer + "api/Account/Register";

    blockRegistration.find("input[type='submit']").click(function (e) {
        e.preventDefault();
        var data = {
            Email: blockRegistration.find("input[type='email']").val(),
            Password: $("input[type='password']").val(),
            ConfirmPassword: $("input[type='password']").val()
        };

        $.ajax({
            type: "POST",
            url: urlRegistration,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
        }).done(function() {
            alert("Регистрация пройдена");
        }).fail(function() {
            alert("В процесе регистрации возникла ошибка");
        });
    });
}

function loginSubmitListener() {
    var blockLogin = $("#block-login");
    var blockProfile = $("#block-profile");
    var urlLogin = urlServer + "Token";

    blockLogin.find("input[type='submit']").click(function (e) {
        e.preventDefault();

        var loginData = {
            grant_type: "password",
            username: blockLogin.find("input[type='email']").val(),
            password: blockLogin.find("input[type='password']").val()
        };

        $.ajax({
            type: "POST",
            url: urlLogin,
            data: loginData,
        }).done(function(data) {
            // сохраняем в хранилище sessionStorage токен доступа
            sessionStorage.setItem(tokenKey, data.access_token);
            sessionStorage.setItem(usernameKey, data.userName);
            console.log(data.access_token);

            checkAuthIndex(800);
        }).fail(function() {
            alert('При логине возникла ошибка');
        });
    });
}


