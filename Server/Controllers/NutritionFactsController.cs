using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Server.Data;
using RecipeApp.Shared;

namespace RecipeApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionFactsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public NutritionFactsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<NutritionFacts>>> GetAllNutritionFacts()
        {
            var nutritionFacts = await _dataContext.NutritionFacts.ToListAsync();
            return Ok(nutritionFacts);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<NutritionFacts>> GetNutritionFacts(int Id)
        {
            var nutritionFacts = await _dataContext.NutritionFacts.Include(x => x.Recipe).FirstOrDefaultAsync(x => x.Id == Id);

            if (nutritionFacts == null)
            {
                return new NutritionFacts();
            }

            return Ok(nutritionFacts);
        }

        [HttpGet("{RecipeId:int}/recipe")]
        public async Task<ActionResult<List<NutritionFacts>>> GetNutritionFactsByRecipe(int RecipeId)
        {
            var recipe = await _dataContext.Recipes.FindAsync(RecipeId);

            if (recipe == null)
            {
                return new List<NutritionFacts>();
            }

            return await _dataContext.NutritionFacts.Where(x => x.RecipeId == RecipeId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<NutritionFacts>> CreateNutritionFacts(NutritionFacts nutritionFacts)
        {
            nutritionFacts.Recipe = null;
            _dataContext.Add(nutritionFacts);
            await _dataContext.SaveChangesAsync();

            return Ok(nutritionFacts);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<NutritionFacts>> UpdateNutritionFacts(NutritionFacts nutritionFacts, int Id)
        {
            var nutritionFactsPut = await _dataContext.NutritionFacts.FirstOrDefaultAsync(x => x.Id == Id);

            if (nutritionFactsPut == null)
            {
                return new NutritionFacts();
            }

            nutritionFactsPut.Calories = nutritionFacts.Calories;
            nutritionFactsPut.Carbs = nutritionFacts.Carbs;
            nutritionFactsPut.Fat = nutritionFacts.Fat;
            nutritionFactsPut.Protein = nutritionFacts.Protein;
            nutritionFactsPut.RecipeId = nutritionFacts.RecipeId;

            await _dataContext.SaveChangesAsync();

            return Ok(nutritionFactsPut);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<NutritionFacts>> DeleteNutritionFacts(int Id)
        {
            var nutritionFactsDelete = await _dataContext.NutritionFacts.FirstOrDefaultAsync(x => x.Id == Id);

            if (nutritionFactsDelete == null)
            {
                return new NutritionFacts();
            }                

            _dataContext.NutritionFacts.Remove(nutritionFactsDelete);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetNutritionFactsData());
        }

        private async Task<List<NutritionFacts>> GetNutritionFactsData()
        {
            return await _dataContext.NutritionFacts.ToListAsync();
        }
    }
}
