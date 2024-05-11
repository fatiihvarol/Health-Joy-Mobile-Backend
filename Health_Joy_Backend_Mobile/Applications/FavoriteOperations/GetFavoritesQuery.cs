using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.EntityFrameworkCore;


namespace Health_Joy_Backend_Mobile.Applications.FavoriteOperations
{
    public class GetUserFavorites
    {
        private readonly AppDbContext _context;
        private readonly int _userId;

        public GetUserFavorites(AppDbContext context, int userId)
        {
            _context = context;
            _userId = userId;
        }

        public async Task<ApiResponse<List<ProductResponse>>> ExecuteAsync()
        {
            try
            {
                // Retrieve the user by UserId including their favorite products
                var user = await _context.Users
                    .Include(u => u.Favorites) // Include the FavoriteProducts collection
                        .ThenInclude(fp => fp.Ingredients) // Include ingredients for each favorite product if needed
                    .FirstOrDefaultAsync(u => u.UserId == _userId);

                if (user == null || user.Favorites == null || !user.Favorites.Any())
                {
                    return new ApiResponse<List<ProductResponse>>("No favorite products found for the user");
                }

                // Map the user's favorite products to ProductResponse objects
                var favoriteProducts = user.Favorites.Select(fp => new ProductResponse
                {
                    ProductId = fp.ProductId,
                    BarcodeNo = fp.BarcodeNo,
                    Name = fp.Name,
                    Description = fp.Description,
                    TotalRiskValue = fp.TotalRiskValue,
                    ProductType = fp.ProductType,
                    IsApprovedByAdmin = fp.IsApprovedByAdmin,
                    UserId = fp.UserId,
                    Ingredients = fp.Ingredients
                        .Select(ingredient => new IngredientResponse
                        {
                            Name = ingredient.Name,
                            Description = ingredient.Description,
                            RiskLevel = ingredient.RiskLevel
                        })
                        .ToList()
                }).ToList();

                return new ApiResponse<List<ProductResponse>>(favoriteProducts);
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions here
                return new ApiResponse<List<ProductResponse>>(ex.Message);
            }
        }
    }
}
