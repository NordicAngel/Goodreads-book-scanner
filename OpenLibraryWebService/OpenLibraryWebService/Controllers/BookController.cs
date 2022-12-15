using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenLibraryWebService.Managers;
using OpenLibraryWebServiceLibrary.Model;

namespace OpenLibraryWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookManager _bookManager = new BookManager();

        // GET: api/<BookController>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAll()
        {
            return Ok(_bookManager.GetAll());
        }

        // GET api/<BookController>/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Get(int id)
        {
            return Ok(_bookManager.GetByID(id));
        }

        // POST api/<ListController>
        [HttpPost]
        [ProducesResponseType(201)]

        public IActionResult Post([FromBody] Books_In_List List_ID)
        {
            return Created("https://openlibrary.azurewebsites.net/api/book", _bookManager.Create(List_ID));
        }
    }
}
