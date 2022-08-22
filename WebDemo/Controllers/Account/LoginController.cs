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
    public class LoginController : BaseApiController
    {
        private readonly IUserReponsitory _iUserReponsitory;
        public LoginController(IUserReponsitory iUserReponsitory)
        {
            _iUserReponsitory = iUserReponsitory;
        }
        [HttpPost("api/loginAccount/Login")]
        public async Task<ActionResult<WsResponse>> LoginAccount([FromBody] LoginModel request)
        {
            WsResponse response = new WsResponse();
            var data = await _iUserReponsitory.LoginAccount(request);
            if (data == false)
            {
                response.Status = WsConstants.MessageLoginFaild;
            }else
            {
                response.Status = WsConstants.MessageLoginSuccess;
            }
            return response;
        }
    }
}
