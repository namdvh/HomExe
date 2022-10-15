using HomExe.Data;
using HomExe.ViewModels.BaseResponse;
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
        public async Task<IActionResult> GetScheduleForUser(int userId)
        {
            BaseResponse<Schedule> response = new();

            var sche = await (from con in _context.Contracts
                        join pt in _context.Pts on con.PtId equals pt.PtId
                        join schedule in _context.Schedules on pt.PtId equals schedule.PtId
                        where con.UserId == userId && !con.Status.Equals("0")
                              select schedule).FirstOrDefaultAsync();

            if(sche != null)
            {
                response.Code = "200";
                response.Message = " Get schedule for user successfully";
                response.Data = sche;
            }
            else
            {
                response.Code = "201";
                response.Message = " User do not have pt yet";
            }


            return Ok(response);
        }
        
        //[Route("{ptId}")]
        [HttpGet("pt")]
        public async Task<IActionResult> GetScheduleForPt(int ptId)
        {
            BaseResponse<Schedule> response = new();

            var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == ptId);

            if (sche != null)
            {
                response.Code = "200";
                response.Message = " Get schedule for pt successfully";
                response.Data = sche;
            }
            else
            {
                response.Code = "201";
                response.Message = " Get schedule for pt failed";
            }


            return Ok(response);
        }


        

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ptId,[FromBody] Schedule sche)
        {
            BaseResponse<string> response = new();

            if (ptId != sche.PtId)
            {
                return BadRequest();
            }
            _context.Entry(sche).State = EntityState.Modified;

            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Edit schedule successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Edit schedule failed";
            }
            return Ok(response);

        }

        
    }
}
