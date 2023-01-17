const app = Vue.createApp({
    data() {
        return {
            list: []
        }
    },

    computed:{
        //Gets id from url
        listID(){
            return new URLSearchParams(window.location.search).get('id')
        },
    },

    methods: {
        //Fetches all book lists from our database through our restservice
        GetList(){
            const listSource = `https://openlibrary2.azurewebsites.net/api/book/${this.listID}`
            axios.get(listSource)
            .then((response)=>{
                for (book in response.data){
                    this.GetBookByIsbn(book, response.data[book].isbn)
                }
            })
        },

        //Fetches a book from openlibrary API given the books isbn 
        GetBookByIsbn(index, isbn){
            const BookSource = `https://openlibrary.org/isbn/${isbn}.json`
            axios.get(BookSource)
            .then( response =>{
                const obj = {
                    title: response.data.title,
                    author: "Unknown",
                    numberOfPages: response.data.number_of_pages
                }
                this.list.push(obj)
                this.GetAuthorName(index, response.data.authors[0].key.split("/")[2])
            })
            .catch(function(error){
                console.log(error);
            })
        },

        //Fetches an authors name given the author ID given from openlibrary API
        GetAuthorName(index, authorID){
            const AutherSource = `https://openlibrary.org/authors/${authorID}.json`
            axios.get(AutherSource)
            .then( response =>{
                  this.list[index].author = response.data.name
            })
            .catch(function(error){
                console.log(error);
            })
        },
    },
    
    //When website is "created" these methods are called
    created: function(){
        this.GetList()
    }
});
