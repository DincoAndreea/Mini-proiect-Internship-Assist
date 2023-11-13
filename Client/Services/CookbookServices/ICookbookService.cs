using RecipeApp.Shared;
using System.Net;

namespace RecipeApp.Client.Services.CookbookServices
{
    public interface ICookbookService
    {
        List<Cookbook> Cookbooks { get; set; }
        Task GetCookbooks(); 
        Task<Cookbook> GetCookbook(int id);
        Task<Cookbook> CreateCookbook(Cookbook cookbook);
        Task<HttpStatusCode> UpdateCookbook(Cookbook cookbook);
        Task<List<Cookbook>> DeleteCookbook(int id);
        
    }
}
