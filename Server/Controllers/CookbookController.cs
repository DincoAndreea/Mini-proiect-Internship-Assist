using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Server.Data;
using RecipeApp.Shared;

namespace RecipeApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookbookController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CookbookController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cookbook>>> GetCookbooks()
        {
            var cookbooks = await _dataContext.Cookbooks.ToListAsync();
            return Ok(cookbooks);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<Cookbook>> GetCookbook(int Id)
        {
            var cookbook = await _dataContext.Cookbooks.FirstOrDefaultAsync(x => x.Id == Id);

            if(cookbook == null)
            {
                return new Cookbook();
            }

            return Ok(cookbook);
        }

        [HttpPost]
        public async Task<ActionResult<Cookbook>> CreateCookbook(Cookbook cookbook)
        {
            _dataContext.Add(cookbook);
            await _dataContext.SaveChangesAsync();

            return Ok(cookbook);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Cookbook>> UpdateCookbook(Cookbook cookbook, int Id)
        {
            var cookbookPut = await _dataContext.Cookbooks.FirstOrDefaultAsync(x => x.Id == Id);

            if (cookbookPut == null)
            {
                return NotFound("The cookbook doesn't exist.");
            }
               
            cookbookPut.Name = cookbook.Name;

            await _dataContext.SaveChangesAsync();

            return Ok(cookbookPut);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Cookbook>>> DeleteCookbook(int Id)
        {
            var cookbookDelete = await _dataContext.Cookbooks.FirstOrDefaultAsync(x => x.Id == Id);

            if (cookbookDelete == null)
                return NotFound("The cookbook doesn't exist.");

            _dataContext.Cookbooks.Remove(cookbookDelete);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetCookbookData());
        }

        private async Task<List<Cookbook>> GetCookbookData()
        {
            return await _dataContext.Cookbooks.ToListAsync();
        }
    }
}
