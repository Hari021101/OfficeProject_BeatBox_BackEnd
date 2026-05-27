namespace Domain.Entities;

public class UserAddress
{
    public int UserAddressId { get; set; }
    public string UserId { get; set; } // Foreign Key to AppUser
    
    public string FullName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }

    public bool IsDefault { get; set; }

    public AppUser User { get; set; }
}
