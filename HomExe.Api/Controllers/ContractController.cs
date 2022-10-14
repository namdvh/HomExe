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
        public async Task<ActionResult<Contract>> GetContractForUser(int userId)
        {
            var con = await _context.Contracts.FirstOrDefaultAsync(x => x.UserId == userId);


            return Ok(con);
        }

        //[Route("{ptId}")]
        [HttpGet("pt")]
        public async Task<ActionResult<List<Contract>>> GetContractForPt( int ptId)
        {
            var con = await _context.Contracts.Where(x=>x.PtId == ptId).ToListAsync();


            return Ok(con);
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
