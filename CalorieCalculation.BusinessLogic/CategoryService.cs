using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using CalorieCalculation.Core.Services;

namespace CalorieCalculation.BusinessLogic
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Category> Create(Category newCategory)
        {
            if(newCategory == null)
            {
                throw new ArgumentNullException(nameof(newCategory));
            }
            if(newCategory.Id != 0
                || string.IsNullOrWhiteSpace(newCategory.Name))
            {
                throw new ArgumentException(nameof(newCategory));
            }
            var result = await _categoryRepository.Create(newCategory);

            return result;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> Update(Category newUser)
        {
            throw new NotImplementedException();
        }
    }
}