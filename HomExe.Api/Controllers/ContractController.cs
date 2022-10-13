using HomExe.Data;
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
        public async Task<Contract> GetContractForUser([FromRoute]int userId)
        {
            var con = await _context.Contracts.FirstOrDefaultAsync(x => x.UserId == userId);


            return con;
        }

        //[Route("{ptId}")]
        [HttpGet("pt")]
        public async Task<List<Contract>> GetContractForPt([FromRoute] int ptId)
        {
            var con = await _context.Contracts.Where(x=>x.PtId == ptId).ToListAsync();


            return con;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractDTO request)
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
                return Ok();
            }
            else
            {

                return BadRequest();
            }

        }

       
    }
}
