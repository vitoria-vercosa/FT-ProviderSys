namespace FT_ProviderSys.DTOs
{
    public record ProductUpdateRequestDTO
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
