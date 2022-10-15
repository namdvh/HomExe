using HomExe.Data;
using HomExe.ViewModels;
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
                        where con.UserId == userId
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
        [HttpPut("{ptId}")]
        public async Task<IActionResult> Put(int ptId, ScheduleDTO sche)
        {
            BaseResponse<string> response = new();
            var x =await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == ptId);
            if (x == null)
            {
                response.Code = "202";
                response.Message = "Not found that Schedule";
                return Ok(response);
            }
            x.Date = sche.Date;
            _context.Schedules.Update(x);
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
