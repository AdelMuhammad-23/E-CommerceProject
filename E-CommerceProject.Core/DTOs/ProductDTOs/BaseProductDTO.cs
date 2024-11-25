namespace E_CommerceProject.Core.DTOs.ProductDTOs
{
    public class BaseProductDto
    {
        public string? Name { get; set; }
        public string Description { get; set; }
        public string? CategoryName { get; set; }
        public int? Stock { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
