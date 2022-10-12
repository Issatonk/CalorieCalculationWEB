using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using CalorieCalculation.Core.Services;

namespace CalorieCalculation.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Create(User newUser)
        {
            if(newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }
            if(newUser.Id !=0
                || string.IsNullOrWhiteSpace(newUser.Name)
                || string.IsNullOrWhiteSpace(newUser.Password)
                || string.IsNullOrWhiteSpace(newUser.Email))
            {
                throw new ArgumentException(nameof(newUser));
            }
            var result = await _userRepository.Create(newUser);
            return result;
        }
        public async Task<User> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("InvalidId");
            }
            return await _userRepository.GetById(id);
        }

        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (user.Id <= 0
                || string.IsNullOrWhiteSpace(user.Name)
                || string.IsNullOrWhiteSpace(user.Password)
                || string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException(nameof(user));
            }
            var result = await _userRepository.Update(user);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }
            return await _userRepository.Delete(id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _userRepository.GetByUsername(username);
        }
    }
}