const app = Vue.createApp({
    data() {
        return {
            }

        }
    },
);

function SetupWebSocket(){

    let ip = "ws://10.200.178.183:12000"

    var ws = new WebSocket(ip)

    ws.onopen = function(){
        alert("Connection is open")
    }

    ws.onmessage = function(evt){
        console.log(`isbn is: ${evt.data}`)
        
    }

    while (true){
        console.log(ws.readyState)
    }
}

SetupWebSocket()

