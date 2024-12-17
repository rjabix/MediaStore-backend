using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserMService.Contexts;

public sealed class StoreUser : IdentityUser
{
    [PersonalData]
    [MaxLength(50)]
    public string? Name { get; set; }

    public List<Guid> OrdersGuids { get; set; } = []; // List of Order_Guids

    public List<int> ProductWishList { get; set; } = []; // List of Product_Ids

    public List<int> ProductCart_Ids { get; set; } = []; // List of product_Ids

    public List<int> ProductCartQuantities { get; set; } = [];  // List of quantities


    // public List<string>? PaymentMethods { get; set; } // List of PaymentMethod_Ids

    // Addresses are stored in a Delivery Service
}
