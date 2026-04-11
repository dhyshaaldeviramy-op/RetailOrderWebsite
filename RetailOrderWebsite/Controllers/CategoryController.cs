using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailOrderWebsite.DTOs.Catergory;
using RetailOrderWebsite.Services.Interfaces;
namespace RetailOrderWebsite.Controllers
{
  

    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto dto)
        {
            return Ok(await _service.Add(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto dto)
        {
            var result = await _service.Update(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result) return NotFound();
            return Ok("Deleted");
        }
    }
}
