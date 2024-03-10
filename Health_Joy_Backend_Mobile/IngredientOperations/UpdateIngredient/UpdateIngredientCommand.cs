using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Backend_Mobile.IngredientOperations.UpdateIngredient
{
    public class UpdateIngredientCommand
    {
        private readonly AppDbContext _context;
        private readonly IngredientRequest _ingredientRequest;
        private readonly int _ingredientId;

        public UpdateIngredientCommand(AppDbContext context, int ingredientId, IngredientRequest ingredientRequest)
        {
            _context = context;
            _ingredientId = ingredientId;
            _ingredientRequest = ingredientRequest;
        }


        public async Task<IActionResult> Handle()
        {
            try
            {
                var ingredient = await _context.Ingredients.FindAsync(_ingredientId);
                if (ingredient == null)
                    return new NotFoundResult();

                ingredient.Name = _ingredientRequest.Name;
                ingredient.Description = _ingredientRequest.Description;
                ingredient.RiskLevel = _ingredientRequest.RiskLevel;

                _context.Ingredients.Update(ingredient);
                await _context.SaveChangesAsync();

                return new OkObjectResult(ingredient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating ingredient: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}