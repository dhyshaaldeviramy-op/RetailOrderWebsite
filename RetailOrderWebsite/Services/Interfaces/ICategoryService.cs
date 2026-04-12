using RetailOrderWebsite.DTOs.Catergory;

namespace RetailOrderWebsite.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> Add(CategoryDto dto);
        Task<CategoryDto> Update(int id, CategoryDto dto);
        Task<bool> Delete(int id);
    }
}
