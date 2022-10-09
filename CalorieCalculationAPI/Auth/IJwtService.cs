using CalorieCalculation.API.Contracts;
using CalorieCalculation.Core.Services;

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

        public Task<string> GetTokenAsync(AuthRequest request)
        {
            var user = _userService.
        }
    }
}
