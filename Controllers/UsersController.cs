using Microsoft.AspNetCore.Mvc;

namespace ControleMercadoria.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        [HttpPost]

        public async Task<IActionResult> CreateUser()
        {
            string[] user = { "Arthur","Raul","Pele" };
            return Ok(user);      
        }

    }
}
