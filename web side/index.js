const app = Vue.createApp({
    data() {
        return {
            }

        },
        methods:{
          openSocket(){
            SetupWebSocket()
          }  
        }
    },
);

function SetupWebSocket(){

    let ip = "ws://localhost:12000"

    var ws = new WebSocket(ip)

    ws.onopen = function(){
        alert("Connection is open")
    }

    ws.onmessage = function(evt){
        console.log(`isbn is: ${evt.data}`)
        
    }

    // while (true){
    //     console.log(ws.readyState)
    // }
}


