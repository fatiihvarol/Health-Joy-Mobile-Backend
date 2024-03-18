using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Joy_Backend_Mobile.IngredientOperations.GetIngredients;
using Health_Joy_Backend_Mobile.IngredientOperations.GetIngredientDetailQuery;
using Health_Joy_Backend_Mobile.IngredientOperations.CreateIngredient;
using Health_Joy_Backend_Mobile.IngredientOperations.UpdateIngredient;
using Health_Joy_Backend_Mobile.IngredientOperations.DeleteIngredient;
using Health_Joy_Mobile_Backend.Data.Entity;
using Microsoft.IdentityModel.Tokens;

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
            GetIngredientsQuery query = new GetIngredientsQuery(_context);
            return await query.Handle();
        }


        [HttpPost("CalculateAverageRiskLevel")]
        public async Task<IActionResult> CalculateAverageRiskLevelAsync([FromBody] string[] incomingArray)
        {
            try
            {
                if (incomingArray == null || incomingArray.Length == 0)
                    return BadRequest("Incoming array is null or empty.");

                List<Ingredient> ingredients = new List<Ingredient>();
                int totalRiskLevel = 0;
                double avarageTotalRiskLevel = 0;
                int ingredientNotInTheDatabaseCounter = 0;

                for (int i = 0; i < incomingArray.Length; i++)
                {
                    var element = await _context.Ingredients.FirstOrDefaultAsync(x => x.Name == incomingArray[i]);

                    if (element is not null)
                    {
                        ingredients.Add(element);
                        totalRiskLevel += element.RiskLevel;
                    }
                    else
                    {
                        ingredientNotInTheDatabaseCounter++;
                    }

                }
                if (!ingredients.IsNullOrEmpty())
                {
                    if (ingredients.Count != 0)
                    {
                        avarageTotalRiskLevel = Math.Round((double)totalRiskLevel / ingredients.Count, 3);
                    }
                    else
                    {
                        avarageTotalRiskLevel = 0;
                    }

                    ProductIngredientResponse response = new ProductIngredientResponse();
                    response.Ing = ingredients;
                    response.AverageRiskLevel = avarageTotalRiskLevel;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("No ingredient is registered in the database");
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            GetIngredientDetailQuery query = new GetIngredientDetailQuery(_context, id);
            return await query.Handle();
        }


        [HttpPost]
        public async Task<IActionResult> CreateIngredient(IngredientRequest request)
        {
            CreateIngredientCommand command = new CreateIngredientCommand(_context, request);
            return await command.Handle();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, IngredientRequest request)
        {
            UpdateIngredientCommand command = new UpdateIngredientCommand(_context, id, request);
            return await command.Handle();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            DeleteIngredientCommand command = new DeleteIngredientCommand(_context, id);
            return await command.Handle();
        }
    }
}