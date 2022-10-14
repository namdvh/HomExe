using HomExe.Data;
using HomExe.ViewModels.BaseResponse;
using HomExe.ViewModels.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomExe.Api.Controllers
{
    [Route("api/contract")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly HomExeContext _context;

        public ContractController(HomExeContext context)
        {
            _context = context;
        }


        [HttpGet("user")]
        public async Task<IActionResult> GetContractForUser(int userId)
        {
            BaseResponse<Contract> response = new();
            var con = await _context.Contracts.FirstOrDefaultAsync(x => x.UserId == userId);

            if(con != null)
            {
                response.Code = "200";
                response.Message = "Get contract for user successfully";
                response.Data = con;

            }
            else
            {
                response.Code = "201";
                response.Message = "User do not have contract or contract is expired";
            }


            return Ok(response);
        }

        //[Route("{ptId}")]
        [HttpGet("pt")]
        public async Task<IActionResult> GetContractForPt( int ptId)
        {
            BaseResponse<List<Contract>> response = new();

            var con = await _context.Contracts.Where(x=>x.PtId == ptId).ToListAsync();

            if (con.Count != 0)
            {
                response.Code = "200";
                response.Message = "Get contracts for pt successfully";
                response.Data = con;

            }
            else
            {
                response.Code = "201";
                response.Message = "PT do not have contract or all contracts are expired";
            }


            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractDTO request)
        {
            BaseResponse<string> response = new();

            var existed = await _context.Contracts.FirstOrDefaultAsync(x => x.UserId == request.UserId);
            if(existed == null)
            {
                var con = new Contract
                {
                    UserId = request.UserId,
                    PtId = request.PtId,
                    CreatedDate = request.CreatedDate,
                    EndDate = request.EndDate,
                    Status = request.Status,
                };

                _context.Contracts.Add(con);
                var rs = await _context.SaveChangesAsync();
                if (rs > 0)
                {
                    response.Code = "200";
                    response.Message = "Create contract successfully";
                }
                else
                {
                    response.Code = "201";
                    response.Message = "Create contract failed";

                }
            }
            else
            {
                response.Code = "202";
                response.Message = "User had a contract already";

            }
            return Ok(response);
            

        }

       
    }
}
