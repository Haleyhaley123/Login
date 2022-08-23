using Domain.BaseModel;
using Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                string token = CreateToken(username);
                response.Status = WsConstants.MessageLoginSuccess;
                response.Data = token;
            }
            return response;
        }
        private string CreateToken (string username)
        {
            List<Claim> claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.Name, username),
            };
            var secretKey = "mysupersecret_secretkey!123";
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddMinutes(2),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
