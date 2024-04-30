using Health_Joy_Backend_Mobile.Applications.UserOperations.AdminLogin;
using Health_Joy_Backend_Mobile.Applications.ProductOperations.UpdateProduct;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;

namespace Health_Joy_Backend_Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(AppDbContext dbcontext) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ApiResponse> LoginAdmin( string email, string password)
        {
            AdminLoginQuery adminLoginQuery = new AdminLoginQuery(dbcontext);
            return await adminLoginQuery.ExecuteAsync(email, password);

        }




    }
}