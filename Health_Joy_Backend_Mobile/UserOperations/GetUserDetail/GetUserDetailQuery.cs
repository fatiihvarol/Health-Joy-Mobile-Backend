using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Backend_Mobile.UserOperations.GetUserDetail
{
    public class GetUserDetailQuery
    {
        private readonly AppDbContext _context;
        private readonly int _userId;

        public GetUserDetailQuery(AppDbContext context, int userId)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userId = userId;
        }

        public async Task<IActionResult> Handle()
        {
            try
            {
                var user = await _context.Users.FindAsync(_userId);
                if (user == null)
                    return new NotFoundResult();

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching user by id: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}
