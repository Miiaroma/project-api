using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using project_api.Models;

namespace project_api.Controllers
{
    [Route("api/[controller]")]
    public class HourController : ControllerBase
    {
         public HourController(Database db)
        {
            Db = db;
        }

        // GET api/Hour
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Hour(Db);
            var result = await query.GetAllAsync();
            //Console.WriteLine("Test");
            return new OkObjectResult(result);
        }

        // GET api/Hour/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Hour(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Hour
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Hour body)
        {
            await Db.Connection.OpenAsync();          
            body.Db = Db;
            int result=await body.InsertAsync();
            Console.WriteLine("inserted id="+result);
            if(result == 0){
                return new ConflictObjectResult(0);
            }
            return new OkObjectResult(result);
        }
        

       // PUT api/Hour/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Hour body)
        {
            Console.WriteLine("update called");
            await Db.Connection.OpenAsync();
            Console.WriteLine(body);
            var query = new Hour(Db);         
    
            var result = await query.FindOneAsync(id);
            result.id_project=body.id_project;
            result.id_person=body.id_person;
            result.work_hour=body.work_hour;            

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

        // DELETE api/Hour/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Hour(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}