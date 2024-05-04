using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.EntityFrameworkCore;


namespace Health_Joy_Backend_Mobile.Applications.ProductOperations.GetProduct
{
    public class GetAllProducts
    {
        private readonly AppDbContext _context;

        public GetAllProducts(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<ProductResponse>>> ExecuteAsync()
        {
            var products = await _context.Products
                .Include(p => p.Ingredients)
                .ToListAsync();

            if (products.Count == 0)
            {
                return new ApiResponse<List<ProductResponse>>("not found any product");
            }

            var productResponses = new List<ProductResponse>();

            foreach (var product in products)
            {
                var ingredientResponses = product.Ingredients
                    .Select(i => new IngredientResponse
                    {
                        Name = i.Name,
                        Description = i.Description,
                        RiskLevel = i.RiskLevel
                    })
                    .ToList();

                var response = new ProductResponse
                {
                    ProductId = product.ProductId,
                    BarcodeNo = product.BarcodeNo,
                    Name = product.Name,
                    Description = product.Description,
                    TotalRiskValue = product.TotalRiskValue,
                    ProductType = product.ProductType,
                    IsApprovedByAdmin = product.IsApprovedByAdmin,
                    UserId = product.UserId,
                    Ingredients = ingredientResponses
                };

                productResponses.Add(response);
            }

            return new ApiResponse<List<ProductResponse>>(productResponses);
        }
    }
}
