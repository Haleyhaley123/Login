using Domain.BaseModel;
using Domain.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reponsitory.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebDemo.Constants;
using static Domain.DBContext.DBContext;

namespace Admin.Controllers.Account
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        private readonly IUserReponsitory _iUserReponsitory;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _dataBaseContext;
        public AccountController(
            IUserReponsitory iUserService, IConfiguration configuration, DatabaseContext dataBaseContext
            )
        {
            _configuration = configuration;
            _iUserReponsitory = iUserService; ;
            _dataBaseContext = dataBaseContext;
        }
       
        [HttpPost]
        public async Task<ActionResult<WsResponse>> LoginAccount(string username, string password)
        {
            WsResponse response = new WsResponse();
            var data = await _iUserReponsitory.LoginAccount(username, password);
            if (data != null)
            {
                var token = "";
                var rolecode = "abcd";
                try
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, data.UserName),
                    new Claim(ClaimTypes.Role, rolecode),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    if (data.RoleCode != null)
                    {
                        rolecode = data.RoleCode.ToString();
                        authClaims.Add(new Claim(ClaimTypes.Role, rolecode));
                    }
                    token = CreateToken(data);
                }
                catch
                {

                }

                response.Data = token;
                response.Status = WsConstants.MessageLoginSuccess;
            }
            else
            {

                response.Status = WsConstants.MessageLoginFaild;

            }
            return View(response);
        }
        private string CreateToken(UserModel data)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, data.UserName),
            };
            var secretKey = "mysupersecret_secretkey!123";
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            data.Token = jwt;
            _dataBaseContext.Users.Update(data);
            _dataBaseContext.SaveChangesAsync();

            return jwt;
        }
    }
}
