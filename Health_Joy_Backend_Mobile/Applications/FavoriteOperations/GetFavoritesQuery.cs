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
                var userFavorites = await _context.UserProductFavorites
                    .Where(upf => upf.UserId == _userId)
                    .Include(upf => upf.Product) // Include the Product
                        .ThenInclude(p => p.Ingredients) // Include ingredients for each product if needed
                    .ToListAsync();

                if (userFavorites == null || !userFavorites.Any())
                {
                    return new ApiResponse<List<ProductResponse>>("No favorite products found for the user");
                }

                // Map the user's favorite products to ProductResponse objects
                var favoriteProducts = userFavorites.Select(upf => new ProductResponse
                {
                    ProductId = upf.Product.ProductId,
                    BarcodeNo = upf.Product.BarcodeNo,
                    Name = upf.Product.Name,
                    Description = upf.Product.Description,
                    TotalRiskValue = upf.Product.TotalRiskValue,
                    ProductType = upf.Product.ProductType,
                    IsApprovedByAdmin = upf.Product.IsApprovedByAdmin,
                    Ingredients = upf.Product.Ingredients
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
