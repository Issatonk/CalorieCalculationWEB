using AutoMapper;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalorieCalculation.DataAccess.Sqlite.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CalorieCalculationContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(CalorieCalculationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Category> Create(Category category)
        {
            var newCategory = _mapper.Map<Core.Category, Entities.Category>(category);
            var result = await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return _mapper.Map<Entities.Category,Core.Category>(result.Entity);
        }

        public async Task<Category> GetById(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Entities.Category, Core.Category>(result);
        }

        public async Task<Category> Update(Category category)
        {
            var data = _mapper.Map<Core.Category, Entities.Category>(category);
            var result = _context.Categories.Update(data);
            await _context.SaveChangesAsync();
            return _mapper.Map<Entities.Category, Core.Category>(result.Entity);
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _context.Categories.Remove(category);
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }


    }
}
