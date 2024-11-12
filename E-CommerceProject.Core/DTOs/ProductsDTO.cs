﻿using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Core.DTOs
{
    public class ProductsDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}