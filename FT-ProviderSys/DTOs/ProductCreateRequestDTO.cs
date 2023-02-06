namespace FT_ProviderSys.DTOs
{
    public record ProductCreateRequestDTO
    {
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime RegistrationDate { get; } = DateTime.UtcNow;

    }
}
