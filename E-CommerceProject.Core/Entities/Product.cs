namespace E_CommerceProject.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsAvailable => QuantityInStock > 0;

    }

}
