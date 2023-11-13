using RecipeApp.Shared;
using System.Net;
using System.Net.Http.Json;

namespace RecipeApp.Client.Services.IngredientServices
{
    public class IngredientService : IIngredientService
    {
        private readonly HttpClient _httpClient;
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public IngredientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Ingredient> GetIngredient(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Ingredient>($"api/ingredient/{id}");

            if (result != null)
            {
                return result;
            }
            throw new Exception("Ingredient not found.");
        }

        public async Task GetIngredients()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Ingredient>>("api/ingredient");

            if (result != null)
            {
                Ingredients = result;
            }
        }

        public async Task<List<Ingredient>> GetIngredientsByRecipeAndIngredientNote(int idRecipe, int idIngredientNote)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Ingredient>>($"api/ingredient/{idRecipe}/recipe/{idIngredientNote}/ingredientNote");

            return result;
        }

        public async Task<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            var result = await _httpClient.PostAsJsonAsync("api/ingredient", ingredient);

            if (!result.IsSuccessStatusCode)
            {
                return new Ingredient();
            }

            Ingredient? ingredientResult = await result.Content.ReadFromJsonAsync<Ingredient>();

            return ingredientResult;
        }

        public async Task<Ingredient> UpdateIngredient(Ingredient ingredient)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/ingredient/{ingredient.Id}", ingredient);

            if (!result.IsSuccessStatusCode)
            {
                return new Ingredient();
            }

            Ingredient? ingredientResult = await result.Content.ReadFromJsonAsync<Ingredient>();

            return ingredientResult;
        }

        public async Task<HttpStatusCode> DeleteIngredient(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/ingredient/{id}");

            if (!result.IsSuccessStatusCode)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }
    }
}
