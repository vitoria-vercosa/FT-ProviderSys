namespace FT_ProviderSys.DTOs
{
    public record OrderCreateRequestDTO
    {
        public string Code { get; set; }
        public IEnumerable<ProductQuantityRequestDTO> Products { get; set; }
        public int ProviderId { get; set; }
    }
}
