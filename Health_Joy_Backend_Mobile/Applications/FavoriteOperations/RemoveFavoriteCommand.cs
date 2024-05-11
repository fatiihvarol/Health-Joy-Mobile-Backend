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
            // Find the user by UserId
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == _favoriteRequest.UserId);

            if (user == null)
            {
                return new ApiResponse("User not found");
            }
            
            // Find the product by ProductId
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == _favoriteRequest.ProductId);

            if (product == null)
            {
                return new ApiResponse("Product not found");
            }

            // Check if the product exists in user's favorites
            var favoriteToRemove = user.Favorites.FirstOrDefault(f => f.ProductId == _favoriteRequest.ProductId);
            if (favoriteToRemove == null)
            {
                return new ApiResponse("Product is not in user's favorites");
            }

            // Remove the product from user's favorites
            user.Favorites.Remove(favoriteToRemove);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
                return new ApiResponse();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception
                return new ApiResponse($"Failed to remove favorite: {ex.Message}");
            }
        }
    }
}
