using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using project_api.Models;

namespace project_api.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
         public PersonController(Database db)
        {
            Db = db;
        }

        // GET api/Person
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Person(Db);
            var result = await query.GetAllAsync();
            Console.WriteLine("Test");
            return new OkObjectResult(result);
        }

        // GET api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Person(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Person
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person body)
        {
            await Db.Connection.OpenAsync();   
            body.password=BCrypt.Net.BCrypt.HashPassword(body.password);         
            body.Db = Db;
            Console.WriteLine(body.id_person);
            int result=await body.InsertAsync();
            Console.WriteLine("inserted id="+result);
            if(result == 0){
                return new ConflictObjectResult(0);
            }
            return new OkObjectResult(result);
        }

       // PUT api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Person body)
        {
            Console.WriteLine("update called");
            await Db.Connection.OpenAsync();
            Console.WriteLine(body);
            var query = new Person(Db);
            body.password = BCrypt.Net.BCrypt.HashPassword(body.password);
    
            var result = await query.FindOneAsync(id);
            result.firstname=body.firstname;
            result.lastname=body.lastname;
            result.city=body.city;
            result.birth_year=body.birth_year;
            result.salary=body.salary;
            result.password=body.password;

            if (result is null)
                return new NotFoundResult();
            int updateTest = await result.UpdateAsync();
            if (updateTest == 0)
            {
                return new BadRequestResult();
            }
            else
            {
                return new OkObjectResult(updateTest);
            }
        }

        // DELETE api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Person(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}