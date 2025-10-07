using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetById(int? id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            await _context.Products.Where(p => p.Id == product.Id)
                .Include(c => c.Category)
                .ExecuteUpdateAsync(setters => setters
                                     .SetProperty(p => p.Name, product.Name)
                                     .SetProperty(p => p.Description, product.Description)
                                     .SetProperty(p => p.Price, product.Price)
                                     .SetProperty(p => p.Stock, product.Stock)
                                     .SetProperty(p => p.Image, product.Image)
                                     .SetProperty(p => p.Category, product.Category));
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
