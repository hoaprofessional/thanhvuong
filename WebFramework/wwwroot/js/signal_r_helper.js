
function async(your_function, callback) {
    setTimeout(function () {
        your_function();
        if (callback) { callback(); }
    }, 0);
}

class SignalRHelper {
    constructor(signalrclass, group) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("/" + signalrclass)
            .build();
        this.connection.start().catch(err => console.error(err.toString()));
        if (group != null) {
            this.connection.on("HubConnected", () => {
                this.joinGroup(group);
            });
        }
    }


    joinGroup(group) {
        this.connection.invoke("JoinGroup", group).catch(err => console.error(err));
    }

    /**
     * on receive text
     * @param {any} messageCallback : function(text){}  text : text receive
     */
    onReceiveText(messageCallback) {
        this.connection.on("ReceiveText", (text) => {
            if (messageCallback != null) {
                messageCallback(text);
            }
        });
    }

    sendText(text) {
        this.connection.invoke("BroadcastText", text).catch(function(err) 
        {
            console.error(err)
        });
    }
}