using Health_Joy_Backend_Mobile.Applications.FavoriteOperations;
using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Backend_Mobile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly AppDbContext _context;

    public FavoriteController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<ApiResponse> CreateIngredient(FavoriteRequest request)
    {
        AddFavoriteCommand command = new AddFavoriteCommand(_context, request);
        return await command.Handle();
    }
    
}