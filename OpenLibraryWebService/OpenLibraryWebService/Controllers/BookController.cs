using Microsoft.AspNetCore.Mvc;
using OpenLibraryWebService.Managers;
using OpenLibraryWebServiceLibrary.Model;

namespace OpenLibraryWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager<List_Names> _bookManager = new BookManager();

        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookController>/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_bookManager.GetByID(id));
        }

        // POST api/<BookController>
        [HttpPost]

        public IActionResult Post([FromBody] List_Names list_Names)
        {
            return Ok(_bookManager.Create(list_Names));
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
