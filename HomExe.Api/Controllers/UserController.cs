using HomExe.Data;
using HomExe.ViewModels.BaseResponse;
using HomExe.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomExe.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HomExeContext _context;

        public UserController(HomExeContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            BaseResponse<List<User>> response = new();

            List<User> userList = new();
            userList = await _context.Users.ToListAsync();
            if (userList.Count > 0)
            {
                response.Code = "200";
                response.Message = "Get users list successfully";
                response.Data = userList;
            }
            else
            {
                response.Code = "201";
                response.Message = "Users list is empty";
            }

            return Ok(response);
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BaseResponse<User> response = new();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user != null)
            {
                response.Code = "200";
                response.Message = "Get user successfully";
                response.Data = user;
            }
            else
            {
                response.Code = "201";
                response.Message = "Get user failed";
            }

            return Ok(response);
        }

        // POST api/<UserController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO user)
        {
            BaseResponse<User> response = new();

            var us = await _context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefaultAsync();
            if (us == null)
            {
                BaseResponse<Pt> responsePt = new();

                var pt = await _context.Pts.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefaultAsync();
                if (pt == null)
                {
                    responsePt.Code = "201";
                    responsePt.Message = "Username or password is incorrect";
                }
                else
                {
                    var cons = await _context.Contracts.Where(x => x.PtId == pt.PtId && !x.Status.Equals("0")).ToListAsync();
                    if (cons.Count > 0)
                    {
                        var now = DateTime.Now.ToString("MM/dd/yyyy");
                        foreach (var con in cons)
                        {
                            var endDate = DateTime.Parse(con.EndDate);
                            if (endDate.ToString("MM/dd/yyyy").Equals(now))
                            {
                                _context.Contracts.Remove(con);
                                await _context.SaveChangesAsync();
                            }

                        }
                    }

                    responsePt.Code = "200";
                    responsePt.Message = "Login for PT successfully";
                    responsePt.Data = pt;
                }
                return Ok(responsePt);
            }
            else
            {
                var con = await _context.Contracts.Where(x => x.UserId == us.UserId && !x.Status.Equals("0")).FirstOrDefaultAsync();
                var now = DateTime.Now.ToString("MM/dd/yyyy");
                if (us.RoleId == 1 && con != null && DateTime.Parse(con.EndDate).ToString("MM/dd/yyyy").Equals(now))
                {
                    _context.Contracts.Remove(con);
                    await _context.SaveChangesAsync();
                }

                response.Code = "200";
                response.Message = "Login for user successfully";
                response.Data = us;
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            BaseResponse<string> response = new();

            var existed = await _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();
            if (existed == null)
            {
                var us = new User
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = user.Password,
                    Phone = user?.Phone,
                    Height = user?.Height,
                    Weight = user?.Weight,
                    Status = "1",
                    RoleId = 1,
                    FullName = user.FirstName + " " + user.LastName
                };

                _context.Users.Add(us);
                var rs = await _context.SaveChangesAsync();
                if (rs > 0)
                {
                    response.Code = "200";
                    response.Message = "Regist successfully";
                }
                else
                {
                    response.Code = "201";
                    response.Message = "Regist pt failed";
                }
            }
            else
            {
                response.Code = "202";
                response.Message = "Username is already exist";
            }
            return Ok(response);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserRequestDTO us)
        {
            BaseResponse<string> response = new();

            if (id != us.UserId)
            {
                return BadRequest();
            }
            var x = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (x == null)
            {
                response.Code = "202";
                response.Message = "Not found that Users";
                return Ok(response);
            }
            x.UserName = us.UserName;
            x.Password = us.Password;
            x.FullName = us.FullName;
            x.Status = us.Status;
            x.Email = us.Email;
            x.Weight = us.Weight;
            x.Height = us.Height;
            x.Phone = us.Phone;
            _context.Users.Update(x);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Edit user successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Edit user failed";
            }
            return Ok(response);

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            BaseResponse<string> response = new();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user.Status.Equals("1"))
            {
                user.Status = "0";
            }
            else
            {
                user.Status = "1";
            }

            _context.Users.Update(user);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Delete recipe successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Delete recipe failed";
            }
            return Ok(response);


        }
    }
}
