using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Data.Entity;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Joy_Backend_Mobile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            try
            {
                var ingredients = await _context.Ingredients.ToListAsync();
                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching ingredients: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            try
            {
                var ingredient = await _context.Ingredients.FindAsync(id);
                if (ingredient == null)
                    return NotFound();

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching ingredient by id: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateIngredient(IngredientRequest request)
        {
            try
            {
                var newIngredient = new Ingredient
                {
                    Name = request.Name,
                    Description = request.Description,
                    RiskLevel = request.RiskLevel
                };

                _context.Ingredients.Add(newIngredient);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetIngredientById), new { id = newIngredient.IngredientId }, newIngredient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating ingredient: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, IngredientRequest request)
        {
            try
            {
                var ingredient = await _context.Ingredients.FindAsync(id);
                if (ingredient == null)
                    return NotFound();

                ingredient.Name = request.Name;
                ingredient.Description = request.Description;
                ingredient.RiskLevel = request.RiskLevel;

                _context.Ingredients.Update(ingredient);
                await _context.SaveChangesAsync();

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating ingredient: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            try
            {
                var ingredient = await _context.Ingredients.FindAsync(id);
                if (ingredient == null)
                    return NotFound();

                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting ingredient: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
