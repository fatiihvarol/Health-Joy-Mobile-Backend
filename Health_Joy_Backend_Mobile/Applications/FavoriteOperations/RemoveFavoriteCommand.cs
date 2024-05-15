using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Health_Joy_Backend_Mobile.Applications.FavoriteOperations
{
    public class RemoveFavoriteCommand
    {
        private readonly AppDbContext _context;
        private readonly FavoriteRequest _favoriteRequest;

        public RemoveFavoriteCommand(AppDbContext context, FavoriteRequest favoriteRequest)
        {
            _context = context;
            _favoriteRequest = favoriteRequest;
        }

        public async Task<ApiResponse> Handle()
        {
            var user = await _context.Users.FindAsync(_favoriteRequest.UserId);
            if (user == null)
            {
                return new ApiResponse("User not found");
            }
        
            var product = await _context.Products.FindAsync(_favoriteRequest.ProductId);
            if (product == null)
            {
                return new ApiResponse("Product not found");
            }

            var userProductFavorite = await _context.UserProductFavorites
                .FirstOrDefaultAsync(upf => upf.UserId == _favoriteRequest.UserId && upf.ProductId == _favoriteRequest.ProductId);

            if (userProductFavorite == null)
            {
                return new ApiResponse("Product is not in user's favorites");
            }

            _context.UserProductFavorites.Remove(userProductFavorite);

            try
            {
                await _context.SaveChangesAsync();
                return new ApiResponse();
            }
            catch (DbUpdateException ex)
            {
                return new ApiResponse($"Failed to remove favorite: {ex.Message}");
            }
        }
    }

}
