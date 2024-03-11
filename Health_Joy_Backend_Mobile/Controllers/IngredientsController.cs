using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Joy_Backend_Mobile.IngredientOperations.GetIngredients;
using Health_Joy_Backend_Mobile.IngredientOperations.GetIngredientDetailQuery;
using Health_Joy_Backend_Mobile.IngredientOperations.CreateIngredient;
using Health_Joy_Backend_Mobile.IngredientOperations.UpdateIngredient;
using Health_Joy_Backend_Mobile.IngredientOperations.DeleteIngredient;

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
        public async Task<IActionResult> CalculateAverageRiskLevelAsync([FromBody] IngredientsListReadFromPhoto[] incomingArray)
        {
            if (incomingArray == null || incomingArray.Length == 0)
                return BadRequest("Incoming array is null or empty.");

            IngredientRequest[] ingredients = new IngredientRequest[incomingArray.Length];
            IngredientRequest tempIngredient = new IngredientRequest();
            int totalRiskLevel = 0;
            double avarageTotalRiskLevel = 0;
            int ingredientNotInTheDatabaseCounter = 0; //database de kaydı olmayan ingredient'lerin ortalama hesaplarken dahil edilmemesi için

            for (int i = 0; i < incomingArray.Length; i++)
            {
                var element = await _context.Ingredients.FirstOrDefaultAsync(x => x.Name == incomingArray[i].Name);
                if (element is not null)
                {
                    //buraya ihtiyaç olmayabilir 
                    //sadece ortalma değere ihtiyacımız olursa 
                    //bu kısmı kaldırıp sayıları toplayıp ortalama hesaplat
                    Console.WriteLine(i + ". ingredient, name:  " + element.Name + " risk level: " + element.RiskLevel);
                    tempIngredient.Name = element.Name;
                    tempIngredient.RiskLevel = element.RiskLevel;
                    ingredients[i] = tempIngredient;
                    totalRiskLevel += element.RiskLevel;
                }
                else
                {
                    //eğer database'e kayıtlı olmayan bir dataysa
                    Console.WriteLine(i + ". ingredient: " + incomingArray[i].Name + " is not found in the database.");
                    ingredientNotInTheDatabaseCounter++;
                }
            }

            if (totalRiskLevel is not 0)
            {
                avarageTotalRiskLevel = (double)(totalRiskLevel / (incomingArray.Length - ingredientNotInTheDatabaseCounter));
                return Ok(" Total: " + totalRiskLevel + " Avarage: " + avarageTotalRiskLevel);
            }
            else
                return BadRequest("No ingredient is registered in the database");
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