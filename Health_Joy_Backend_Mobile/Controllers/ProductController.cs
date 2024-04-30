using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using Health_Joy_Backend_Mobile.Applications.ProductOperations.CreateProduct;
using Health_Joy_Backend_Mobile.Applications.ProductOperations.GetProduct;

namespace Health_Joy_Backend_Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct(ProductRequest productRequest)
        {
            var createProductCommand = new CreateProductCommand(_context, productRequest);
            return await createProductCommand.Handle();
        }
        
        [HttpGet("Product")]
        public async Task<ProductResponse> GetProduct(int productBarcode)
        {
            var getProductQuery = new GetProductQuery(_context);
            return await getProductQuery.ExecuteAsync(productBarcode);
        }
    }
}