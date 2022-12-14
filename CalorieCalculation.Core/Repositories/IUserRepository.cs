namespace CalorieCalculation.Core.Repositories
{
    public interface IUserRepository 
    {
        Task<User> Create(User newUser);
        Task<User> GetById(int id);
        Task<User> Update(User newUser);
        Task<bool> Delete(int id);
    }
}
