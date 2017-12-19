function checkAuth() {
    return sessionStorage.getItem(tokenKey) != undefined;
}

function checkAuthIndex(animation_duration) {
    var blockProfile = $("#block-profile");
    var blockLogin = $("#block-login");
    var blockRegistration = $("#block-registration");

    if (checkAuth()) {
        $("#login").text(sessionStorage.getItem(usernameKey));
        // Add query for user info and write to filds

        if (blockLogin.css("display") == "none" && blockRegistration.css("display") == "none") {
            blockProfile.css("display", "block");
        }

        if (blockLogin.css("display") != "none") {
            blockLogin.fadeOut(animation_duration, function () {
                blockProfile.fadeIn(animation_duration / 2);
            });
        }
        if (blockRegistration.css("display") != "none") {
            blockRegistration.fadeOut(animation_duration, function () {
                blockProfile.fadeIn(animation_duration / 2);
            });
        }
    }
    else {
        blockRegistration.css("display", "block");
    }
}

function displayListEvents(events) {
    var otherEventsBlock = $(".event-organization").find(".other");
    var listEventsBlock = otherEventsBlock.find(".list");
    listEventsBlock.empty();

    var queueSelect = $(".queue-organization-modal").find(".event");

    $.each(events, function(index, value) {
        var itemBlock = '<div class="item"><div class="title">' + value.Name + '</div><div class="line"><div class="remove button" id_event="' + value.Id + '">Remove</div></div</div>'
        listEventsBlock.append(itemBlock);
        var option = '<option value="' + value.Id + '">' + value.Name + '</option>';
        queueSelect.append(option);
    });

    //TODO: Button listener
}

function displayListQueues(queues) {
    var otherQueuesBlock = $(".queue-organization").find(".other");
    var listQueuesBlock = otherQueuesBlock.find(".list");
    listQueuesBlock.empty();

    $.each(queues, function(index, value) {
        var itemBlock = '<div class="item"><div class="title">' + value.Name + '</div><div class="line"><div class="remove button" id_queue="' + value.Id + '">Edit</div></div</div>'
        listQueuesBlock.append(itemBlock);
    });

    openCardsListListener();
    //TODO: Button listener
}

function actionIfOrgnizationExist(organization) {
    var organizationBlock = $(".organization");
    var currentOrganizationBLock = organizationBlock.find(".current");

    currentOrganizationBLock.css("display", "block");
    organizationBlock.find(".other").css("display", "none");

    currentOrganizationBLock.find(".title").text(organization.Name);
    currentOrganizationBLock.find(".description").text(organization.Description);

    var eventsOrganizationBLock = $(".event-organization");
    eventsOrganizationBLock.css("display", "block");

    var queueorganizationBLock = $(".queue-organization");
    queueorganizationBLock.css("display", "block");

    var urlGetEvents = urlServer + "api/Events";
    $.ajax({
        type: "GET",
        url: urlGetEvents,
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).done(function(result) {
        displayListEvents(result);
    }).fail(function() {
        alert("Информация об организациях не получена");
    });

    var urlGetQueues = urlServer + "api/Queues";
    $.ajax({
        type: "GET",
        url: urlGetQueues,
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).done(function(result) {
        displayListQueues(result);
    }).fail(function() {
        alert("Информация об организациях не получена");
    });
}

function displayListOrganization(organizations) {
    var otherOrganizationBlock = $(".organization").find(".other");
    var listOrganizationsBlock = otherOrganizationBlock.find(".list");
    listOrganizationsBlock.empty();

    $.each(organizations, function(index, value) {
        var itemBlock = '<div class="item"><div class="title">' + value.Name + '</div><div class="join button" id_organization="' + value.Id + '">Join</div></div>'
        listOrganizationsBlock.append(itemBlock);
    });

    joinOrganizationListener();
}

function actionIfOrgnizationNotExist() {
    var organizationBlock = $(".organization");
    organizationBlock.find(".current").css("display", "none");
    organizationBlock.find(".other").css("display", "block");

    var urlGetOrganizations = urlServer + "api/Organizations";
    $.ajax({
        type: "GET",
        url: urlGetOrganizations,
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).done(function(result) {
        displayListOrganization(result);
    }).fail(function() {
        alert("Информация об организациях не получена");
    });

    var eventsOrganizationBLock = $(".event-organization");
    eventsOrganizationBLock.css("display", "none");

    var queueorganizationBLock = $(".queue-organization");
    queueorganizationBLock.css("display", "none");
}

function actionIfAdministrator() {
    var blockProfile = $("#block-profile");
    blockProfile.find(".status").find(".value").text("Administrator");
    $("#become-admin").css("display", "none");

    $(".organization").css("display", "block");

    var urlGetOrganization = urlServer + "api/Administrators/AdministratorInfo";
    $.ajax({
        type: "GET",
        url: urlGetOrganization,
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).done(function(result) {
        var organization = result.Organization;
        if (organization == null) {
            actionIfOrgnizationNotExist()
        }
        else {
            actionIfOrgnizationExist(organization)
        }
    }).fail(function() {
        alert("Информация об организации не получена");
    });
}

function actionIfUser() {
    var blockProfile = $("#block-profile");
    blockProfile.find(".status").find(".value").text("User");
    $("#become-admin").css("display", "block");

    $(".organization").css("display", "none");
    $(".event-organization").css("display", "none");
    $(".queue-organization").css("display", "none");
    $(".queue-current").find(".button").css("display", "none");
}

function checkAuthProfile(animation_duration) {
    var urlGetUserUnfo = urlServer + "api/Account/UserInfo";
    var blockProfile = $("#block-profile");

    if (checkAuth()) {
        $.ajax({
            type: "GET",
            url: urlGetUserUnfo,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            }
        }).done(function(result) {
            var userEmail = result.Email;
            var isAdministrator = result.IsAdministrator;
            blockProfile.find("#login").text(userEmail);
            if (isAdministrator) {
                actionIfAdministrator();
            }
            else {
                actionIfUser();
            }
            
        }).fail(function() {
            alert("Информация о пользователе не получена");
        });
    }
    else {
        url = "index.html";
        $(location).attr("href", url);
    }
}