using RecipeApp.Shared;
using System.Net;
using System.Net.Http.Json;

namespace RecipeApp.Client.Services.IngredientNoteServices
{
    public class IngredientNoteService : IIngredientNoteService
    {
        private readonly HttpClient _httpClient;
        public List<IngredientNote> IngredientNotes { get; set; } = new List<IngredientNote>();
        public IngredientNoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IngredientNote> GetIngredientNote(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<IngredientNote>($"api/ingredientnote/{id}");

            if (result != null)
            {
                return result;
            }
            throw new Exception("Ingredient note not found.");
        }

        public async Task GetIngredientNotes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<IngredientNote>>("api/ingredientnote");

            if (result != null)
            {
                IngredientNotes = result;
            }
        }

        public async Task<List<IngredientNote>> GetIngredientNotesByRecipe(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<IngredientNote>>($"api/ingredientnote/{id}/recipe");

            return result;
        }

        public async Task<IngredientNote> CreateIngredientNote(IngredientNote ingredientNote)
        {
            var result = await _httpClient.PostAsJsonAsync("api/ingredientnote", ingredientNote);

            if (!result.IsSuccessStatusCode)
            {
                return new IngredientNote();
            }

            IngredientNote? ingredientNoteResult = await result.Content.ReadFromJsonAsync<IngredientNote>();

            return ingredientNoteResult;
        }

        public async Task<IngredientNote> UpdateIngredientNote(IngredientNote ingredientNote)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/ingredientnote/{ingredientNote.Id}", ingredientNote);

            if (!result.IsSuccessStatusCode)
            {
                return new IngredientNote();
            }

            IngredientNote? ingredientNoteResult = await result.Content.ReadFromJsonAsync<IngredientNote>();

            return ingredientNoteResult;
        }

        public async Task<HttpStatusCode> DeleteIngredientNote(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/ingredientnote/{id}");

            if (!result.IsSuccessStatusCode)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }
    }
}
