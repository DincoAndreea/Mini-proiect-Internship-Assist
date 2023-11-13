using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Server.Data;
using RecipeApp.Shared;

namespace RecipeApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RecipeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetRecipes()
        {
            var recipes = await _dataContext.Recipes.ToListAsync();
            return Ok(recipes);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int Id)
        {
            var recipe = await _dataContext.Recipes.Include(x => x.Cookbook).FirstOrDefaultAsync(x => x.Id == Id);

            if (recipe == null)
            {
                return new Recipe();
            }

            return Ok(recipe);
        }

        [HttpGet("{CookbookId:int}/cookbook")]
        public async Task<ActionResult<List<Recipe>>> GetRecipesByCookbook(int CookbookId)
        {
            var cookbook = await _dataContext.Cookbooks.FindAsync(CookbookId);

            if (cookbook == null)
            {
                return new List<Recipe>();
            }

            return await _dataContext.Recipes.Where(x => x.CookbookId == CookbookId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Cookbook>> CreateRecipe(Recipe recipe)
        {
            _dataContext.Add(recipe);
            await _dataContext.SaveChangesAsync();

            return Ok(recipe);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Recipe>> UpdateRecipe(Recipe recipe, int Id)
        {
            var recipePut = await _dataContext.Recipes.FirstOrDefaultAsync(x => x.Id == Id);

            if (recipePut == null)
            {
                return new Recipe();
            }

            recipePut.Title = recipe.Title;
            recipePut.Description = recipe.Description;
            recipePut.PrepTime = recipe.PrepTime;
            recipePut.CookTime = recipe.CookTime;
            recipePut.AdditionalTime = recipe.AdditionalTime;
            recipePut.TotalTime = recipe.TotalTime;
            recipePut.Image = recipe.Image;
            recipePut.Servings = recipe.Servings;
            recipePut.Yield = recipe.Yield;
            recipePut.Note = recipe.Note;
            recipePut.CookbookId = recipe.CookbookId;

            await _dataContext.SaveChangesAsync();

            return Ok(recipePut);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Recipe>> DeleteRecipe(int Id)
        {
            var recipeDelete = await _dataContext.Recipes.FirstOrDefaultAsync(x => x.Id == Id);

            if (recipeDelete == null)
            {
                return new Recipe();
            }               

            _dataContext.Recipes.Remove(recipeDelete);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetRecipeData());
        }

        private async Task<List<Recipe>> GetRecipeData()
        {
            return await _dataContext.Recipes.ToListAsync();
        }
    }
}
