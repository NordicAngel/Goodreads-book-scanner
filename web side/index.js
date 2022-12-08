const app = Vue.createApp({
    data() {
        return {
            isbn: "",
            title: "",
            author: "",
            subjects: "",
            numberOfPages: "",
            languages: "",
            publishDate: "",
            authorID: "",
            authorTrimmed: "",
            personalAuthorName: "",
            languageTrimmed:"",
            show: false,
            workTrimmed: "",
            listName: "",
            addToListProps: {
                listID: 0,
                ISBN: ""
            },
            connectionStatus: ""
            }
        },

        //When side is "created" these methods are called
        created: function(){
            this.GetBookLists()
        },
        
        methods: {
            //Fetches specific book from the openlibrary API given the books isbn
            GetBookByIsbn(){
                const BookSource = `https://openlibrary.org/isbn/${this.isbn}.json`
                axios.get(BookSource)
                .then( response =>{
                    this.title = response.data.title
                    this.author = response.data.authors
                    this.subjects = response.data.subjects
                    if (response.data.number_of_pages != null){
                        this.numberOfPages = response.data.number_of_pages
                    }
                    else if (response.data.pagination != null){
                        this.numberOfPages = response.data.pagination
                    }
                    this.languages = response.data.languages[0].key
                    this.publishDate = response.data.publish_date
                    this.authorID = this.author[0].key
                    this.languageTrimmed = this.languages.substring(11, this.languages.length)
                    this.authorTrimmed = this.authorID.substring(9, this.authorID.length)
                    this.workTrimmed = response.data.works[0].key.split("/")[2]

                    this.GetAuthorName()
                    //Makes elements in the view visible only after the first book is scanned
                    this.show = true
                })
                .catch(function(error){
                    console.log(error);
                })
            },

            //Fetches all lists from our database through our restservice
            GetBookLists(){
                const Listsource = `https://openlibrary.azurewebsites.net/api/list`   
                axios.get(Listsource)
                .then( response =>{
                    this.ListName = response.data;
                })
                .catch(function(error){
                    console.log(error);
                })
            },

            //Adds a book to list in our database through our restservice
            AddBookToList(){
                axios.post(`https://openlibrary.azurewebsites.net/api/book`, {
                    List_ID: this.addToListProps.listID,
                    isbn: this.isbn
                })
                .catch(function (error){
                    console.log(error);
                })
            },

            //Fetches the authors from the openlibrary API given the author name ID from openlibrary
            GetAuthorName(){
                const AutherSource = `https://openlibrary.org/authors/${this.authorTrimmed}.json`
                axios.get(AutherSource)
                .then( response =>{
                      this.personalAuthorName = response.data.name
                })
                .catch(function(error){
                    console.log(error);
                })
            },

            //Fetches work from the openlibrary API given the work ID from openlibrary
            GetWorkById(){
                const WorkSource = `https://openlibrary.org/works/${this.workTrimmed}.json`
                axios.get(WorkSource)
                .then( response =>{
                      this.personalAuthorName = response.data.name
                })
                .catch(function(error){
                    console.log(error);
                })
            },

            //Opens websocket connection between raspberry pi and website and sends information from the 
            //barcode scanner through this connection
            OpenWebSocket(){
                let ip = "ws://192.168.14.102:12000"
                // Showing popup
                document.getElementById('scanpopup').classList.remove("hide")
                this.connectionStatus = "Establishing connection ..."
                // Trying to open webSocket
                var ws = new WebSocket(ip)
                // On succesful opening changes popup text 
                ws.onopen = () => {
                    this.connectionStatus = "Ready for scanning ..."
                }
                // On incoming message insert isbn and searches for a book
                ws.onmessage = (evt) => {
                    this.isbn = evt.data
                    this.GetBookByIsbn()
                }
                // On closing connection hides popup
                ws.onclose = () => {
                document.getElementById('scanpopup').classList.add("hide")
                    this.connectionStatus = ""
                }
                // On error print console
                ws.onerror = (evt) => {
                    console.log(evt)
                }
            }
        }
    },
);


