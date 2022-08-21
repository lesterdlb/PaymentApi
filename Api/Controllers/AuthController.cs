using Api.Authentication;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthController(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("login")]
    public IActionResult Login(User request)
    {
        var users = new List<User>
        {
            new() { Email = "admin@mail.com", Password = "Password123" },
            new() { Email = "frodo@mail.com", Password = "TheOneRing" },
        };

        var user = users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);

        if (user is null) return Unauthorized();

        var token = _tokenGenerator.GenerateToken(user);
        return Ok(new
        {
            user.Id,
            user.Email,
            Token = token
        });
    }
}