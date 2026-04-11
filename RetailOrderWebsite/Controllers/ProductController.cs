using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailOrderWebsite.DTOs.Product;
using RetailOrderWebsite.Services.Interfaces;

namespace RetailOrderWebsite.Controllers
{
   

    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllProducts());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateDto dto)
        {
            return Ok(await _service.AddProduct(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
        {
            var result = await _service.UpdateProduct(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteProduct(id);
            if (!result) return NotFound();
            return Ok("Deleted");
        }
    }
}
