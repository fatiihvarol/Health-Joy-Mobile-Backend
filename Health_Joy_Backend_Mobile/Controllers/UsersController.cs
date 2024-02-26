using Health_Joy_Mobile_Backend.Schema;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Health_Joy_Backend_Mobile.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching users: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching user by id: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email,string password)
        {
            try
            {
                var user = await _context.Users.Where(x => x.Email == email)
                    .Where(x => x.Password == password)
                    .FirstOrDefaultAsync();
                
                if (user == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching user by id: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
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
                        ModelState.AddModelError("Message", "Passwords do not match");
                        return BadRequest(ModelState);
                    }

                    var fromDb = await _context.Users.Where(x => x.Email == userRequest.Email)
                        .FirstOrDefaultAsync();

                    if (fromDb is not null)
                    {
                        ModelState.AddModelError("Message", "Email already taken");
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

                    return Ok();
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.Error.WriteLine($"Error during registration: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound();

                user.IsActive = false;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting user: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest userRequest)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound();

                user.FullName = userRequest.FullName;
                user.Email = userRequest.Email;
                user.Password = userRequest.Password; // Şifreleme yapmadan kaydediyoruz

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating user: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
