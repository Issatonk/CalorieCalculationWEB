namespace CalorieCalculation.Core.Services
{
    public interface IUserService
    {
        Task<User> Create(User newUser);
        Task<User> GetById(int id);
        Task<User> Update(User newUser);
        Task<bool> Delete(int id);

        Task<User> GetByUsername(string username);

    }
}
