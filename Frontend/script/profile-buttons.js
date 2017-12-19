function becomeAdministratorListener() {
    var urlAdministrator = urlServer + "api/Administrators";

    $("#become-admin").click(function () {
        $.ajax({
            type: "POST",
            url: urlAdministrator,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            }
        }).done(function() {
            alert("Вы стали админиcтратором");
            actionIfAdministrator();
        }).fail(function() {
            alert("Произошла ошибка, когда вы попытались стать администратором");
        });
    });
}

function openCreateOrganizationListener() {
    $(".organization").find(".create").click(function () {
        $(".organization-modal").show(500);
        $(".organization-modal").css("display", "flex");
    });

    $(".organization-modal").find(".close").click(function () {
        $(".organization-modal").hide(500);
    });
}

function createOrganizationListener() {
    var urlOrganization = urlServer + "api/Organizations";
    var organizationModalBlock = $(".organization-modal");

    organizationModalBlock.find(".create").click(function () {
        var name = organizationModalBlock.find(".name").val();
        var description = organizationModalBlock.find(".description").val();

        if (name == "" || description == "") {
            alert("Введите данные");
            return;
        }

        var inputData = {
            Name: name,
            Description: description
        };

        $.ajax({
            type: "POST",
            url: urlOrganization,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            data: inputData
        }).done(function() {
            organizationModalBlock.hide(500);
            actionIfAdministrator();
        }).fail(function() {
            alert("Произошла ошибка, когда вы попытались добавить организацию");
        });
    });
}

function openCreateEventListener() {
    $(".event-organization").find(".add").click(function () {
        $(".event-modal").show(500);
        $(".event-modal").css("display", "flex");
    });

    $(".event-modal").find(".close").click(function () {
        $(".event-modal").hide(500);
    });
}

function createEventListener() {
    var urlEvent = urlServer + "api/Events";
    var eventModalBlock = $(".event-modal");

    eventModalBlock.find(".add").click(function () {
        var name = eventModalBlock.find(".name").val();
        var description = eventModalBlock.find(".description").val();
        var type = eventModalBlock.find(".type").val();

        if (name == "" || description == "" || type == null) {
            alert("Введите данные");
            return;
        }

        var inputData = {
            Name: name,
            Type: type,
            Description: description
        };

        $.ajax({
            type: "POST",
            url: urlEvent,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            data: inputData
        }).done(function() {
            eventModalBlock.hide(500);
            actionIfAdministrator();
        }).fail(function() {
            alert("Произошла ошибка, когда вы попытались добавить событие");
        });
    });
}

function openCreateQueueOrganizationListener() {
    var queueOrganizationBlock =  $(".queue-organization-modal");

    $(".queue-organization").find(".add").click(function () {
        queueOrganizationBlock.show(500);
        queueOrganizationBlock.css("display", "flex");
    });

    queueOrganizationBlock.find(".close").click(function () {
        queueOrganizationBlock.hide(500);
    });
}

function createQueueOrganizationListener() {
    var urlQueue = urlServer + "api/Queues";
    var queueModalBlock = $(".queue-organization-modal");

    queueModalBlock.find(".add").click(function () {
        var name = queueModalBlock.find(".name").val();
        var description = queueModalBlock.find(".description").val();
        var event = queueModalBlock.find(".event").val();

        if (name == "" || description == "" || event == null) {
            alert("Введите данные");
            return;
        }

        var inputData = {
            Name: name,
            Event: {
                Id: event
            },
            Description: description
        };

        $.ajax({
            type: "POST",
            url: urlQueue,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            data: inputData
        }).done(function() {
            queueModalBlock.hide(500);
            actionIfAdministrator();
        }).fail(function() {
            alert("Произошла ошибка, когда вы попытались добавить событие");
        });
    });
}

function leaveOrganizationListener() {
    var urlOrganization = urlServer + "api/Administrators/Organization";

    $(".organization").find(".leave").click(function () {

        $.ajax({
            type: "DELETE",
            url: urlOrganization,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            }
        }).done(function() {
            actionIfAdministrator();
        }).fail(function() {
            alert("Произошла ошибка, когда вы попытались покинуть организацию");
        });
    });
}

function joinOrganizationListener() {
    var urlOrganization = urlServer + "api/Administrators/AddOrganization";

    $(".organization").find(".join").click(function () {

        var inputData = {
            Id: $(this).attr("id_organization")
        };

        $.ajax({
            type: "POST",
            url: urlOrganization,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            data: inputData
        }).done(function() {
            actionIfAdministrator();
        }).fail(function() {
            alert("Произошла ошибка, когда вы попытались присоединиться организации");
        });
    });
}

function displayListCards(cards, idQueue) {
    var cardsBlock = $(".cards-queue-modal");

    cardsBlock.find(".add").attr("id_queue", idQueue);

    var listCardsBlock = cardsBlock.find(".list");
    listCardsBlock.empty();
    
    listCardsBlock.append('<tr class="item"><th> Number </th><th> Uid </th><th> Edit </th></tr>');

    $.each(cards, function(index, value) {
        var itemBlock = '<tr class="item"><td class="number">' + value.Id + '</td><td class="uid">' + value.Uid + '</td><td class="edit" id_card="' + value.Id + '">Edit</td></tr>'
        listCardsBlock.append(itemBlock);
    });

    //TODO: Button listener
}

function openCardsListListener() {
    var queueOrganizationBlock = $(".queue-organization");
    var queueOrganizationModalBlock = $(".cards-queue-modal");
    queueOrganizationBlock.find(".item").find(".title").click(function () {
        queueOrganizationModalBlock.show(500);
        queueOrganizationModalBlock.css("display", "flex");

        var idQueue = $(this).parent().find(".remove").attr("id_queue");
        var urlGetCards = urlServer + "api/CardRFIDs" + "?idQueue=" + idQueue;
        $.ajax({
            type: "GET",
            url: urlGetCards,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            }
        }).done(function(result) {
            displayListCards(result, idQueue);
        }).fail(function() {
            alert("Информация о картах не получена");
        });
    });

    queueOrganizationModalBlock.find(".close").click(function () {
        queueOrganizationModalBlock.hide(500);
    });
}

function displayInputAddCardsListener() {
    var cardsQueueModalBlock = $(".cards-queue-modal");
    var addData = cardsQueueModalBlock.find(".add-data");

    cardsQueueModalBlock.find(".add").click(function () {
        var uid = addData.find(".uid").val();
        var idQueue = $(this).attr("id_queue");
        
        if (uid == "") {
            //alert("Введите данные полностью");
            addData.fadeToggle(500);
            return;
        }

        var data = {
            Uid: uid,
            Queue: {
                Id: idQueue
            }
        }

        var urlCards = urlServer + "api/CardRFIDs";
        $.ajax({
            type: "POST",
            url: urlCards,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            data: data
        }).done(function(result) {
            var urlGetCards = urlServer + "api/CardRFIDs" + "?idQueue=" + idQueue;
            $.ajax({
                type: "GET",
                url: urlGetCards,
                beforeSend: function (xhr) {
                    var token = sessionStorage.getItem(tokenKey);
                    xhr.setRequestHeader("Authorization", "Bearer " + token);
                }
            }).done(function(result) {
                displayListCards(result, idQueue);
            }).fail(function() {
                alert("Информация о картах не получена");
            });
            addData.fadeToggle(500);
            addData.find(".uid").val("");
        }).fail(function() {
            alert("При попытке создать карту, произошла ошибка");
        });
        
    });
}