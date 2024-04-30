using Health_Joy_Backend_Mobile.Common;
using Microsoft.EntityFrameworkCore;
using Health_Joy_Mobile_Backend.Data;

namespace Health_Joy_Backend_Mobile.Applications.UserOperations.AdminLogin
{
    public class AdminLoginQuery
    {
        private readonly AppDbContext _context;

        public AdminLoginQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> ExecuteAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return new ApiResponse("user not found");
            }

            if (user.Password != password)
            {
                return new ApiResponse("username or password wrong");
            }

            if (user.Role == "admin")
            {
                return new ApiResponse();
            }
            else
            {
                return new ApiResponse("only admins can log-in");
            }
        }
    }
}