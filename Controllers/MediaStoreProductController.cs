using MediaStore_backend.Models;
using MediaStore_backend.Models.Categories;
using MediaStore_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;

namespace MediaStore_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaStoreProductController : ControllerBase
    {
        private readonly ILogger<MediaStorePromosController> _logger;
        private readonly ProductService _productService;
        public MediaStoreProductController(ILogger<MediaStorePromosController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularProducts()
        {

            var products = await _productService.GetPopularProductsAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            // ---Capitalize the first letter of the category
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            category = textInfo.ToTitleCase(category);

            var products = await _productService.GetProductByCategory(category);


            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            var productType = category.GetCategoryType();
            if (productType == null)
            {
                return NotFound($"Category '{category}' is not supported.");
            }

            // Check if all products are of the expected type
            if (products.All(p => p.GetType() == productType))
            {
                // Create a non-generic list to hold the casted products
                var typedProductsList = (System.Collections.IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(productType));
                foreach (var product in products)
                {
                    typedProductsList.Add(Convert.ChangeType(product, productType));
                }

                return Ok(typedProductsList);
            }

            return Ok(products);
        }

        [HttpGet("{category}/{id}")]
        public async Task<IActionResult> GetProduct(string category, int id)
        {
            // ---Capitalize the first letter of the category
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            category = textInfo.ToTitleCase(category);

            var product = await _productService.GetProductByCategoryAndIdAsync(category, id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("{category}/{id}/description")]
        public async Task<IActionResult> GetProductDescription(string category, int id)
        {
            // ---Capitalize the first letter of the category
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            category = textInfo.ToTitleCase(category);
            try
            {
                var description = await _productService.GetProduct_Description_ByCategoryAndIdAsync(category, id); // Get the description property
                return Ok(description);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        


        //[HttpPost]
        //public async Task<IActionResult> AddProduct(Product item)
        //{
        //    var newItem = await _productService.AddProductAsync(item);
        //    return CreatedAtAction(nameof(GetProduct), new { id = newItem.id }, newItem);
        //}
    }
}
