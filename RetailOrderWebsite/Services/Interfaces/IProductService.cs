using RetailOrderWebsite.DTOs.Product;

namespace RetailOrderWebsite.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProducts();
        Task<ProductResponseDto> AddProduct(ProductCreateDto dto);
        Task<ProductResponseDto> UpdateProduct(int id, ProductUpdateDto dto);
        Task<bool> DeleteProduct(int id);
    }
}
