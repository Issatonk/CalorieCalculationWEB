using CalorieCalculation.API.Contracts;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Services;
using CalorieCalculation.DataAccess.Sqlite.Entities;
using CalorieCalculation.DataAccess.Sqlite.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CalorieCalculation.API.Auth
{
    public interface IJwtService
    {
        Task<AuthResponse> GetTokenAsync(AuthRequest request, string ipAddress);
        JwtSecurityToken GetJwtToken(string expiredToken);

        Task<UserRefreshToken> GetUserRefreshTokenAsync(RefreshTokenRequest request, string ipAddress);

        Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string username);

        Task UpdateRefreshToken(UserRefreshToken token);

        Task<bool> IsTokenValid(string accessToken, string ipAddress);
    }
    public class JwtService : IJwtService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

        public JwtService(IUserService userService, IConfiguration config, IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _userService = userService;
            _config = config;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<AuthResponse> GetTokenAsync(AuthRequest request, string ipAddress)
        {
            var user = await _userService.GetByUsername(request.Name);

            if(user == null)
            {
                return await Task.FromResult<AuthResponse>(null);
            }

            if (request.Password.Equals(user.Password))
            {
                string tokenString = GenerateToken(user);
                string refreshToken = GenerateRefreshToken();

                return await SaveTokenDetails(ipAddress, user.Id, tokenString, refreshToken);
            }
            return await Task.FromResult<AuthResponse>(null);
        }

        private async Task<AuthResponse> SaveTokenDetails(string ipAddress, int userId, string tokenString, string refreshToken)
        {
            var userRefreshToken = new UserRefreshToken
            {
                CreatedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                IpAddress = ipAddress,
                IsInvalidated = false,
                RefreshToken = refreshToken,
                Token = tokenString,
                UserId = userId
            };

            await _userRefreshTokenRepository.AddAsync(userRefreshToken);

            return new AuthResponse
            {
                Token = tokenString,
                RefreshToken = refreshToken,
                IsSuccess = true
            };
        }

        public JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }

        public async Task<UserRefreshToken> GetUserRefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            var userRefreshToken = await _userRefreshTokenRepository.Get(new UserRefreshToken
            {
                Token = request.ExpiredToken,
                RefreshToken = request.RefreshToken,
                IsInvalidated = false,
                IpAddress = ipAddress
            });
            return userRefreshToken;
        }
        public async Task UpdateRefreshToken(UserRefreshToken token)
        {
            await _userRefreshTokenRepository.Update(token);
        }
        private string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            using(var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(byteArray);
            }
            return Convert.ToBase64String(byteArray);
        }
        private string GenerateToken(Core.User user)
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
                Expires = DateTime.UtcNow.AddSeconds(90),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public async Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string username)
        {
            var refreshToken = GenerateRefreshToken();
            var user = await _userService.GetByUsername(username);
            var accessToken = GenerateToken(user);
            return await SaveTokenDetails(ipAddress, userId, accessToken, refreshToken);
        }

        public async Task<bool> IsTokenValid(string accessToken, string ipAddress)
        {
            var isValid = await _userRefreshTokenRepository.IsTokenValid(accessToken, ipAddress);
            return await Task.FromResult(isValid);
        }
    }
}
