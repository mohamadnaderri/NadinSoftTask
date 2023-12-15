using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using NadinSoftTask.Infrastructure;
using System.Security.Claims;

namespace NadinSoftTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        protected UserInfo CurrentUser => (User.Identity as ClaimsIdentity).Claims.ToList()?.ToUserInfo();

    }
}
