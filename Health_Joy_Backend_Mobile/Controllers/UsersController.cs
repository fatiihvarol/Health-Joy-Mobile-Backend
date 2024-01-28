using Health_Joy_Mobile_Backend.Schema;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Mobile_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                FullName = userRequest.FullName,
                Email = userRequest.Email,
                Password = userRequest.Password,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            var userResponse = new UserResponse
            {
                FullName = user.FullName
            };

            return Ok(userResponse);
        }

        return BadRequest(ModelState);
    }
}

