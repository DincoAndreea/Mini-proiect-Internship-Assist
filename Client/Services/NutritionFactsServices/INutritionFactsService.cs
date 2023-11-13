using RecipeApp.Shared;
using System.Net;
using System.Threading.Tasks;

namespace RecipeApp.Client.Services.NutritionFactsServices
{
    public interface INutritionFactsService
    {
        List<NutritionFacts> NutritionFacts { get; set; }
        Task GetNutritionFacts();
        Task<NutritionFacts> GetNutritionFacts(int id);
        Task<List<NutritionFacts>> GetNutritionFactsByRecipe(int id);
        Task<NutritionFacts> CreateNutritionFacts(NutritionFacts nutritionFacts);
        Task<NutritionFacts> UpdateNutritionFacts(NutritionFacts nutritionFacts);
        Task<HttpStatusCode> DeleteNutritionFacts(int id);
    }
}
