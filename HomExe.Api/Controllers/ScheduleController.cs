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
        public Task<Schedule> GetScheduleForUser([FromRoute]int userId)
        {
            var sche = (from con in _context.Contracts
                        join pt in _context.Pts on con.PtId equals pt.PtId
                        join schedule in _context.Schedules on pt.PtId equals schedule.PtId
                        where con.UserId == userId
                        select schedule);


            return (Task<Schedule>)sche;
        }
        
        //[Route("{ptId}")]
        [HttpGet("pt")]
        public async Task<Schedule> GetScheduleForPt([FromRoute]int ptId)
        {
            var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == ptId);


            return sche;
        }


        

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int ptId,[FromBody] Schedule sche)
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
