using Microsoft.AspNetCore.Mvc;
using OpenLibraryWebService.Managers;
using OpenLibraryWebServiceLibrary.Model;
using System.Collections;

namespace OpenLibraryWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ListManager _listManager = new ListManager();

        // GET: api/<ListController>
        [HttpGet]
        public IActionResult GetAll()
        {
            //return new string[] { "value1", "value2" };
            return Ok(_listManager.GetAll());
        }

        // GET api/<ListController>/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_listManager.GetByID(id));
        }

        // POST api/<ListController>
        [HttpPost]
        public IActionResult Post([FromBody] List_Names List_Names)
        {
            return Created("https://openlibrary.azurewebsites.net/api/list", _listManager.Create(List_Names));
        }

        
    }
}
