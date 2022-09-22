namespace CalorieCalculation.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> Create(Category newUser);
        Task<Category> GetById(int id);
        Task<Category> Update(Category newUser);
        Task<bool> Delete(int id);
    }
}
