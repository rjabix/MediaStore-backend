using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaStore_backend.Services;

namespace MediaStore_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MediaStoreGeoLocationController : ControllerBase
    {
        private readonly ILogger<MediaStoreGeoLocationController> _logger;
        private readonly GeoLocationService _geoLocationService;

        public MediaStoreGeoLocationController(ILogger<MediaStoreGeoLocationController> logger, GeoLocationService geoLocationService)
        {
            _logger = logger;
            _geoLocationService = geoLocationService;
        }

        [HttpGet("city")]
        public async Task<IActionResult> GetCityByCoords(double lat, double lon, string? lang)
        {
            if (lat < -90 || lat > 90 || lon < -180 || lon > 180)
            {
                return BadRequest("Invalid coordinates");
            }
            var city = await _geoLocationService.GetCityByCoordsAsync(lat, lon, lang);

            if (city == null)
            {
                return BadRequest("Not found");
            }

            return Ok(city);
        }
    }
}
