using HomExe.Data;
using HomExe.ViewModels.BaseResponse;
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
        public async Task<IActionResult> GetList()
        {
            BaseResponse<List<Recipee>> response = new();

            List<Recipee> recList = new();
            recList = await _context.Recipees.ToListAsync();
            if(recList.Count > 0)
            {
                response.Code = "200";
                response.Message = "Get recipe list successfully";
                response.Data = recList;
            }
            else
            {
                response.Code = "201";
                response.Message = "Recipe list is empty";

            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BaseResponse<Recipee> response = new();


            var rec = await _context.Recipees.FirstOrDefaultAsync(x => x.RecipeId == id);
            if(rec != null)
            {
                response.Code = "200";
                response.Message = "Get recipe successfully";
                response.Data = rec;
            }
            else
            {
                response.Code = "201";
                response.Message = "Get recipe failed";
            }


            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeDTO request)
        {
            BaseResponse<string> response = new();

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
                response.Code = "200";
                response.Message = "Create recipe successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Create recipe failed";
            }
            return Ok(response);

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Recipee rec)
        {
            BaseResponse<string> response = new();

            if (id != rec.RecipeId)
            {
                return BadRequest();
            }
            _context.Entry(rec).State = EntityState.Modified;

            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Edit recipe successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Edit recipe failed";
            }
            return Ok(response);

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            BaseResponse<string> response = new();

            var rec = await _context.Recipees.FirstOrDefaultAsync(x => x.RecipeId == id);

             _context.Recipees.Remove(rec);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Delete recipe successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Delete recipe failed";
            }
            return Ok(response);

        }
    }
}
