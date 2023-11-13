using RecipeApp.Shared;
using System.Net;

namespace RecipeApp.Client.Services.IngredientNoteServices
{
    public interface IIngredientNoteService
    {
        List<IngredientNote> IngredientNotes { get; set; }
        Task<IngredientNote> GetIngredientNote(int id);
        Task GetIngredientNotes();
        Task<List<IngredientNote>> GetIngredientNotesByRecipe(int id);
        Task<IngredientNote> CreateIngredientNote(IngredientNote ingredientNote);
        Task<IngredientNote> UpdateIngredientNote(IngredientNote ingredientNote);
        Task<HttpStatusCode> DeleteIngredientNote(int id);
    }
}
