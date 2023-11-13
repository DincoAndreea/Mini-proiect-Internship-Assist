using RecipeApp.Shared;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace RecipeApp.Client.Services.CookbookServices
{
    public class CookbookService : ICookbookService
    {
        private readonly HttpClient _httpClient;
        public List<Cookbook> Cookbooks { get; set; } = new List<Cookbook>();
        public CookbookService(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }
        
        public async Task<Cookbook> GetCookbook(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Cookbook>($"api/cookbook/{id}");

            if (result != null)
            {
                return result;
            }
            throw new Exception("Cookbook not found.");
        }

        public async Task GetCookbooks()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Cookbook>>("api/cookbook");

            if (result != null)
            {
                Cookbooks = result;
            }
        }

        public async Task<Cookbook> CreateCookbook(Cookbook cookbook)
        {
            var result = await _httpClient.PostAsJsonAsync("api/cookbook", cookbook);

            if(!result.IsSuccessStatusCode)
            {
                return new Cookbook();
            }

            Cookbook? cookbookResult = await result.Content.ReadFromJsonAsync<Cookbook>();

            return cookbookResult;
        }

        public async Task<HttpStatusCode> UpdateCookbook(Cookbook cookbook)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/cookbook/{cookbook.Id}", cookbook);

            if (!result.IsSuccessStatusCode)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }

        public async Task<List<Cookbook>> DeleteCookbook(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/cookbook/{id}");

            if (!result.IsSuccessStatusCode)
            {
                return new List<Cookbook>();
            }

            return await result.Content.ReadFromJsonAsync<List<Cookbook>>();
        }
    }
}
