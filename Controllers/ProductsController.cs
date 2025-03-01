using Microsoft.AspNetCore.Mvc;
using SpaceBasedPatternApi.Models;
using SpaceBasedPatternApi.Services;

namespace SpaceBasedPatternApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET api/products/get/{key}
        [HttpGet]
        public async Task<IActionResult> Get(string key)
        {
            var value =  await _productService.GetAsync(key);
            if (value == null)
            {
                return NotFound(); // Return 404 if value is not found
            }
            return Ok(value); // Return 200 with the value if found
        }

        // POST api/products/add
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductRequest request)
        {
            // Check if the incoming data is valid
            if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Value))
            {
                return BadRequest("Key and value cannot be empty."); // Return 400 if invalid input
            }

            var result=await _productService.GetOrAddAsync(request.Key, request.Value);
            return Ok(result); // Return 200 with the result
        }
    }
}
