namespace FT_ProviderSys.DTOs
{
    public record OrderUpdateRequestDTO
    {
        public int OrderId { get; set; }
        public string Code { get; set; }
        public IEnumerable<int> ProductIds { get; set; }
        public int ProviderId { get; set; }
        public float Amount { get; set; }
    }
}
