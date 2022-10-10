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


        [HttpGet("{id}")]
        public async Task<Contract> Get(int userId)
        {
            var con = await _context.Contracts.FirstOrDefaultAsync(x => x.UserId == userId);


            return con;
        }


        [HttpPost]
        public async Task<IActionResult> Create(ContractDTO request)
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

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Contract con)
        {
            if (id != con.ContractId)
            {
                return BadRequest();
            }
            _context.Entry(con).State = EntityState.Modified;

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

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var con = await _context.Contracts.FirstOrDefaultAsync(x => x.ContractId == id);

            if (con.Status.Equals("1"))
            {
                con.Status = "0";
            }
            else
            {
                con.Status = "1";
            }

            _context.Contracts.Update(con);
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
