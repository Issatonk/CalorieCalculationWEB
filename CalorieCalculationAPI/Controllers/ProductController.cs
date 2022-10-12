using AutoMapper;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCalculationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _productService.CreateProduct(product);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetProduct(id);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var result = await _productService.UpdateProduct(product);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.DeleteProduct(productId);
            return Ok(result);
        }
    }
}