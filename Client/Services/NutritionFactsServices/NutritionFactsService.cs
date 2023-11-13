using RecipeApp.Shared;
using System.Net;
using System.Net.Http.Json;

namespace RecipeApp.Client.Services.NutritionFactsServices
{
    public class NutritionFactsService : INutritionFactsService
    {
        private readonly HttpClient _httpClient;
        public List<NutritionFacts> NutritionFacts { get; set; } = new List<NutritionFacts>();
        public NutritionFactsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NutritionFacts> GetNutritionFacts(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<NutritionFacts>($"api/nutritionfacts/{id}");

            if (result != null)
            {
                return result;
            }
            throw new Exception("Nutrition facts not found.");
        }

        public async Task GetNutritionFacts()
        {
            var result = await _httpClient.GetFromJsonAsync<List<NutritionFacts>>("api/nutritionfacts");

            if (result != null)
            {
                NutritionFacts = result;
            }
        }

        public async Task<List<NutritionFacts>> GetNutritionFactsByRecipe(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<NutritionFacts>>($"api/nutritionfacts/{id}/recipe");

            return result;
        }

        public async Task<NutritionFacts> CreateNutritionFacts(NutritionFacts nutritionFacts)
        {
            var result = await _httpClient.PostAsJsonAsync("api/nutritionfacts", nutritionFacts);

            if (!result.IsSuccessStatusCode)
            {
                return new NutritionFacts();
            }

            NutritionFacts? nutritionFactsResult = await result.Content.ReadFromJsonAsync<NutritionFacts>();

            return nutritionFactsResult;
        }

        public async Task<NutritionFacts> UpdateNutritionFacts(NutritionFacts nutritionFacts)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/nutritionfacts/{nutritionFacts.Id}", nutritionFacts);

            if (!result.IsSuccessStatusCode)
            {
                return new NutritionFacts();
            }

            NutritionFacts? nutritionFactsResult = await result.Content.ReadFromJsonAsync<NutritionFacts>();

            return nutritionFactsResult;
        }

        public async Task<HttpStatusCode> DeleteNutritionFacts(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/nutritionfacts/{id}");

            if (!result.IsSuccessStatusCode)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }
    }
}
