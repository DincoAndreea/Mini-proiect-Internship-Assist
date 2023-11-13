using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Server.Data;
using RecipeApp.Shared;

namespace RecipeApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructionController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public InstructionController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Instruction>>> GetInstructions()
        {
            var instruction = await _dataContext.Instructions.ToListAsync();
            return Ok(instruction);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Instruction>> GetInstruction(int Id)
        {
            var instructions = await _dataContext.Instructions.Include(x => x.Recipe).FirstOrDefaultAsync(x => x.Id == Id);

            if (instructions == null)
            {
                return new Instruction();
            }

            return Ok(instructions);
        }

        [HttpGet("{RecipeId:int}/recipe")]
        public async Task<ActionResult<List<Instruction>>> GetInstructionsByRecipe(int RecipeId)
        {
            var recipe = await _dataContext.Recipes.FindAsync(RecipeId);

            if (recipe == null)
            {
                return new List<Instruction>();
            }

            return await _dataContext.Instructions.Where(x => x.RecipeId == RecipeId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Instruction>> CreateInstruction(Instruction instruction)
        {
            instruction.Recipe = null;
            _dataContext.Add(instruction);
            await _dataContext.SaveChangesAsync();

            return Ok(instruction);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Instruction>> UpdateInstruction(Instruction instruction, int Id)
        {
            var instructionPut = await _dataContext.Instructions.FirstOrDefaultAsync(x => x.Id == Id);

            if (instructionPut == null)
            {
                return new Instruction();
            }

            instructionPut.Title = instruction.Title;
            instructionPut.RecipeId = instruction.RecipeId;

            await _dataContext.SaveChangesAsync();

            return Ok(instructionPut);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Instruction>>> DeleteInstruction(int Id)
        {
            var instructionDelete = await _dataContext.Instructions.FirstOrDefaultAsync(x => x.Id == Id);

            if (instructionDelete == null)
            {
                return new List<Instruction>();
            }
                
            _dataContext.Instructions.Remove(instructionDelete);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetInstructionData());
        }

        private async Task<List<Instruction>> GetInstructionData()
        {
            return await _dataContext.Instructions.ToListAsync();
        }
    }
}
