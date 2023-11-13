using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecipeApp.Client;
using RecipeApp.Client.Services.CookbookServices;
using RecipeApp.Client.Services.IngredientNoteServices;
using RecipeApp.Client.Services.IngredientServices;
using RecipeApp.Client.Services.InstructionServices;
using RecipeApp.Client.Services.NutritionFactsServices;
using RecipeApp.Client.Services.RecipeServices;
using RecipeApp.Shared;

namespace RecipeApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddScoped<ICookbookService, CookbookService>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();
            builder.Services.AddScoped<IIngredientService, IngredientService>();
            builder.Services.AddScoped<IInstructionService, InstructionService>();
            builder.Services.AddScoped<IIngredientNoteService, IngredientNoteService>();
            builder.Services.AddScoped<INutritionFactsService, NutritionFactsService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}