using AutoMapper;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCalculationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _categoryService.Create(category);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryService.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            var result = await _categoryService.Update(category);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            return Ok(result);
        }
    }
}