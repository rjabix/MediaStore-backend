using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMService.Models
{
    public abstract class Product //info for card preview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public int? rating { get; set; }
        public int? reviews { get; set; }
        public List<string>? specialTags { get; set; }
        public string link { get; set; }
        public int? oldprice { get; set; }
        public string image { get; set; }
        public ProductCategory category { get; set; }
        public string? description { get; set; }
    }
}
