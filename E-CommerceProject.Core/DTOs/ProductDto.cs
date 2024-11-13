namespace E_CommerceProject.Core.DTOs
{
    public class ProductDto : BaseProductDto
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }

    }

}
