using AutoMapper;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCalculation.DataAccess.Sqlite.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CalorieCalculationContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(CalorieCalculationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if (await _context.Products.FirstOrDefaultAsync(x => x.Name == product.Name) != null)
            {
                throw new ArgumentException("product is exists");
            }
            var newProduct = _mapper.Map<Core.Product, Entities.Product>(product);
            var result = await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return _mapper.Map<Entities.Product, Core.Product>(result.Entity);
        }
        public async Task<Product> GetProduct(int id)
        {
            var result = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Entities.Product, Core.Product>(result);
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var result = await _context.Products.AsNoTracking().ToArrayAsync();
            return _mapper.Map<Entities.Product[], Core.Product[]>(result);
        }
        public async Task<Product> UpdateProduct(Product product)
        {
            var update = _mapper.Map<Core.Product, Entities.Product>(product);
            var result = _context.Products.Update(update);
            await _context.SaveChangesAsync();
            return _mapper.Map<Entities.Product, Core.Product>(result.Entity);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            _context.Products.Remove(product);
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }
    }
}
