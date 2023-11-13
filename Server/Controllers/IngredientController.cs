using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Server.Data;
using RecipeApp.Shared;

namespace RecipeApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public IngredientController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> GetIngredients()
        {
            var ingredient = await _dataContext.Ingredients.ToListAsync();
            return Ok(ingredient);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int Id)
        {
            var ingredients = await _dataContext.Ingredients.Include(x => x.Recipe).Include(x => x.IngredientNote).FirstOrDefaultAsync(x => x.Id == Id);

            if (ingredients == null)
            {
                return new Ingredient();
            }

            return Ok(ingredients);
        }

        [HttpGet("{RecipeId:int}/recipe/{IngredientNoteId:int}/ingredientnote")]
        public async Task<ActionResult<List<Ingredient>>> GetIngredientsByRecipe([FromRoute]int RecipeId, [FromRoute]int IngredientNoteId)
        {
            var recipe = await _dataContext.Recipes.FindAsync(RecipeId);

            var ingredientNote = await _dataContext.IngredientNotes.FindAsync(IngredientNoteId);

            if (recipe == null || ingredientNote == null)
            {
                return new List<Ingredient>();
            }

            return await _dataContext.Ingredients.Where(x => x.RecipeId == RecipeId && x.IngredientNoteId == IngredientNoteId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Ingredient>> CreateIngredient(Ingredient ingredient)
        {
            ingredient.Recipe = null;
            ingredient.IngredientNote = null;
            _dataContext.Add(ingredient);
            await _dataContext.SaveChangesAsync();

            return Ok(ingredient);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Ingredient>> UpdateIngredient(Ingredient ingredient, int Id)
        {
            var ingredientPut = await _dataContext.Ingredients.FirstOrDefaultAsync(x => x.Id == Id);

            if (ingredientPut == null)
            {
                return NotFound("The ingredient doesn't exist.");
            }

            ingredientPut.Name = ingredient.Name;
            ingredientPut.Quantity = ingredient.Quantity;
            ingredientPut.IngredientNoteId = ingredient.IngredientNoteId;
            ingredientPut.RecipeId = ingredient.RecipeId;

            await _dataContext.SaveChangesAsync();

            return Ok(ingredientPut);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Ingredient>>> DeleteIngredient(int Id)
        {
            var ingredientDelete = await _dataContext.Ingredients.FirstOrDefaultAsync(x => x.Id == Id);

            if (ingredientDelete == null)
                return NotFound("The ingredient doesn't exist.");

            _dataContext.Ingredients.Remove(ingredientDelete);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetIngredientData());
        }

        private async Task<List<Ingredient>> GetIngredientData()
        {
            return await _dataContext.Ingredients.ToListAsync();
        }
    }
}
