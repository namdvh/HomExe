using HomExe.Data;
using HomExe.ViewModels.BaseResponse;
using HomExe.ViewModels.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomExe.Api.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly HomExeContext _context;

        public ReportController(HomExeContext context)
        {
            _context = context;
        }

        [HttpGet("user")]
        public async Task<IActionResult> Get(int userId)
        {
            BaseResponse<ReportResponse> response = new();


            var rp = await _context.HealthReports.FirstOrDefaultAsync(x => x.UserId == userId);
            
            if (rp != null)
            {
                var videoList = new List<Video>();
                var problems = rp.Problems.Split(',').ToList();
                foreach (var item in problems)
                {
                    var video = await _context.Videos.FirstOrDefaultAsync(x => x.ProblemId == Int32.Parse(item));
                    videoList.Add(video);
                }
                response.Code = "200";
                response.Message = "Get health report successfully";
                ReportResponse res = new();
                res.HealthReport = rp;
                res.Videos = videoList;
                response.Data = res;
            }
            else
            {
                response.Code = "201";
                response.Message = "Get health report failed";
            }


            return Ok(response);
        }
        
        [HttpGet("pt")]
        public async Task<IActionResult> GetReportForPt(int ptId)
        {
            BaseResponse<List<HealthReport>> response = new();


            var cons = await _context.Contracts.Where(x => x.PtId == ptId && !x.Status.Equals("0")).ToListAsync();
            var reportList = new List<HealthReport>();
            foreach (var item in cons)
            {
                var rp = await _context.HealthReports.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                var us = await _context.Users.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                reportList.Add(rp);
            }
            
            if (reportList.Count > 0)
            {
                
                
                response.Code = "200";
                response.Message = "Get health report successfully";
                response.Data = reportList;
            }
            else
            {
                response.Code = "201";
                response.Message = "Get health report failed";
            }


            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportDTO request)
        {
            BaseResponse<string> response = new();

            var rp = new HealthReport
            {
                UserId = request.UserId,
                Problems = request.Problems,
                Target = request.Target
            };

            _context.HealthReports.Add(rp);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Create health report successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Create health report failed";
            }
            return Ok(response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReportDTO rp)
        {
            BaseResponse<string> response = new();

            if (id != rp.HealthId)
            {
                return BadRequest();
            }
            _context.Entry(rp).State = EntityState.Modified;

            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Edit health report successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Edit health report failed";
            }
            return Ok(response);

        }
    }
}
