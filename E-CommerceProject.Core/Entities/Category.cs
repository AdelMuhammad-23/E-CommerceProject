namespace E_CommerceProject.Core.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
