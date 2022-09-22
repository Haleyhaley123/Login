using Domain.BaseModel;
using Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reponsitory.Interface;
using WebDemo.Constants;

namespace WebDemo.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAccountController : BaseApiController
    {
        private readonly IUserReponsitory _iUserService;        
        public RegisterAccountController(IUserReponsitory iUserService)
        {
            _iUserService = iUserService;
        }
        [HttpPost("api/{type}/{visitId}/{idform}")]
        public async Task<ActionResult<WsResponse>> CreateAccount([FromBody] UserModel request)
        {
            WsResponse response = new WsResponse();
            var data = await _iUserService.Registation(request);
            if (data == false)
            {
                response.Status = WsConstants.MessageAccountExist;
            }else
            {
                response.Status = WsConstants.MessageRegisterSuccess;
                response.Data = new
                {
                    UserId = request.UserId,
                    UserName = request.UserName,
                    Password = request.PasswordHash
                };
            }
            return response;
        }
    }
}
