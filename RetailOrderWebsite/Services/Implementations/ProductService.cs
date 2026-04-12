namespace RetailOrderWebsite.Services.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using RetailOrderWebsite.Data;
    using RetailOrderWebsite.DTOs.Product;
    using RetailOrderWebsite.Models;
    using RetailOrderWebsite.Services.Interfaces;

    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductResponseDto>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();
        }

        public async Task<ProductResponseDto> AddProduct(ProductCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var category = await _context.Categories.FindAsync(dto.CategoryId);

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = category?.Name
            };
        }

        public async Task<ProductResponseDto> UpdateProduct(int id, ProductUpdateDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            var category = await _context.Categories.FindAsync(dto.CategoryId);

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = category?.Name
            };
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
