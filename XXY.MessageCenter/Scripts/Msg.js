$.connection.hub.url = "http://localhost:10430/Msg/hubs";

var hub = $.connection.messageHub;
hub.client.NewMsg = function (msg) {

};

$.connection.hub.start();