using System.Security.Cryptography;
using GoGlobal.Test.Backend.Model;
using GoGlobal.Test.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoGlobal.Test.Backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController :ControllerBase
{
    private readonly IAuthMemoryCacheService _sessionBrain;
    public UserController(IAuthMemoryCacheService sessionBrain)
    {
        _sessionBrain = sessionBrain;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AuthUser([FromBody] User credentials)
    {
        if (string.IsNullOrEmpty(credentials.Username) || string.IsNullOrEmpty(credentials.Password))
        {
            return BadRequest(" Please fill the data");
        }
        if (credentials.Username.Equals("simpleuser") && credentials.Password.Equals("Test1234$"))
        {
            //  HttpContext.Response.Delete("gglobal-Token");
            string cookieValue = "someHashValueOfCookie";
          //  HttpContext.Response.Cookies.Append("gglobal-Token", cookieValue);
            _sessionBrain.Set(cookieValue,credentials.Username);
            return Ok(new UserAuthResponse()
            {
                AuthToken = "gglobal-Token",
                Email = "username@gmail.com",
                TokenValue = cookieValue
            });
        }
        else
        {
            return BadRequest();
        }
    }
    
    
    
}