using E_CommerceProject.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("ProductList")]
        public async Task<IActionResult> GetProductList()
        {
            var products = await _productRepository.GetProductsListAsync();
            if (products == null)
                return NotFound("Not Found Product yet");
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound($"Not Found Product with this ID:{id}");
            return Ok(product);
        }
    }
}
