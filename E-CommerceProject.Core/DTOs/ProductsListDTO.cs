namespace E_CommerceProject.Core.DTOs
{
    public class ProductsListDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
