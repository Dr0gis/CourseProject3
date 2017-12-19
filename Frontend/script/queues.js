function queuesParseJson(queues) {
    var sectionQueues = $(".section-queues");
    var blocks = sectionQueues.find(".blocks");

    blocks.empty();
    var dayBlock = '<div class="day-block">' +
                        '<div class="container">' +
                            '<div class="block">' +
                                '<div class="day-week">Friday</div>' +
                                '<div class="number">05</div>' +
                                '<div class="month">January</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>';
    blocks.append(dayBlock);

    $.each(queues, function (index, value) {
        var classType = "";
        if (value.Event.Type == "Football") {
            classType = "event-football";
        }
        else if (value.Event.Type == "Cybersport") {
            classType = "event-cybersport";
        }
        else if (value.Event.Type == "Concert") {
            classType = "event-concert";
        }
        else if (value.Event.Type == "Conference") {
            classType = "event-conference";
        }

        var queueBlock =    '<div class="queue-block ' + classType + '" id="' + value.Id + '">' +
                                '<div class="container">' +
                                    '<div class="block">' +
                                        '<div class="name">' + value.Name + '</div>' +
                                        '<div class="union">' +
                                            '<div class="event">' + value.Event.Name + '</div>' +
                                            '<div class="organization">' + value.Event.Organization.Name + '</div>' +
                                        '</div>' +
                                        '<div class="description">' + value.Description + '</div>' +
                                        '<div class="join">Join</div>' +
                                    '</div>' +
                                '</div>' +
                            '</div>';
        blocks.append(queueBlock);

        var listQueue = '<div class="list-queue">' +
                            '<div class="container">' +
                                '<div class="title">' +
                                    '<div class="number">Number</div>' +
                                    '<div class="email">Email</div>' +
                                    '<div class="time-join">Join time</div>' +
                                    '<div class="profile">Profile</div>' +
                                '</div>' +
                                '<div class="item">' +
                                    '<div class="number">1</div>' +
                                    '<div class="email">User 1</div>' +
                                    '<div class="time-join">04.01.2018 12:53</div>' +
                                    '<div class="profile">Profile</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>';
        blocks.append(listQueue);
    });
    queueListDisplayListener();
}

function getQueues() {
    var urlQueues = urlServer + "api/Queues/All";

    $.ajax({
        type: "GET",
        url: urlQueues,
    }).done(function (result) {
        queuesParseJson(result);
    }).fail(function () {
        alert("Ошибка в получении данных очереди");
    });
}