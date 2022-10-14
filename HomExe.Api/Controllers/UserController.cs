﻿using HomExe.Data;
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
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO user)
        {
            BaseResponse<User> response = new();

            var us =_context.Users.Where(x=>x.UserName==user.UserName && x.Password==user.Password).FirstOrDefault();
            if (us== null)
            {
                response.Code = "201";
                response.Message = "Username or password is incorrect";
            }
            else
            {
                response.Code = "200";
                response.Message = "Login successfully";
                response.Data = us;
            }
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO user)
        {
            BaseResponse<string> response = new();

            var existed = _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();
            if (existed == null)
            {
                var us = new User
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = user.Password,
                    Phone = user.Phone,
                    Height = user.Height,
                    Weight = user.Weight,
                    Status = user.Status,
                    RoleId = 1
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
        public async Task<IActionResult> Put(int id,[FromBody] User us)
        {
            BaseResponse<string> response = new();

            if (id != us.UserId)
            {
                return BadRequest();
            }
            _context.Entry(us).State = EntityState.Modified;
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Code = "200";
                response.Message = "Edit recipe successfully";
            }
            else
            {
                response.Code = "201";
                response.Message = "Edit recipe failed";
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
