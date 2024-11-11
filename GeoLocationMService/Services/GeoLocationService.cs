
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoLocationMService.Services
{
    public class GeoLocationService(IConfiguration configuration)
    {
        private readonly string _GEO_API_KEY = configuration["GEOLOCATION_API_KEY"] ?? throw new Exception("No GEO API Key provided.");

        //classes for deserialization
        private class Address
        {
            [JsonPropertyName("city")]
            public string City { get; set; }

            [JsonPropertyName("state")]
            public string State { get; set; }

            [JsonPropertyName("countryName")]
            public string Country { get; set; }
        }

        private class Item
        {
            [JsonPropertyName("address")]
            public Address Address { get; set; }
        }

        private class Response
        {
            [JsonPropertyName("items")]
            public Item[] Items { get; set; }
        }
        public async Task<string?> GetCityByCoordsAsync(double lat, double lon, string? lang)
        {
            string slat = lat.ToString().Replace(',', '.');
            string slon = lon.ToString().Replace(',', '.');
            lang ??= "en-EN";
            var client = new HttpClient();
            var response = await client.GetAsync($"https://discover.search.hereapi.com/v1/discover?at={slat},{slon}&q={slat},{slon}&lang={lang}&apiKey={_GEO_API_KEY}&limit=1");
            var content = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<Response>(content);
            if(responseJson == null || responseJson.Items.Length == 0)
            {
                return null;
            }
            return $"{responseJson.Items[0].Address.City}, {responseJson.Items[0].Address.State}, {responseJson.Items[0].Address.Country}";
        }
    }
}
