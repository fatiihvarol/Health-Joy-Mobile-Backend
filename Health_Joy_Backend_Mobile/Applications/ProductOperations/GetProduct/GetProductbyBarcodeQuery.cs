using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Health_Joy_Backend_Mobile.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Health_Joy_Backend_Mobile.Applications.ProductOperations.GetProduct
{
    public class GetProductbyBarcodeQuery
    {
        private readonly AppDbContext _context;

        public GetProductbyBarcodeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ProductResponse>> ExecuteAsync(string barcodeNo)
        {
            var product = await _context.Products
                .Include(p => p.Ingredients) // Ingredients'ı da Include et
                .Where(p => p.BarcodeNo.Equals(barcodeNo))
                .FirstOrDefaultAsync();

            if (product is null)
            {
                return new ApiResponse<ProductResponse>("not found");
            }

            // Ingredients'ı IngredientResponse listesine dönüştür
            var ingredientResponses = product.Ingredients
                .Select(i => new IngredientResponse
                {
                    Name = i.Name,
                    Description = i.Description,
                    RiskLevel = i.RiskLevel
                })
                .ToList();

            // ProductResponse oluştur
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

            return new ApiResponse<ProductResponse>(response);
        }
    }
}