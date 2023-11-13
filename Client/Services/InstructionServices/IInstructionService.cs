using RecipeApp.Shared;
using System.Net;

namespace RecipeApp.Client.Services.InstructionServices
{
    public interface IInstructionService
    {
        List<Instruction> Instructions { get; set; }
        Task<Instruction> GetInstruction(int id);
        Task GetInstructions();
        Task<List<Instruction>> GetInstructionsByRecipe(int id);   
        Task<Instruction> CreateInstruction(Instruction instruction);
        Task<Instruction> UpdateInstruction(Instruction instruction);
        Task<HttpStatusCode> DeleteInstruction(int id);
    }
}
