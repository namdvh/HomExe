using HomExe.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomExe.Api.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly HomExeContext _context;

        public ScheduleController(HomExeContext context)
        {
            _context = context;
        }

        //[Route("{userId}")]
        [HttpGet("user")]
        public async Task<ActionResult<Schedule>> GetScheduleForUser(int userId)
        {
            var sche = await (from con in _context.Contracts
                        join pt in _context.Pts on con.PtId equals pt.PtId
                        join schedule in _context.Schedules on pt.PtId equals schedule.PtId
                        where con.UserId == userId
                        select schedule).FirstOrDefaultAsync();


            return Ok(sche);
        }
        
        //[Route("{ptId}")]
        [HttpGet("pt")]
        public async Task<ActionResult<Schedule>> GetScheduleForPt(int ptId)
        {
            var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == ptId);


            return Ok(sche);
        }


        

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ptId,[FromBody] Schedule sche)
        {
            if (ptId != sche.PtId)
            {
                return BadRequest();
            }
            _context.Entry(sche).State = EntityState.Modified;

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
