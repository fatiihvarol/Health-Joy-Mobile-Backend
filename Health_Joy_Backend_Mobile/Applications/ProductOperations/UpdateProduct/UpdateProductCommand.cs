using Microsoft.EntityFrameworkCore;
using Health_Joy_Backend_Mobile.Common;
using Health_Joy_Mobile_Backend.Data;

namespace Health_Joy_Backend_Mobile.Applications.ProductOperations.UpdateProduct
{
    public class UpdateProductCommand
    {
        private readonly AppDbContext _context;

        public UpdateProductCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> ExecuteAsync(int productId)
        {
            // Ürünü bul
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return new ApiResponse("product not found");
            }

            product.IsApprovedByAdmin = true;

            await _context.SaveChangesAsync();

            return new ApiResponse();
        }
    }
}