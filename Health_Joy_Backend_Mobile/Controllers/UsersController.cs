using Health_Joy_Mobile_Backend.Schema;
using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Health_Joy_Backend_Mobile.Applications.UserOperations.GetUsers;
using Health_Joy_Backend_Mobile.Applications.UserOperations.CreateUser;
using Health_Joy_Backend_Mobile.Applications.UserOperations.GetUserDetail;
using Health_Joy_Backend_Mobile.Applications.UserOperations.UpdateUser;
using Health_Joy_Backend_Mobile.Applications.UserOperations.DeleteUser;
using Health_Joy_Backend_Mobile.Applications.UserOperations.Login;
using Health_Joy_Backend_Mobile.Common;

namespace Health_Joy_Backend_Mobile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            /*
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return StatusCode(500, "Internal Server Error");
            }
            */
            GetUsersQuery query = new GetUsersQuery(_context);
            return await query.Handle();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            GetUserDetailQuery query = new GetUserDetailQuery(_context, id);
            return await query.Handle();
        }


        [HttpPost("Login")]
        public async Task<ApiResponse<LoginResponse>> Login(string email, string password)
        {
            LoginCommand command = new LoginCommand(_context, email, password);
            return await command.Handle();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            CreateUserCommand command = new CreateUserCommand(_context, userRequest);
            return await command.Handle();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            DeleteUserCommand command = new DeleteUserCommand(_context, id);
            return await command.Handle();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserModel model)
        {
            try
            {
                UpdateUserCommand command = new UpdateUserCommand(_context, id, model.OldPassword, model.NewPassword, model.ConfirmPassword);
                return await command.Handle();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating user: {ex}");
                return new StatusCodeResult(500);
            }
        }

    }
}
