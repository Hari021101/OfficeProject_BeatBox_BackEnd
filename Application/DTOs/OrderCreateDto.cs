namespace Application.DTOs;

public class OrderCreateDto
{
    public ShippingAddressDto ShippingAddress { get; set; }

    public string PaymentMethod { get; set; }

    public PaymentDetailsDto PaymentDetails { get; set; }
}

public class ShippingAddressDto
{
    public string FullName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
}

public class PaymentDetailsDto
{
    public string CardNumber { get; set; }
    public string Expiry { get; set; }
    public string Cvv { get; set; }
    public string TransactionReference { get; set; }
}