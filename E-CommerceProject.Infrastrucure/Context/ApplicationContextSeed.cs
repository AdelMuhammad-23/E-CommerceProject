using E_CommerceProject.Core.Entities;
using Newtonsoft.Json;

namespace E_CommerceProject.Infrastructure.Context
{
    public class ApplicationContextSeed
    {
        public class CategoryData
        {
            public List<Category> Categories { get; set; }
        }
        public class ProductData
        {
            public List<Product> Products { get; set; }
        }

        public static List<Category> LoadCategoriesFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var categoryData = JsonConvert.DeserializeObject<CategoryData>(json);
            return categoryData.Categories;
        }


        public static List<Product> LoadProductsFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var productData = JsonConvert.DeserializeObject<ProductData>(json);
            return productData.Products;
        }
    }
}
