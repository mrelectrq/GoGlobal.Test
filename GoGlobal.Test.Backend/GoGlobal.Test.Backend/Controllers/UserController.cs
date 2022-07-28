using System.Security.Cryptography;
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
    public async Task<ActionResult<string>> AuthUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return BadRequest(" Please fill the data");
        }
        if (username.Equals("user") && password.Equals("user"))
        {
            HttpContext.Response.Cookies.Delete("gglobal-Token");
            string cookieValue = "someHashValueOfCookie";
            HttpContext.Response.Cookies.Append("gglobal-Token", cookieValue);
            _sessionBrain.Set(cookieValue,username);
            return Ok("username@gmail.com");
        }
        else
        {
            return BadRequest();
        }
    }
    
    
    
}