namespace E_CommerceProject.Core.DTOs
{
    public class AddAddressDTO
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
