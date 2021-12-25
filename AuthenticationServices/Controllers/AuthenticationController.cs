using AuthenticationService.Models;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        public IActionResult Authenticate(User user)
        {
            var u = new UserRepository().GetUser(user.Username);
            if (u == null)
            {
                return NotFound("username not found"); 
            }
            bool credentials = u.Password.Equals(user.Password);
            if (!credentials)
            {
                return Forbid("The username/password combination is wrong."); 
            }

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
          
            response.Content = new StringContent(TokenManager.GenerateToken(user.Username));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); 
            return new OkObjectResult(TokenManager.GenerateToken(user.Username));
        }

        [HttpGet]
        public IActionResult Validate(string token, string username)
        {
            bool exists = new UserRepository().GetUser(username) != null;
            if (!exists)
            {
                return NotFound("username not found");
            }
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
                return Ok();

            return BadRequest("The username / password combination is wrong");
        }
    }
}
