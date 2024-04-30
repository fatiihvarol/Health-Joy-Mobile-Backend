using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Joy_Backend_Mobile.Applications.IngredientOperations.GetIngredients
{
    public class GetIngredientsQuery
    {

        private readonly AppDbContext _context;
        public GetIngredientsQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle()
        {
            try
            {
                var ingredients = await _context.Ingredients.ToListAsync();
                return new OkObjectResult(ingredients);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching ingredients: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }
}