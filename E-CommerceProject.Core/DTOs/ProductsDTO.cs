﻿using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Core.DTOs
{
    public class AddProductsDTO : BaseProductDto
    {
        public IFormFile? Image { get; set; }
    }
}
