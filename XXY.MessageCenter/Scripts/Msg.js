$.connection.hub.url = "http://localhost:10430/Msg/hubs";

var hub = $.connection.messageHub;
hub.client.NewMsg = function (msg) {
    $("#msgMenu #msgLabel").text("New").parent().css("color", "orangeRed");
    $("<li class='new'><a href='#'>" + msg.Subject + "</li>").insertBefore("#msgMenu ul li:first");
    $("#msgMenu .divider").removeClass("hidden");

    var news = $("#msgMenu .new");
    if (news.length > 5) {
        var m;
        for (var i = 5; m = news[i]; i++) {
            $(m).remove();
        }
    }
};

$.connection.hub.start();