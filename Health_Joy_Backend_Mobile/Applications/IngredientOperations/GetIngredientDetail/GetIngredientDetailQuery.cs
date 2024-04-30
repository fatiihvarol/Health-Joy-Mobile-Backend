using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Joy_Backend_Mobile.Applications.IngredientOperations.GetIngredientDetailQuery
{
    public class GetIngredientDetailQuery
    {

        private readonly AppDbContext _context;
        private readonly int _ingredientId;
        public GetIngredientDetailQuery(AppDbContext context, int ingredientId)
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

                return new OkObjectResult(ingredient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching ingredient by id: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}