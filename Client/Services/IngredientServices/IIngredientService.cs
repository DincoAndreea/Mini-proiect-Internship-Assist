using RecipeApp.Shared;
using System.Net;

namespace RecipeApp.Client.Services.IngredientServices
{
    public interface IIngredientService
    {
        List<Ingredient> Ingredients { get; set; }
        Task<Ingredient> GetIngredient(int id);
        Task GetIngredients();
        Task<List<Ingredient>> GetIngredientsByRecipeAndIngredientNote(int idRecipe, int idIngredientNote);
        Task<Ingredient> CreateIngredient(Ingredient ingredient);
        Task<Ingredient> UpdateIngredient(Ingredient ingredient);
        Task<HttpStatusCode> DeleteIngredient(int id);
    }
}
