using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMService.Models;

public class Address
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Street { get; set; }
    public string? City { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
}