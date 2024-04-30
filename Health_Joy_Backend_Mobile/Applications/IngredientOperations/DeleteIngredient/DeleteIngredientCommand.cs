using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Health_Joy_Backend_Mobile.Applications.IngredientOperations.DeleteIngredient
{
    public class DeleteIngredientCommand
    {
        private readonly AppDbContext _context;
        private readonly int _ingredientId;

        public DeleteIngredientCommand(AppDbContext context, int ingredientId)
        {
            _context = context;
            _ingredientId = ingredientId;
        }


        public async Task<IActionResult> Handle()
        {
            try
            {
                var ingredient = await _context.Ingredients.FindAsync(_ingredientId);
                if (ingredient == null)
                    return new NotFoundResult();

                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting ingredient: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}