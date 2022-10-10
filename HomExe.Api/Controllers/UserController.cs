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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginRequestDTO user)
        {
            var us=_context.Users.Where(x=>x.UserName==user.UserName && x.Password==user.Password).FirstOrDefault();
            if (us== null)
            {
                return NotFound();
            }
            return us;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO user)
        {
            var us = _context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            if (us != null)
            {
                return NotFound();
            }
            return us;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User us)
        {
            if (id != us.UserId)
            {
                return BadRequest();
            }
            _context.Entry(us).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!UserExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private bool UserExist(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
