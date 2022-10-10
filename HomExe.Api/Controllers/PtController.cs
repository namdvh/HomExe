using HomExe.Data;
using HomExe.ViewModels.Pts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomExe.Api.Controllers
{
    [Route("api/pt")]
    [ApiController]
    public class PtController : ControllerBase
    {
        private readonly HomExeContext _context;

        public PtController(HomExeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pt>>> GetList()
        {
            List<Pt> ptList = new();
            ptList = await _context.Pts.ToListAsync();

            return ptList;
        }

        [HttpGet("{id}")]
        public async Task<Pt> Get(int id)
        {
            var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == id);

           
            return pt;
        }


        [HttpPost]
        public async Task<IActionResult> Create(PtDTO request)
        {
            var pt = new Pt
            {
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                CategoryId = request.CategoryId,
                LinkMeet = request.LinkMeet,
                UserName = request.UserName,
                Status = request.Status 
            };

            _context.Pts.Add(pt);
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
        public async Task<IActionResult> Put(int id, Pt pt)
        {
            if (id != pt.PtId)
            {
                return BadRequest();
            }
            _context.Entry(pt).State = EntityState.Modified;

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
            var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == id);

            if (pt.Status.Equals("1"))
            {
                pt.Status = "0";
            }
            else
            {
                pt.Status = "1";
            }

            _context.Pts.Update(pt);
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
