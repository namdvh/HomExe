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
            BaseResponse<HealthReport> response = new();


            var rp = await _context.HealthReports.FirstOrDefaultAsync(x => x.UserId == userId);
            
            if (rp != null)
            {
                response.Code = "200";
                response.Message = "Get health report successfully";
                response.Data = rp;
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
                Problems = request.Problems
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
            var x = await _context.HealthReports.FirstOrDefaultAsync(x => x.HealthId == id);
            if (x == null)
            {
                response.Code = "202";
                response.Message = "Not found that Report";
                return Ok(response);
            }
            x.Problems = rp.Problems;
            _context.HealthReports.Update(x);
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
