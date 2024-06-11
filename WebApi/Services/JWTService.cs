using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.Users;

namespace WebApi.Services
{
    public class JWTService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<JWTService> _logger;
        private readonly JWTOptions _jwt;

        public JWTService(UserManager<ApplicationUser> userManager, ILogger<JWTService> logger,IOptions<JWTOptions> options)
        {
            _userManager = userManager;
            _logger = logger;
            _jwt = options.Value;
        }
        public string GetAccessToken(ApplicationUser user,CancellationToken token=default)
        {
            //var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = _userManager.GetRolesAsync(user).Result;
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id.ToString())
            }
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            _logger.LogInformation($"JWT Token created a for account with email:{user.Email}.");
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
