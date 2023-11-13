using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Server.Data;
using RecipeApp.Shared;

namespace RecipeApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientNoteController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public IngredientNoteController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<IngredientNote>>> GetIngredientNotes()
        {
            var ingredientNote = await _dataContext.IngredientNotes.ToListAsync();
            return Ok(ingredientNote);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IngredientNote>> GetIngredientNote(int Id)
        {
            var ingredientNotes = await _dataContext.IngredientNotes.Include(x => x.Recipe).FirstOrDefaultAsync(x => x.Id == Id);

            if (ingredientNotes == null)
            {
                return new IngredientNote();
            }

            return Ok(ingredientNotes);
        }

        [HttpGet("{RecipeId:int}/recipe")]
        public async Task<ActionResult<List<IngredientNote>>> GetIngredientNotesByRecipe(int RecipeId)
        {
            var recipe = await _dataContext.Recipes.FindAsync(RecipeId);

            if (recipe == null)
            {
                return new List<IngredientNote>();
            }

            return await _dataContext.IngredientNotes.Where(x => x.RecipeId == RecipeId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IngredientNote>> CreateIngredientNote(IngredientNote ingredientNote)
        {
            ingredientNote.Recipe = null;
            _dataContext.Add(ingredientNote);
            await _dataContext.SaveChangesAsync();

            return Ok(ingredientNote);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<IngredientNote>> UpdateIngredientNote(IngredientNote ingredientNote, int Id)
        {
            var ingredientNotePut = await _dataContext.IngredientNotes.FirstOrDefaultAsync(x => x.Id == Id);

            if (ingredientNotePut == null)
            {
                return NotFound("The ingredient note doesn't exist.");
            }

            ingredientNotePut.Title = ingredientNote.Title;
            ingredientNotePut.RecipeId = ingredientNote.RecipeId;

            await _dataContext.SaveChangesAsync();

            return Ok(ingredientNotePut);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<IngredientNote>>> DeleteIngredientNote(int Id)
        {
            var ingredientNoteDelete = await _dataContext.IngredientNotes.FirstOrDefaultAsync(x => x.Id == Id);

            if (ingredientNoteDelete == null)
                return NotFound("The ingredient note doesn't exist.");

            _dataContext.IngredientNotes.Remove(ingredientNoteDelete);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetIngredientNoteData());
        }

        private async Task<List<IngredientNote>> GetIngredientNoteData()
        {
            return await _dataContext.IngredientNotes.ToListAsync();
        }
    }
}
