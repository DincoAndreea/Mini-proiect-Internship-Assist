using RecipeApp.Shared;
using System.Net;
using System.Net.Http.Json;

namespace RecipeApp.Client.Services.InstructionServices
{
    public class InstructionService : IInstructionService
    {
        private readonly HttpClient _httpClient;
        public List<Instruction> Instructions { get; set; } = new List<Instruction>();
        public InstructionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Instruction> GetInstruction(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Instruction>($"api/instruction/{id}");

            if (result != null)
            {
                return result;
            }
            throw new Exception("Instruction not found.");
        }

        public async Task GetInstructions()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Instruction>>("api/instruction");

            if (result != null)
            {
                Instructions = result;
            }
        }

        public async Task<List<Instruction>> GetInstructionsByRecipe(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Instruction>>($"api/instruction/{id}/recipe");

            return result;
        }

        public async Task<Instruction> CreateInstruction(Instruction instruction)
        {
            var result = await _httpClient.PostAsJsonAsync("api/instruction", instruction);

            if (!result.IsSuccessStatusCode)
            {
                return new Instruction();
            }

            Instruction? instructionResult = await result.Content.ReadFromJsonAsync<Instruction>();

            return instructionResult;
        }

        public async Task<Instruction> UpdateInstruction(Instruction instruction)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/instruction/{instruction.Id}", instruction);

            if (!result.IsSuccessStatusCode)
            {
                return new Instruction();
            }

            Instruction? instructionResult = await result.Content.ReadFromJsonAsync<Instruction>();

            return instructionResult;
        }

        public async Task<HttpStatusCode> DeleteInstruction(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/instruction/{id}");

            if (!result.IsSuccessStatusCode)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }
    }
}
