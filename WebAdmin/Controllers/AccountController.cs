
using Domain.BaseModel;
using Domain.User;
using Microsoft.AspNetCore.Mvc;
using Reponsitory.Interface;
using WebDemo.Constants;
using static Domain.DBContext.DBContext;

namespace WebAdmin.Controllers
{
    public class AccountController : Controller
    {
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
        public async Task<ActionResult<WsResponse>> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<WsResponse>> Login( string username, string password)
        {
            WsResponse response = new WsResponse();
            var data = await _iUserReponsitory.LoginAccount(username, password);
            if (data == null) return NotFound();
            response.Status = WsConstants.MessageLoginSuccess;
            return View("Login", response);
        }       
        

    }
}
