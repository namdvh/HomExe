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
                    FullName = pt.FullName,
                    Cover = pt.Cover,
                    Dob = pt.Dob,
                };
                ptDTOs.Add(dto);

            }
            if (ptDTOs.Count > 0)
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

        [HttpGet("detail")]
        public async Task<IActionResult> GetPtById(int ptId)
        {
            BaseResponse<PtDTO> response = new();


            var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == ptId);

            if (pt != null)
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
                    FullName = pt.FullName,
                    Cover = pt.Cover,
                    Dob = pt.Dob,
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
        [HttpGet("category")]
        public async Task<IActionResult> GetCategory()
        {
            BaseResponse<List<PtCategory>> response = new();


            var category = await _context.PtCategories.ToListAsync();

            if (category.Count > 0)
            {
               
                response.Code = "200";
                response.Message = "Get pt category successfully";
                response.Data = category;
            }
            else
            {
                response.Code = "201";
                response.Message = "Get pt category failed";
            }


            return Ok(response);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetPtForUser(int userId)
        {
            BaseResponse<PtDTO> response = new();

            var pt = await (from con in _context.Contracts
                            join _pt in _context.Pts on con.PtId equals _pt.PtId
                            where con.UserId == userId && !con.Status.Equals("0")
                            select _pt).FirstOrDefaultAsync();

            if (pt != null)
            {


                var dto = new PtDTO
                {
                    PtId = pt.PtId,
                    Email = pt.Email,
                    UserName = pt.UserName,
                    Password = pt.Password,
                    Phone = pt.Phone,
                    CategoryId = pt.CategoryId,
                    FullName = pt.FullName,
                    LinkMeet = pt.LinkMeet,
                    Cover = pt.Cover,
                    Dob = pt.Dob,
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
        public async Task<IActionResult> Create([FromBody] CreatePtDTO request)
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
                Status = "1",
                Cover = request.Cover,
                Dob = request.Dob,
                Address = request.Address,
                FullName = request.FirstName + " " + request.LastName,
                RoleId = 2
            };

            _context.Pts.Add(pt);
            var rs = await _context.SaveChangesAsync();

            if (rs > 0)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PtDTO pt)
        {
            BaseResponse<string> response = new();

            if (id != pt.PtId)
            {
                return BadRequest();
            }

            var _pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == id);
            _pt.Email = pt.Email;
            _pt.UserName = pt.UserName;
            _pt.Status = pt.Status;
            _pt.CategoryId = pt.CategoryId;
            _pt.LinkMeet = pt.LinkMeet;
            _pt.FullName = pt.FullName;
            pt.Address = pt.Address;
            pt.Cover = pt.Cover;
            pt.Dob = pt.Dob;
            _pt.Phone = pt.Phone;

            _context.Pts.Update(_pt);

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