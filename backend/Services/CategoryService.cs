using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos;
using server.Models;
using server.Services.Interfaces;

namespace server.Services
{
    public class CategoryService(AppDbContext context) : ICategory
    {
        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            return await context.Categories
                .Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category is null) return null;

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest category)
        {
            var newCategory = new Category
            {
                Name = category.Name,
                Description = category.Description
            };

            context.Categories.Add(newCategory);
            await context.SaveChangesAsync();

            return new CategoryResponse
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description
            };
        }

        public async Task<bool> UpdateCategoryAsync(int id, CreateCategoryRequest category)
        {
            var existing = await context.Categories.FindAsync(id);
            if (existing is null) return false;

            existing.Name = category.Name;
            existing.Description = category.Description;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category is null) return false;

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
