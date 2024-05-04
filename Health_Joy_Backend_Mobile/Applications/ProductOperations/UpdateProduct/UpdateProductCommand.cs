using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Joy_Backend_Mobile.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Schema;

namespace Health_Joy_Backend_Mobile.Applications.ProductOperations.UpdateProduct
{
    public class UpdateProductCommand
    {
        private readonly AppDbContext _context;
        private readonly ProductRequest _productRequest;
        private readonly int _productId;

        public UpdateProductCommand(AppDbContext context, ProductRequest productRequest, int productId)
        {
            _context = context;
            _productRequest = productRequest;
            _productId = productId;
        }

        public async Task<ApiResponse> Handle()
        {
            var existingProduct =
                await _context.Products
                    .Include(p => p.Ingredients)
                    .FirstOrDefaultAsync(p => p.ProductId == _productId);
            if (existingProduct == null)
            {
                return new ApiResponse("Product not found with specified barcode");
            }

            try
            {
                List<Ingredient> ingredients = new List<Ingredient>();
                double totalRiskValue = 0;
                int ingredientCount = 0;

                foreach (var ingredientRequest in _productRequest.Ingredients)
                {
                    var existingIngredient =
                        await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredientRequest.Name);
                    if (existingIngredient != null)
                    {
                        ingredients.Add(existingIngredient);
                        totalRiskValue += existingIngredient.RiskLevel;
                        ingredientCount++;
                    }
                }

                long averageRiskValue = ingredientCount > 0 ? (long)(totalRiskValue / ingredientCount) : 0;

                // Update existing Product entity with new values
                existingProduct.BarcodeNo = _productRequest.BarcodeNo;
                existingProduct.Name = _productRequest.Name;
                existingProduct.Description = _productRequest.Description;
                existingProduct.Ingredients = ingredients;
                existingProduct.ProductType = _productRequest.ProductType;
                existingProduct.UserId = _productRequest.UserId;
                existingProduct.IsApprovedByAdmin = true;
                existingProduct.TotalRiskValue = averageRiskValue;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details including inner exception
                Console.WriteLine($"Database error occurred while saving changes: {ex.InnerException?.Message}");
                return new ApiResponse("Database error occurred while saving changes:");
            }

        }
    }
}