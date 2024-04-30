using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Joy_Backend_Mobile.Applications.ProductOperations.GetProduct
{
    public class GetProductQuery
    {
        private readonly AppDbContext _context;

        public GetProductQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> ExecuteAsync(int barcodeNo)
        {
            var product = await _context.Products
                .Include(p => p.Ingredients) // Ingredients'ı da Include et
                .FirstOrDefaultAsync(p => p.BarcodeNo == barcodeNo);

            if (product == null)
            {
                return null;
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

            return response;
        }
    }
}