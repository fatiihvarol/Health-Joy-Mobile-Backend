using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Health_Joy_Backend_Mobile.Applications.UserOperations.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly AppDbContext _context;
        private readonly int _userId;
        private readonly string _oldPassword;
        private readonly string _newPassword;
        private readonly string _confirmPassword;

        public UpdateUserCommand(AppDbContext context, int userId, string oldPassword, string newPassword, string confirmPassword)
        {
            _context = context;
            _userId = userId;
            _oldPassword = oldPassword;
            _newPassword = newPassword;
            _confirmPassword = confirmPassword;
        }

        public async Task<IActionResult> Handle()
        {
            try
            {
                var user = await _context.Users.FindAsync(_userId);
                if (user == null)
                    return new NotFoundResult();

                // Only update password if provided and matches the old password
                if (!string.IsNullOrEmpty(_newPassword) && 
                    _newPassword == _confirmPassword && 
                    _oldPassword == user.Password)
                {
                    user.Password = _newPassword;
                    await _context.SaveChangesAsync();

                    return new OkResult();
                }
                return new NotFoundResult();
                

              
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
