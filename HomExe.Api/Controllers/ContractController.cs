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
            var con = await _context.Contracts.FirstOrDefaultAsync(x => x.UserId == userId && !x.Status.Equals("0"));
            if (con != null)
            {
                var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == con.PtId);

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
        public async Task<IActionResult> GetContractForPt(int ptId)
        {
            BaseResponse<List<Contract>> response = new();

            var conList = await _context.Contracts.Where(x=>x.PtId == ptId && !x.Status.Equals("0")).ToListAsync();
            
            if (conList.Count != 0)
            {
                var userList = new List<User>();
                foreach (var con in conList)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == con.UserId);
                    var rp = await _context.HealthReports.FirstOrDefaultAsync(x => x.UserId == con.UserId);
                    if(rp != null)
                    {
                        user.Status = "2";
                    }
                    userList.Add(user);

                }
                response.Code = "200";
                response.Message = "Get contracts for pt successfully";
                response.Data = conList;

            }
            else
            {
                response.Code = "201";
                response.Message = "PT do not have contract or all contracts are expired";
            }


            return Ok(response);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetContractForAdmin()
        {
            BaseResponse<List<Contract>> response = new();

            var conList = await _context.Contracts.Where(x => x.Status.Equals("0")).ToListAsync();

            if (conList.Count > 0)
            {
                var userList = new List<User>();
                var ptList = new List<Pt>();
                foreach (var con in conList)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == con.UserId);
                    userList.Add(user);
                    var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == con.PtId);
                    ptList.Add(pt);

                }
                response.Code = "200";
                response.Message = "Get contracts for admin successfully";
                response.Data = conList;

            }
            else
            {
                response.Code = "201";
                response.Message = "Do not have waiting contract";
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
                    Schedule = request.Schedule,
                    Status = "0"
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
                response.Message = "User had a contract already or not payment yet";

            }
            return Ok(response);         
        }

        [HttpPut("{contractId}")]
        public async Task<IActionResult> ConfirmContract(int contractId)
        {
            BaseResponse<string> response = new();

            var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.ContractId == contractId);
            contract.Status = "1";
            //_context.Entry(contract).State = EntityState.Modified;
            _context.Entry(contract).Property(x => x.Status).IsModified = true;

            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Confirm contract successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Confirm contract failed";
            }
            return Ok(response);

        }


    }
}
