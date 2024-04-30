using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Handle()
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == _email && x.Password == _password);

                if (user == null)
                    return new NotFoundResult();

                // Burada başarılı bir giriş olduğunu işaretlemek için başka bir şey döndürebiliriz(TOKEN)

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching user by id: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}
