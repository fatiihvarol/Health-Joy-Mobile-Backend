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
    
    [HttpPost("Add")]
    public async Task<ApiResponse> AddFavorite(FavoriteRequest request)
    {
        AddFavoriteCommand command = new AddFavoriteCommand(_context, request);
        return await command.Handle();
    }
    
    [HttpPost("Remove")]
    public async Task<ApiResponse> RemoveFavorite(FavoriteRequest request)
    {
        RemoveFavoriteCommand command = new RemoveFavoriteCommand(_context, request);
        return await command.Handle();
    }
    
    [HttpPost("{id}")]
    public async Task<ApiResponse<List<ProductResponse>>> GetFavorites(int id)
    {
        GetUserFavorites command = new GetUserFavorites(_context, id);
        return await command.ExecuteAsync();
    }
    
}