using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using CalorieCalculation.Core.Services;

namespace CalorieCalculation.BusinessLogic
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Product> CreateProduct(Product product)
        {

            if (product == null)
            {
                throw new ArgumentNullException("Product is null");
            }
            if (product.Id != 0
                || String.IsNullOrWhiteSpace(product.Name)
                || product.Category == null
                || product.Calories < 0
                || product.Carbohydrates < 0
                || product.Proteins < 0
                || product.Fats <0
                || product.Picture == null)
            {
                throw new ArgumentException("Argument is invalid");
            }
            var result =  await _repository.CreateProduct(product);
            return result;
        }
        public async Task<Product> GetProduct(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            var result = await _repository.GetProduct(id);

            return result;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _repository.GetAllProducts();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product is null");
            }
            if (product.Id <= 0
                || String.IsNullOrWhiteSpace(product.Name)
                || product.Category == null
                || product.Calories < 0
                || product.Carbohydrates < 0
                || product.Proteins < 0
                || product.Fats < 0
                || product.Picture == null)
            {
                throw new ArgumentException("Argument is invalid");
            }
            var result = await _repository.UpdateProduct(product);
            return result;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid id");
            return await _repository.DeleteProduct(id);
        }
    }
}