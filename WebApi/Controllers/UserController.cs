using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;
using WebApi.Models.Users;
using WebApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTService _jWTService;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JWTService jWTService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTService = jWTService;
        }
        [HttpPost("Register")]
        public ActionResult<IdentityResult> Register([FromBody] RegisterUserRequest request)
        {
            var user = new ApplicationUser
            {
                Firstname=request.Firstname, 
                Lastname=request.Lastname,
                Email = request.Email,
                UserName = request.Email
            };
            var result=_userManager.CreateAsync(user,request.Password).Result;
            if (!result.Succeeded)
            {
                return result;
            }
             _=_userManager.AddToRoleAsync(user, Roles.Consumer.ToString()).Result;
            return Ok(result);
        }


        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthUserRequest request)
        {
            var user = _userManager.FindByEmailAsync(request.Email).Result;
            if (user == null)
            {
                return Unauthorized();
            }
            var isAuthenticated=_userManager.CheckPasswordAsync(user, request.Password).Result;
            if(!isAuthenticated)
                return Unauthorized();
            var token = _jWTService.GetAccessToken(user);
            return token;
        }
    }
}
