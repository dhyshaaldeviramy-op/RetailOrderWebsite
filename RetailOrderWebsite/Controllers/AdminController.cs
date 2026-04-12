using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailOrderWebsite.Services.Interfaces;

namespace RetailOrderWebsite.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IProductService _service;

        public AdminController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _service.GetAllProducts());
        }
    }
}
