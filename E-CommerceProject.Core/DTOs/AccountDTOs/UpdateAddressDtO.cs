namespace E_CommerceProject.Core.DTOs.AccountDTOs
{
    public class UpdateAddressDtO
    {
        public int Id { get; set; } // Address ID
        public string AddressLine { get; set; } // Street or detailed address
        public string City { get; set; } // City name
        public string State { get; set; } // State or region
        public string PostalCode { get; set; } // Postal/ZIP code
        public string Country { get; set; } // Country name
    }
}
