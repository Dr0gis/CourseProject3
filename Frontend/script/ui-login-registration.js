function changeLoginRegistarionListener()
{
    var blockRegistration = $("#block-registration");
    var blockLogin = $("#block-login");

    blockRegistration.find("a").click(function () {
        blockRegistration.fadeOut(500, function () {
            blockLogin.fadeIn(800);
        });
    });

    blockLogin.find("a").click(function () {
        blockLogin.fadeOut(500, function () {
            blockRegistration.fadeIn(800);
        });
    });
}

function logoutSubmitListener() {
    var blockProfile = $("#block-profile");
    var blockLogin = $("#block-login");

    blockProfile.find("input[type='submit']").click(function (e) {
        e.preventDefault();
        sessionStorage.removeItem(tokenKey);
        blockProfile.fadeOut(500, function () {
            blockLogin.fadeIn(800);
        });
    });
}