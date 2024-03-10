using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Backend_Mobile.UserOperations.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly AppDbContext _context;
        private readonly int _userId;
        private readonly UserRequest _userRequest;

        public UpdateUserCommand(AppDbContext context, int userId, UserRequest userRequest)
        {
            _context = context;
            _userId = userId;
            _userRequest = userRequest;
        }

        public async Task<IActionResult> Handle()
        {
            try
            {
                var user = await _context.Users.FindAsync(_userId);
                if (user == null)
                    return new NotFoundResult();

                // Update user properties with values from request
                user.FullName = _userRequest.FullName;
                user.Email = _userRequest.Email;
                user.Password = _userRequest.Password;

                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating user: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}
