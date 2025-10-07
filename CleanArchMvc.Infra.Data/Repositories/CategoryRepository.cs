using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetById(int? id)
        {
            return await _context.categories.FindAsync(id);
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.categories.ToListAsync();
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> DeleteCategoryAsync(Category category)
        {
            _context.categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.categories.Update(category);
            await _context.SaveChangesAsync();
            return category;

        }
    }
}
