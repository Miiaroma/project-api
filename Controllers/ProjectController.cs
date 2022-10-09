using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project_api.Models;

namespace project_api.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        public ProjectController(Database db)
        {
            Db = db;
        }

        // GET api/project
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Project(Db);
            var result = await query.GetAllAsync();
            return new OkObjectResult(result);
        }

        // GET api/project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Project(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/project
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result = await body.InsertAsync();
            //Console.WriteLine(body.date);
            if (result == 1)
            {
                return new OkObjectResult(result);
            }
            else {
                return new ConflictObjectResult(result);
            }

        }

        // PUT api/course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Project body)
        {
            await Db.Connection.OpenAsync();
            var query = new Project(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.id_project = body.id_project;
            result.pname = body.pname;
            result.place = body.place;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Project(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}