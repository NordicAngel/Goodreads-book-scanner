const app = Vue.createApp({
    data() {
        return {
            allData: "",
            isbn: "9780345422408",
            title: "",
            author: "",
            subjects: "",
            numberOfPages: "",
            languages: "",
            publishDate: "",
            }

        },
        methods: {
            GetBookByIsbn(){
                const BookSource = `https://openlibrary.org/isbn/${this.isbn}.json`
            
                axios.get(BookSource)

            
                    .then( response =>{
                        
                      
                        this.title = response.data.title
                        this.author = response.data.authors
                        this.subjects = response.data.subjects
                        this.numberOfPages = response.data.number_of_pages
                        this.languages = response.data.languages
                        this.publishDate = response.data.publish_date


                        console.log(this.title)
                        console.log(response)
                        
                    })
            
                    .catch(function(error){
                        console.log(error);
                    })
            
                    .then(function (){
                        
                    });


            },
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

//GetBookByIsbn()
//SetupWebSocket()

