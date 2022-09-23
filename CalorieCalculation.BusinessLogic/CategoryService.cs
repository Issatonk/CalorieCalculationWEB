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
        public async Task<Category> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            return await _categoryRepository.GetById(id);
        }
        public async Task<Category> Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (category.Id <= 0
                || string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException(nameof(category));
            }
            var result = await _categoryRepository.Update(category);

            return result;
        }
        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            return await _categoryRepository.Delete(id);
        }



     
    }
}