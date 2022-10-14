using HomExe.Data;
using HomExe.ViewModels.Pts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomExe.Api.Controllers
{
    [Route("api/pt")]
    [ApiController]
    public class PtController : ControllerBase
    {
        private readonly HomExeContext _context;

        public PtController(HomExeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PtDTO>>> GetList()
        {
            List<Pt> ptList = new();
            ptList = await _context.Pts.ToListAsync();
            List<PtDTO> ptDTOs = new();
            foreach (var pt in ptList)
            {
                var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == pt.PtId);
                var dto = new PtDTO{
                    PtId = pt.PtId,
                    Email = pt.Email,
                    UserName = pt.UserName,
                    Password = pt.Password,
                    Phone = pt.Phone,
                    CategoryId = pt.CategoryId,
                    LinkMeet = pt.LinkMeet,
                    Status = pt.Status,
                    Schedule = sche.Date
                        };
                ptDTOs.Add(dto);

            }

            return Ok(ptDTOs);
        }

        //[Route("{ptId}")]
        [HttpGet("detail")]
        public async Task<ActionResult<PtDTO>> GetPtById(int ptId)
        {
            var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == ptId);
            var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == ptId);
            var dto = new PtDTO
            {
                PtId = pt.PtId,
                Email = pt.Email,
                UserName = pt.UserName,
                Password = pt.Password,
                Phone = pt.Phone,
                CategoryId = pt.CategoryId,
                LinkMeet = pt.LinkMeet,
                Status = pt.Status,
                Schedule = sche.Date
            };


            return Ok(dto);
        }
        
        //[Route("{userId}")]
        [HttpGet("user")]
        public async Task<ActionResult<PtDTO>> GetPtForUser(int userId)
        {
            var pt = await (from con in _context.Contracts
                            join _pt in _context.Pts on con.PtId equals _pt.PtId
                            where con.UserId == userId
                            select _pt).FirstOrDefaultAsync();

            var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == pt.PtId);
            var dto = new PtDTO
            {
                PtId = pt.PtId,
                Email = pt.Email,
                UserName = pt.UserName,
                Password = pt.Password,
                Phone = pt.Phone,
                CategoryId = pt.CategoryId,
                LinkMeet = pt.LinkMeet,
                Status = pt.Status,
                Schedule = sche.Date
            };


            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PtDTO request)
        {
            var pt = new Pt
            {
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                CategoryId = request.CategoryId,
                LinkMeet = request.LinkMeet,
                UserName = request.UserName,
                Status = request.Status 
            };

            _context.Pts.Add(pt);
            var rs = await _context.SaveChangesAsync();

            var sche = new Schedule
            {
                Date = request.Schedule,
                PtId = pt.PtId
            };

            _context.Schedules.Add(sche);
            var rsSche = await _context.SaveChangesAsync();
            if (rs > 0 && rsSche > 0)
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
        public async Task<IActionResult> Put(int id,[FromBody] Pt pt)
        {
            if (id != pt.PtId)
            {
                return BadRequest();
            }
            _context.Entry(pt).State = EntityState.Modified;

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
            var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == id);

            if (pt.Status.Equals("1"))
            {
                pt.Status = "0";
            }
            else
            {
                pt.Status = "1";
            }

            _context.Pts.Update(pt);
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
