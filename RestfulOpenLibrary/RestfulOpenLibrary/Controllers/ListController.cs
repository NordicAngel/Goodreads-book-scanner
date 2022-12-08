﻿using Microsoft.AspNetCore.Mvc;
using RestfulLibrary.Model;
using RestfulOpenLibrary.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestfulOpenLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {

        private readonly ListManager listmanager = new ListManager("Server=tcp:datamatiker-daniel.database.windows.net,1433;Initial Catalog=OpenLibrary;Persist Security Info=False;User ID=DanielAdmin;Password=AdminDaniel1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        // GET: api/<ListController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(listmanager.GetAll());
        }

        // GET api/<ListController>/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetByID(int id)
        {
            return Ok(listmanager.GetById(id));
        }

        // POST api/<ListController>
        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {

            return Created("https://openlibrary.azurewebsites.net/api/list",listmanager.AddList(name));
        }

        // PUT api/<ListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        


    }
}
