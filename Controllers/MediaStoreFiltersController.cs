using Microsoft.AspNetCore.Mvc;
using MediaStore_backend.Models.Categories;

namespace MediaStore_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaStoreFiltersController : ControllerBase
    {
        private readonly ILogger<MediaStorePromosController> _logger;

        public MediaStoreFiltersController(ILogger<MediaStorePromosController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{category}")]
        public ActionResult<List<string>> Get(string category)
        {
            return category switch
            {
                "smartphones" => (ActionResult<List<string>>)GetPropertyNames<Smartphone>(),
                "laptops" => (ActionResult<List<string>>)GetPropertyNames<Laptop>(),
                _ => (ActionResult<List<string>>)NotFound(), //default
            };
        }

        private List<string> GetPropertyNames<T>()
        {
            return typeof(T).GetProperties().Select(p => p.Name).Where(p => Char.IsUpper(p[0])).ToList();
        }
    }
}
