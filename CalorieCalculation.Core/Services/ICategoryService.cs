namespace CalorieCalculation.Core.Services
{
    public interface ICategoryService
    {
        Task<Category> Create(Category newUser);
        Task<Category> GetById(int id);
        Task<Category> Update(Category newUser);
        Task<bool> Delete(int id);
    }
}
