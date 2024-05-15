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

            var isAlreadyFavorite = await _context.UserProductFavorites
                .AnyAsync(upf => upf.UserId == _favoriteRequest.UserId && upf.ProductId == _favoriteRequest.ProductId);

            if (isAlreadyFavorite)
            {
                return new ApiResponse("Product already in favorites");
            }

            var userProductFavorite = new UserProductFavorite
            {
                UserId = _favoriteRequest.UserId,
                ProductId = _favoriteRequest.ProductId
            };

            _context.UserProductFavorites.Add(userProductFavorite);

            try
            {
                await _context.SaveChangesAsync();
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Failed to add favorite: {ex.Message}");
            }
        }
    }

}
