using RecipeApp.Shared;
using System.Net;

namespace RecipeApp.Client.Services.RecipeServices
{
    public interface IRecipeService
    {
        List<Recipe> Recipes { get; set; }
        List<Recipe> RecipesByCookbook { get; set; }
        Task GetRecipes();
        Task<Recipe> GetRecipe(int id);
        Task<List<Recipe>> GetRecipesByCookbook(int id);
        Task<Recipe> CreateRecipe(Recipe recipe);
        Task<Recipe> UpdateRecipe(Recipe recipe);
        Task<List<Recipe>> DeleteRecipe(int id);
    }
}
