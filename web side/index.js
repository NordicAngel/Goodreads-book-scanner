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
                listID: "",
                ISBN: ""
            },
            connectionStatus: ""
            }

        },


        methods: {
            //Finder en bog ved hjælp af et API kald ud fra dens ISBN nummer
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
                        //Gør vores oversigt i view delen er usynlig indtil der klikkes på knappen 
                        this.show = true
                    })
                    .catch(function(error){
                        console.log(error);
                    })
            },

            GetBookLists(){
                const Listsource = `https://openlibrary.azurewebsites.net/api/book`   
                
                axios.get(Listsource)



            
                    .then( response =>{
                       this.ListName = response.data;
                       console.log(response);
                       console.log(this.ListName);
                       var listLength = this.ListName.length
                       for (var i = 0; i<listLength; i++){

                        console.log(this.ListName[i].list_Name);
                       }
                    })
                    .catch(function(error){
                        console.log(error);
                    })

            },

            AddBookToList(){
                axios.post(`https://openlibrary.azurewebsites.net/api/book`, {
                    List_ID: this.addToListProps.listID,
                    isbn: this.isbn
                })

                .then((response) => {
                    console.log(response);
                    console.log(this.isbn)
                })

                .catch(function (error){
                    console.log(error);
                })
            },

                

            //Får fat i forfatterens navn ved at lave et nyt API kald med forfatter id for at få fat i navnet.
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


