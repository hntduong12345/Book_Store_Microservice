namespace Users.Domain.Entities
{
    public class User
    {
        //Unique Id will take from keycloak
        public required Guid Id { get; set; }

        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Address> Addresses { get; set; } = [];
    }
}
