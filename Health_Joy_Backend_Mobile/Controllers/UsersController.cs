using Health_Joy_Mobile_Backend.Schema;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace Health_Joy_Mobile_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            try
            {
                // Log received user request
                Console.WriteLine($"Registration Request: {JsonSerializer.Serialize(userRequest)}");

                if (ModelState.IsValid)
                {
                    if (userRequest.Password != userRequest.ConfirmPassword)
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                        return BadRequest(ModelState);
                    }

                    var user = new User
                    {
                        FullName = userRequest.FullName,
                        Email = userRequest.Email,
                        Password = userRequest.Password, // Şifreleme yapmadan kaydediyoruz
                        IsActive = true
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    var userResponse = new UserResponse
                    {
                        FullName = user.FullName
                    };

                    // Log successful registration
                    Console.WriteLine($"Registration successful for user: {user.FullName}");

                    return Ok(userResponse);
                }

                // Log ModelState errors
                Console.WriteLine($"Registration failed due to ModelState errors: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.Error.WriteLine($"Error during registration: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
