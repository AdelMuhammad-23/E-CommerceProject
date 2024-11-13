﻿using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Core.DTOs
{
    public class UpdateProductDTO : BaseProductDto
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; }
    }
}
