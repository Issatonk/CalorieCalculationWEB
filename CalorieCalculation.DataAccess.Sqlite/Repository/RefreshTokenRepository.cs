using CalorieCalculation.DataAccess.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CalorieCalculation.DataAccess.Sqlite.Repository
{
    public interface IUserRefreshTokenRepository
    {
        Task AddAsync(UserRefreshToken token);

        Task<UserRefreshToken> Get(UserRefreshToken token);

        Task Update(UserRefreshToken token);

        Task<bool> IsTokenValid(string accessToken, string ipAddress);
    }
    public class RefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly CalorieCalculationContext _context;

        public RefreshTokenRepository(CalorieCalculationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserRefreshToken token)
        {
            await _context.UserRefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRefreshToken> Get(UserRefreshToken token)
        {
            var refreshToken = await _context.UserRefreshTokens.FirstOrDefaultAsync(
                x => x.IsInvalidated == token.IsInvalidated 
                && x.Token == token.Token
                && x.RefreshToken == token.RefreshToken
                && x.IpAddress == token.IpAddress
                );
            return refreshToken;
        }

        public async Task<bool> IsTokenValid(string accessToken, string ipAddress)
        {
            var isValid = await _context.UserRefreshTokens.FirstOrDefaultAsync(
                x=>x.Token == accessToken && x.IpAddress == ipAddress ) != null;
            return isValid;
        }

        public async Task Update(UserRefreshToken token)
        {
            _context.UserRefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
