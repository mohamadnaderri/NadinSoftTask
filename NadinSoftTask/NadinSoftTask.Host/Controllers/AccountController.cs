namespace NadinSoftTask.Host.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NadinSoftTask.Commands.User;
    using NadinSoftTask.Host.Helpers;
    using Newtonsoft.Json;
    using System.Data;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IJwtFactory jwtFactory, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtFactory = jwtFactory;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            command.Validate();
            var user = await GetUserByUserName(command.Username);
            await CheckUserPassword(user, command.Password);

            var generatedToken = await GenerateJsonWebToken(user);

            return new OkObjectResult(new { Token = generatedToken, UserId = user.Id, message = "ورود انجام گردید", phoneNumberConfirm = true });
        }

        private async Task<IdentityUser> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception("نام کاربری یا کلمه عبور اشتباه می باشد");
            return user;
        }

        private async Task CheckUserPassword(IdentityUser user, string password)
        {
            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new Exception("نام کاربری یا کلمه عبور اشتباه می باشد");
        }

        private async Task<JsonWebToken> GenerateJsonWebToken(IdentityUser user)
        {
            var identity = await GetClaimsIdentity(user.UserName, user.Id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRoleIds = GetUserRoleIds(userRoles);
            var generatedToken = TokenGenerator.GenerateJwt(user, userRoles.ToList(), userRoleIds.ToList(), identity, _jwtFactory, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return generatedToken;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string userId)
        {
            var identity = await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userId));
            if (identity == null)
                throw new Exception("نام کاربری یا کلمه عبور اشتباه می باشد");
            return identity;
        }

        private IEnumerable<string> GetUserRoleIds(IEnumerable<string> userRoles)
        {
            return userRoles.Select(userRole => _roleManager.GetRoleIdAsync(new IdentityRole(userRole)).Result).ToList();
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged out" });
        }
    }

}
