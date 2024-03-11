using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Backend_Mobile.UserOperations.DeleteUser
{
    public class DeleteUserCommand
    {

        private readonly AppDbContext _context;
        private readonly int _userId;

        public DeleteUserCommand(AppDbContext context, int userId)
        {
            _context = context;
            _userId = userId;
        }

        public async Task<IActionResult> Handle()
        {
            try
            {
                var user = await _context.Users.FindAsync(_userId);
                if (user == null)
                    return new NotFoundResult();

                //user.IsActive = false;
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting user: {ex}");
                return new StatusCodeResult(500);
            }
        }

    }
}
