using RecipeApp.Shared;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace RecipeApp.Client.Services.RecipeServices
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public List<Recipe> RecipesByCookbook { get; set; } = new List<Recipe>();

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Recipe> GetRecipe(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Recipe>($"api/recipe/{id}");

            if (result != null)
            {
                return result;
            }
            throw new Exception("Recipe not found.");
        }

        public async Task GetRecipes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Recipe>>("api/recipe");

            if (result != null)
            {
                Recipes = result;
            }
        }

        public async Task<List<Recipe>> GetRecipesByCookbook(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Recipe>>($"api/recipe/{id}/cookbook");

            return result;
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            var result = await _httpClient.PostAsJsonAsync("api/recipe", recipe);

            if (!result.IsSuccessStatusCode)
            {
                return new Recipe();
            }

            Recipe? recipeResult = await result.Content.ReadFromJsonAsync<Recipe>();

            return recipeResult;
        }

        public async Task<Recipe> UpdateRecipe(Recipe recipe)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/recipe/{recipe.Id}", recipe);

            if (!result.IsSuccessStatusCode)
            {
                return new Recipe();
            }

            Recipe? recipeResult = await result.Content.ReadFromJsonAsync<Recipe>();

            return recipeResult;
        }

        public async Task<List<Recipe>> DeleteRecipe(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/recipe/{id}");

            if (!result.IsSuccessStatusCode)
            {
                return new List<Recipe>();
            }

            return await result.Content.ReadFromJsonAsync<List<Recipe>>();
        }

    }
}
