using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using project_api.Models;

namespace project_api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public LoginController(Database db)
        {
            Db = db;
        }


        // POST api/login
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person body)
        {
            Console.WriteLine(body.id_person);
            Console.WriteLine(body.password);
            await Db.Connection.OpenAsync();
            var query = new Login(Db);
            var result = await query.GetPassword(body.id_person);
   
            if (result is null || ! BCrypt.Net.BCrypt.Verify(body.password, result))
            {
                // authentication failed
                return new OkObjectResult(false);
            }
            else
            {
                // authentication successful
                return new OkObjectResult(true);
                Singleton singObject=Singleton.Instance;
                singObject.Idperson=body.id_person;
                singObject.Password=body.password;
            }
            
        }  

        public Database Db { get; }
    }
}