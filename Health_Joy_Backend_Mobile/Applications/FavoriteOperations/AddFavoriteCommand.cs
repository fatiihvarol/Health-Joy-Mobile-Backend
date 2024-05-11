using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Health_Joy_Mobile_Backend.Data.Entity;

namespace Health_Joy_Backend_Mobile.Applications.FavoriteOperations
{
    public class AddFavoriteCommand
    {
        private readonly AppDbContext _context;
        private readonly FavoriteRequest _favoriteRequest;

        public AddFavoriteCommand(AppDbContext context, FavoriteRequest favoriteRequest)
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

            // Check if the user's Favorites collection is initialized
            if (user.Favorites is null)
            {
                // Initialize the Favorites collection if it's null (assuming Favorites is a navigation property)
                user.Favorites = new List<Product>();
            }

            // Check if the product is already in user's favorites
            var isAlreadyFavorite = user.Favorites.Any(f => f.ProductId == product.ProductId);
            if (isAlreadyFavorite)
            {
                return new ApiResponse("Product already in favorites");
            }

            // Add the product to user's favorites
            user.Favorites.Add(product);

            try
            {
                await _context.SaveChangesAsync();
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions (e.g., database update failure)
                return new ApiResponse($"Failed to add favorite: {ex.Message}");
            }
        }
    }
}
