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
                    this.list.push(this.GetBookByIsbn(response.data[book].isbn))
                }
            })
            console.log(this.list)
            
        },
        GetBookByIsbn(isbn){
            const BookSource = `https://openlibrary.org/isbn/${isbn}.json`
            console.log("isbn " + isbn)
            axios.get(BookSource)
            .then( response =>{
                const obj = {
                    title: response.data.title,
                    author: response.data.authors,
                    numberOfPages: response.data.number_of_pages
                }
                return obj
            })
            .catch(function(error){
                console.log(error);
            })
        }
        
    },

    created: function(){
        this.GetList()
    }
});

// window.app.GetList()
