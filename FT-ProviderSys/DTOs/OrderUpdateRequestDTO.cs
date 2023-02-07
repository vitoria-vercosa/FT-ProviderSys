namespace FT_ProviderSys.DTOs
{
    public record OrderUpdateRequestDTO
    {
        public int OrderId { get; set; }
        public string Code { get; set; }
        public IEnumerable<ProductQuantityRequestDTO> Products { get; set; }
        public int ProviderId { get; set; }
    }
}
