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
            BaseResponse<Data.Enumerables.Schedule> response = new();

            var con = await _context.Contracts.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (con != null)
            {
                response.Code = "200";
                response.Message = " Get schedule for user successfully";
                response.Data = (Data.Enumerables.Schedule)con.Schedule;
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
            BaseResponse<List<Data.Enumerables.Schedule>> response = new();

            var cons = await _context.Contracts.Where(x => x.PtId == ptId).ToListAsync();

            if (cons.Count > 0)
            {
                var scheduleList = new List<Data.Enumerables.Schedule>();
                foreach (var item in cons)
                {
                    scheduleList.Add((Data.Enumerables.Schedule)item.Schedule);
                }
                response.Code = "200";
                response.Message = " Get schedule for pt successfully";
                response.Data = scheduleList;
            }
            else
            {
                response.Code = "201";
                response.Message = " Get schedule for pt failed";
            }


            return Ok(response);
        }

    }
}
