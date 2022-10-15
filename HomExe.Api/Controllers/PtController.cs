using Azure.Core;
using Azure.Identity;
using HomExe.Data;
using HomExe.ViewModels.BaseResponse;
using HomExe.ViewModels.Pts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

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
                    Schedule = sche.Date
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

        //[Route("{ptId}")]
        [HttpGet("detail")]
        public async Task<IActionResult> GetPtById(int ptId)
        {
            BaseResponse<PtDTO> response = new();


            var pt = await _context.Pts.FirstOrDefaultAsync(x => x.PtId == ptId);
            var sche = await _context.Schedules.FirstOrDefaultAsync(x => x.PtId == ptId);
            if (pt != null && sche != null)
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
                            where con.UserId == userId && !con.Status.Equals("0")
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
                    FullName = pt.FullName,
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
        public async Task<IActionResult> Create([FromBody] PtDTO request)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = "9b1ab0a1-9c14-4f05-9600-7c6db921abdb";
            var clientId = "f165b596-446c-4a29-9040-02476ad6c0c3";
            var authority = "https://login.microsoftonline.com";
            var redirectUrl = "https://localhost:5000";
            var clientSecret = "wd88Q~4Fkjsd1a_f.NtNEr394rngmn9kMYIv4dya";
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
              .Create(clientId)
              .WithRedirectUri(redirectUrl)
              .WithClientSecret(clientSecret)
              //.WithAuthority(authority)
              .Build();
            var authenResult = await app.AcquireTokenByAuthorizationCode(scopes, clientId).ExecuteAsync();
            GraphServiceClient graphServiceClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                async (request) =>
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authenResult.AccessToken);
                    await Task.Yield();
                }
                ));

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

            var sche = new HomExe.Data.Schedule
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
                var onlineMeeting = new OnlineMeeting
                {
                    StartDateTime = DateTimeOffset.Now,
                    EndDateTime = DateTimeOffset.Parse("2022-12-12T22:00:34.2464912+00:00"),
                    Subject = "User Token Meeting"
                };
                await graphServiceClient.Me.OnlineMeetings.Request().AddAsync(onlineMeeting);
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
pai