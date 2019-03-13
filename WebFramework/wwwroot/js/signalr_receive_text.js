

var qoutationGroup = new SignalRHelper("notificationHub", "room1");

//signalrRoom1.joinGroup("room1");
//signalrRoom2.joinGroup("room2");

qoutationGroup.onReceiveText(function (text) {
    alert(text);
});