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

        public ProductsController(IProductRepository productRepository,
                                  IMapper mapper,
                                  IFileService fileServies)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileServies = fileServies;
        }
        [HttpGet("ProductList")]
        public async Task<IActionResult> GetProductList()
        {
            var products = await _productRepository.GetProductListAsync();
            if (products == null)
                return NotFound("Not Found Product yet");
            return Ok(products);
        }
        [HttpGet("Get-Product-By{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Not Found Product with this ID:{id}");
            return Ok(product);
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
        [HttpDelete("Delete-Product-{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Not Found Product with this ID: {id}");

            await _productRepository.DeleteAsync(product);
            return Ok("Delete Product Successfully");
        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDTO productDTO)
        {
            var product = await _productRepository.GetByIdAsync(productDTO.Id);
            if (product == null)
                return NotFound($"Not Found Product with this ID: {productDTO.Id}");
            var productMapping = _mapper.Map(productDTO, product);
            var result = await _productRepository.UpdateProductAsync(productMapping, productDTO.Image);
            switch (result)
            {
                case "this extension is not allowed":
                    return BadRequest("this extension is not allowed");
                case "this image is too big":
                    return BadRequest("this image is too big");
                case "FailedToUploadImage":
                    return BadRequest("FailedToUploadImage");
            }
            return Ok("Update Product Successfully");
        }
    }
}
