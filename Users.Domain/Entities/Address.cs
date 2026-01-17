namespace Users.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public required Guid UserId { get; set; }

        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public required string City { get; set; }
        public string? StateProvince { get; set; } // Matches state_province
        public required string PostalCode { get; set; }
        public required string CountryCode { get; set; }

        public bool IsDefault { get; set; }
        public string AddressType { get; set; } = "shipping";
        public DateTime CreatedAt { get; init; }
    }
}
