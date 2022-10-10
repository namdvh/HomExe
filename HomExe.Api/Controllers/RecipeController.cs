using HomExe.Data;
using HomExe.ViewModels.Recipes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomExe.Api.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly HomExeContext _context;

        public RecipeController(HomExeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipee>>> GetList()
        {
            List<Recipee> recList = new();
            recList = await _context.Recipees.ToListAsync();

            return recList;
        }

        [HttpGet("{id}")]
        public async Task<Recipee> Get(int id)
        {
            var rec = await _context.Recipees.FirstOrDefaultAsync(x => x.RecipeId == id);


            return rec;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePt(RecipeDTO request)
        {
            var rec = new Recipee
            {
                Recipe = request.Recipe,
                Pictures = request.Pictures,
                CategoryId = request.CategoryId
            };

            _context.Recipees.Add(rec);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                return Ok();
            }
            else
            {

                return BadRequest();
            }

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Recipee rec)
        {
            if (id != rec.RecipeId)
            {
                return BadRequest();
            }
            _context.Entry(rec).State = EntityState.Modified;

            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rec = await _context.Recipees.FirstOrDefaultAsync(x => x.RecipeId == id);

            _context.Recipees.Remove(rec);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                return Ok();
            }
            else
            {

                return BadRequest();
            }

        }
    }
}
