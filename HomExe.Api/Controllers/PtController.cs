using HomExe.Data;
using HomExe.ViewModels.BaseResponse;
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
        public async Task<IActionResult> GetList()
        {
            BaseResponse<List<PtDTO>> response = new();

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
            if(ptDTOs.Count > 0)
            {
                response.Code = "200";
                response.Message = "Get list pts successfully";
                response.Data = ptDTOs;
            }
            else
            {
                response.Code = "201";
                response.Message = "List pts is empty";
            }

            return Ok(response);
        }

        //[Route("{ptId}")]
        [HttpGet("detail")]
        public async Task<IActionResult> GetPtById(int ptId)
        {
            BaseResponse<PtDTO> response = new();


            var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == ptId);
            var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == ptId);
            if(pt != null && sche != null)
            {
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
                response.Code = "200";
                response.Message = "Get pt by id successfully";
                response.Data = dto;
            }
            else
            {
                response.Code = "201";
                response.Message = "Get pt by id failed";
            }


            return Ok(response);
        }
        
        //[Route("{userId}")]
        [HttpGet("user")]
        public async Task<IActionResult> GetPtForUser(int userId)
        {
            BaseResponse<PtDTO> response = new();

            var pt = await (from con in _context.Contracts
                            join _pt in _context.Pts on con.PtId equals _pt.PtId
                            where con.UserId == userId
                            select _pt).FirstOrDefaultAsync();

            if (pt != null)
            {
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

                response.Code = "200";
                response.Message = "Get pt by id successfully";
                response.Data = dto;
            }
            else
            {
                response.Code = "201";
                response.Message = "User do not have pt yet";
            }


            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PtDTO request)
        {

            BaseResponse<string> response = new();

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
                response.Code = "200";
                response.Message = "Create pt successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Create pt failed"; 
            }
            return Ok(response);

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] Pt pt)
        {
            BaseResponse<string> response = new();

            if (id != pt.PtId)
            {
                return BadRequest();
            }
            _context.Entry(pt).State = EntityState.Modified;

            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Edit pt successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Edit pt failed";
            }
            return Ok(response);

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            BaseResponse<string> response = new();

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
                response.Code = "200";
                response.Message = "Delete pt successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Delete pt failed";
            }
            return Ok(response);

        }
    }
}
