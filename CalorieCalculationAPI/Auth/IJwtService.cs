using CalorieCalculation.API.Contracts;
using CalorieCalculation.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CalorieCalculation.API.Auth
{
    public interface IJwtService
    {
        Task<string> GetTokenAsync(AuthRequest request);
    }
    public class JwtService : IJwtService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public JwtService(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        public async Task<string> GetTokenAsync(AuthRequest request)
        {
            var user = await _userService.GetByUsername(request.Name);

            if(user == null)
            {
                return await Task.FromResult<string>(null);
            }

            if (request.Password.Equals(user.Password))
            {
                var jwtKey = _config.GetValue<string>("JwtSettings:Key");
                var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

                var tokenHandler = new JwtSecurityTokenHandler();

                var descriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddSeconds(60),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(keyBytes),
                        SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(descriptor);
                return await Task.FromResult(tokenHandler.WriteToken(token));
            }
            return await Task.FromResult<string>(null);
        }
    }
}
