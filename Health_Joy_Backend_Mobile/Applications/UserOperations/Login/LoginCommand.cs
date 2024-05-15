using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Joy_Backend_Mobile.Applications.UserOperations.Login
{
    public class LoginCommand
    {
        private readonly AppDbContext _context;
        private readonly string _email;
        private readonly string _password;

        public LoginCommand(AppDbContext context, string email, string password)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _email = email;
            _password = password;
        }

        public async Task<ApiResponse<LoginResponse>> Handle()
        {
            LoginResponse response = new LoginResponse();
            var user = await _context.Users
                .Include(u => u.Favorites) // Eager loading of Favorites
                .FirstOrDefaultAsync(x => x.Email == _email && x.Password == _password);

            if (user == null)
                return new ApiResponse<LoginResponse>("not found"); // User not found

            response.Email = user.Email;
            response.UserId = user.UserId;
            response.UserName = user.FullName;

            if (user.Favorites != null && user.Favorites.Any())
            {
                int[] favorites = user.Favorites.Select(f => f.ProductId).ToArray();
                response.Favorites = favorites;
            }
            else
            {
                response.Favorites = Array.Empty<int>(); // Handle if there are no favorites
            }

            return new ApiResponse<LoginResponse>(response);
        }

    }
}