using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Core.DTOs.ProductDTOs
{
    public class AddProductsDTO : BaseProductDto
    {
        public IFormFile? Image { get; set; }
    }
}
