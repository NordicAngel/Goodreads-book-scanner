const app = Vue.createApp({
    data() {
        return {
            list: []
        }
    },
    computed:{
        listID(){
            return new URLSearchParams(window.location.search).get('id')
        },
       
        
    },
    methods: {
        GetList(){
            const listSource = `https://openlibrary.azurewebsites.net/api/book/${this.listID}`
            console.log(listSource)
            axios.get(listSource)
            .then((response)=>{
                //this.list = response.data
                console.log(response.data)
                for (book in response.data){
                    console.log(book)
                    this.GetBookByIsbn(book, response.data[book].isbn)
                }
                console.log(this.list)
            })
            
        },
        GetBookByIsbn(index, isbn){
            const BookSource = `https://openlibrary.org/isbn/${isbn}.json`
            console.log("isbn " + isbn)
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
        GetAuthorName(index, authorID){
            const AutherSource = `https://openlibrary.org/authors/${authorID}.json`
            console.log(authorID)
            axios.get(AutherSource)
            .then( response =>{
                  this.list[index].author = response.data.name
            })
            .catch(function(error){
                console.log(error);
            })
        },
    },

    created: function(){
        this.GetList()
    }
});

// window.app.GetList()
