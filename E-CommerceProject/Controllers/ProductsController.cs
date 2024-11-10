using AutoMapper;
using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.files;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileServies;
        private readonly IHttpContextAccessor _contextAccessor;


        public ProductsController(IProductRepository productRepository,
                                  IMapper mapper,
                                  IFileService fileServies,
                                  IHttpContextAccessor contextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileServies = fileServies;
            _contextAccessor = contextAccessor;
        }
        [HttpGet("ProductList")]
        public async Task<IActionResult> GetProductList()
        {
            var products = await _productRepository.GetListAsync();
            if (products == null)
                return NotFound("Not Found Product yet");
            var productMapping = _mapper.Map<IReadOnlyList<ProductsListDTO>>(products);
            return Ok(productMapping);
        }
        [HttpGet("Get-Product-By{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Not Found Product with this ID:{id}");
            return Ok(product);
        }
        [HttpDelete("Delete-Product-{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Not Found Product with this ID:{id}");
            var deleteProduct = _productRepository.DeleteAsync(product);
            return Ok("Delete Product Successfully");
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductsDTO productDTO)
        {
            var instructorMapping = _mapper.Map<Product>(productDTO);
            var result = await _productRepository.AddProductAsync(instructorMapping, productDTO.Image);
            switch (result)
            {
                case "this extension is not allowed":
                    return BadRequest("this extension is not allowed");
                case "this image is too big":
                    return BadRequest("this image is too big");
                case "FailedToUploadImage":
                    return BadRequest("FailedToUploadImage");
            }
            return Ok("Product added is done");
        }
    }
}
