using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Joy_Backend_Mobile.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        private readonly AppDbContext _context;
        private readonly UserRequest _userRequest;

        public CreateUserCommand(AppDbContext context, UserRequest userRequest)
        {
            _context = context;
            _userRequest = userRequest;
        }

        public async Task<IActionResult> Handle()
        {
            if (_userRequest.Password != _userRequest.ConfirmPassword)
            {
                return new BadRequestObjectResult(new { Message = "Passwords do not match" });
            }

            var fromDb = await _context.Users.FirstOrDefaultAsync(x => x.Email == _userRequest.Email);
            if (fromDb != null)
            {
                return new BadRequestObjectResult(new { Message = "Email already taken" });
            }

            try
            {
                var user = new User
                {
                    FullName = _userRequest.FullName,
                    Email = _userRequest.Email,
                    Password = _userRequest.Password,
                    IsActive = true
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}
