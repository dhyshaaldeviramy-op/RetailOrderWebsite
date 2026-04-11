namespace RetailOrderWebsite.Services.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using RetailOrderWebsite.Data;
    using RetailOrderWebsite.DTOs.Catergory;
    using RetailOrderWebsite.Models;
    using RetailOrderWebsite.Services.Interfaces;

    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();
        }

        public async Task<CategoryDto> Add(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            dto.Id = category.Id;
            return dto;
        }
        public async Task<CategoryDto> Update(int id, CategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
