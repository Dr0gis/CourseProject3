function listUsersQueueDisplay(listQueue, idQueue) {
    var urlResults = urlServer + "api/Results" + "?idQueue=" + idQueue;
    $.ajax({
        type: "GET",
        url: urlResults,
    }).done(function (result) {
        listQueue.empty();
        $.each(result, function(index, value) {
            var userBlock = '<div class="item">' +
                                '<div class="number">' + (index + 1) + '</div>' +
                                '<div class="email">' + value.Email + '</div>' +
                                '<div class="time-join">' + value.DateTimeRegistration + '</div>' +
                                '<div class="profile">Profile</div>' +
                            '</div>';
            listQueue.append(userBlock);
        })
    }).fail(function () {
        alert("Ошибка в получении данных пользователей очереди");
    });
}

function queueListDisplayListener() {
    var joinModal = $(".join-modal");

    $(".queue-block").find(".block").click(function() {
        var queueBlock = $(this).parent().parent();
        var listQueue = $("#" + queueBlock[0].id + " + .list-queue");
        listUsersQueueDisplay(listQueue, queueBlock[0].id);
        listQueue.slideToggle(500);
    });


    $(".queue-block").find(".join").click(function() {
        joinModal.show(500);
        joinModal.css("display", "flex");
        var queueBlock = $(this).parent().parent().parent();
        joinModal.find(".joinToQueue").attr("id_queue", queueBlock[0].id);
    });



    joinModal.find(".close").click(function() {
        joinModal.hide(500);
    });

    joinModal.find(".joinToQueue").click(function() {
        var numberCard = joinModal.find(".card").val();
        var idQueue = $(this).attr("id_queue");
        var currentdate = new Date(); 
        var datetime =  currentdate.getFullYear() + "-" + 
                        (currentdate.getMonth() + 1) + "-" +
                        currentdate.getDate() + "T" +
                        currentdate.getHours() + ":" +
                        currentdate.getMinutes() + ":" +
                        currentdate.getSeconds() + ":" +
                        currentdate.getMilliseconds();

        var urlResult = urlServer + "api/Results";

        var resultData = {
            CardRfid: {
                Id: numberCard
            },
            Queue: {
                Id: idQueue
            },
            DateTimeRegistration: datetime
        };

        $.ajax({
            type: "POST",
            url: urlResult,
            beforeSend: function (xhr) {
                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            data: resultData,
        }).done(function() {
            alert('Вы присоединились к очереди')
        }).fail(function() {
            alert('При присоединении возникла ошибка');
        });
        $(".join-modal").hide(500);
    });
}