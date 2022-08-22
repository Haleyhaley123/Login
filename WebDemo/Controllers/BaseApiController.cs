using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDemo.Utilities;

namespace WebDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public Guid CurrentUserId
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    var wsid = User.FindFirstValue("wsid").ToString();
                    return Guid.Parse(EncryptHelper.Decrypt(wsid));
                }
                else
                {
                    return (Guid)(Guid?)null;
                }

            }
        }
    }
}
