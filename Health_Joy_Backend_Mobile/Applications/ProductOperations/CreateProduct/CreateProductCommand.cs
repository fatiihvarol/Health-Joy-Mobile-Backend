using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Schema;

namespace Health_Joy_Backend_Mobile.Applications.ProductOperations.CreateProduct
{
    public class CreateProductCommand
    {
        private readonly AppDbContext _context;
        private readonly ProductRequest _productRequest;

        public CreateProductCommand(AppDbContext context, ProductRequest productRequest)
        {
            _context = context;
            _productRequest = productRequest;
        }

       public async Task<IActionResult> Handle()
{
    if (_productRequest == null || string.IsNullOrEmpty(_productRequest.Name) || string.IsNullOrEmpty(_productRequest.Description) || _productRequest.Ingredients == null || _productRequest.Ingredients.Count == 0)
    {
        return new BadRequestObjectResult(new { Message = "ProductRequest is incomplete or invalid." });
    }

    // Check if product with the same barcode already exists
    var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.BarcodeNo == _productRequest.BarcodeNo);
    if (existingProduct != null)
    {
        return new BadRequestObjectResult(new { Message = "Product with the same barcode already exists." });
    }

    try
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        double totalRiskValue = 0;
        int ingredientCount = 0;

        foreach (var ingredientRequest in _productRequest.Ingredients)
        {
            // Retrieve existing ingredient from the database based on its name
            var existingIngredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredientRequest.Name);
            if (existingIngredient != null)
            {
                // Use the existing ingredient instance
                ingredients.Add(existingIngredient);
                totalRiskValue += existingIngredient.RiskLevel;
                ingredientCount++;
            }
        }

        long averageRiskValue = ingredientCount > 0 ? (long)(totalRiskValue / ingredientCount) : 0;

        // Create new Product entity
        var newProduct = new Product
        {
            BarcodeNo = _productRequest.BarcodeNo,
            Name = _productRequest.Name,
            Description = _productRequest.Description,
            Ingredients = ingredients,
            ProductType = _productRequest.ProductType,
            UserId = _productRequest.UserId,
            IsApprovedByAdmin = false,
            TotalRiskValue = averageRiskValue
        };

        // Add new product to the database
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();

        return new OkResult();
    }
    catch (DbUpdateException ex)
    {
        // Log the exception details including inner exception
        Console.WriteLine($"Database error occurred while saving changes: {ex.InnerException?.Message}");

        return new ObjectResult(new { Message = "An error occurred while saving changes to the database. See the inner exception for details." })
        {
            StatusCode = 500 // Set the HTTP status code to 500 (Internal Server Error)
        };
    }
    catch (Exception ex)
    {
        // Handle any other unexpected exception
        Console.WriteLine($"An unexpected error occurred while creating the product: {ex.Message}");

        return new ObjectResult(new { Message = "An unexpected error occurred while processing the request." })
        {
            StatusCode = 500 // Set the HTTP status code to 500 (Internal Server Error)
        };
    }
}

    }
}
