using AutoMapper;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalorieCalculation.DataAccess.Sqlite.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CalorieCalculationContext _context;
        private readonly IMapper _mapper;

        public UserRepository(CalorieCalculationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> Create(User newUser)
        {
            if (await _context.Users.FirstOrDefaultAsync(x => x.Name == newUser.Name) != null)
            {
                throw new ArgumentException("user is exists");
            }
            var user = _mapper.Map<Core.User, Entities.User>(newUser);
            var result = await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<Entities.User, Core.User> (result.Entity);

        }
        public async Task<User> GetById(int id)
        {
            var result = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Entities.User, Core.User>(result);
        }
        public async Task<User> Update(User newUser)
        {
            var update = _mapper.Map<Core.User, Entities.User>(newUser);
            var result = _context.Users.Update(update);
            await _context.SaveChangesAsync();
            return _mapper.Map<Entities.User, Core.User>(result.Entity);
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            _context.Users.Remove(user);
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }




    }
}
