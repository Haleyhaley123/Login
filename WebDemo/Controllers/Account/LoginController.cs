using Domain.BaseModel;
using Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reponsitory.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebDemo.Constants;
using WebDemo.Utilities;

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
        [AllowAnonymous]
        [HttpPost("api/loginAccount/Login")]
        public async Task<ActionResult<WsResponse>> LoginAccount(string username, string password)
        {
            WsResponse response = new WsResponse();
            var data = await _iUserReponsitory.LoginAccount(username, password);
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
