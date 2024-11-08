using System.Globalization;

namespace MediaStore_backend.Models
{
    public enum ProductCategory
    {
        Smartphones,
        Laptops,
        Tablets,
        TVs,
        Monitors,
        Headphones,
        Computers,
        Accessories,
        VR,
        Watches
    }

    public static class ProductExtensions
    {
        public static ProductCategory ToProductCategory(this string category)
        {
            return category switch
            {
                "smartphones" => ProductCategory.Smartphones,
                "laptops" => ProductCategory.Laptops,
                "tablets" => ProductCategory.Tablets,
                "tvs" => ProductCategory.TVs,
                "monitors" => ProductCategory.Monitors,
                "headphones" => ProductCategory.Headphones,
                "computers" => ProductCategory.Computers,
                "accessories" => ProductCategory.Accessories,
                "vr" => ProductCategory.VR,
                "watches" => ProductCategory.Watches,
                _ => throw new ArgumentException("Category not found", nameof(category))
            };
        }
        
        public static string ToCategoryString(this ProductCategory category)
        {
            return category switch
            {
                ProductCategory.Smartphones => "smartphones",
                ProductCategory.Laptops => "laptops",
                ProductCategory.Tablets => "tablets",
                ProductCategory.TVs => "tvs",
                ProductCategory.Monitors => "monitors",
                ProductCategory.Headphones => "headphones",
                ProductCategory.Computers => "computers",
                ProductCategory.Accessories => "accessories",
                ProductCategory.VR => "vr",
                ProductCategory.Watches => "watches",
                _ => throw new ArgumentException("Category not found", nameof(category))
            };
        }
        
        public static Type? GetCategoryType(this string category)
        {
            // Capitalize the first letter of the category
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            category = textInfo.ToTitleCase(category);

            // Remove the last letter 's' if it exists
            if (category.EndsWith("s"))
            {
                category = category.Remove(category.Length - 1);
            }

            // Get the type
            Type categoryType = Type.GetType($"MediaStore_backend.Models.Categories.{category}", throwOnError: false);
            return categoryType;
        }
    }
}
