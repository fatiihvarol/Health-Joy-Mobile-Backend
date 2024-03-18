using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Backend_Mobile.IngredientOperations.CreateIngredient
{
    public class CreateIngredientCommand
    {
        private readonly AppDbContext _context;
        private readonly IngredientRequest _ingredientRequest;


        public CreateIngredientCommand(AppDbContext context, IngredientRequest ingredientRequest)
        {
            _context = context;
            _ingredientRequest = ingredientRequest;
        }


        public async Task<IActionResult> Handle()
        {
            try
            {
                var newIngredient = new Ingredient
                {
                    Name = _ingredientRequest.Name.ToLower(),
                    Description = _ingredientRequest.Description.ToLower(),
                    RiskLevel = _ingredientRequest.RiskLevel
                };
                _context.Ingredients.Add(newIngredient);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating ingredient: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}