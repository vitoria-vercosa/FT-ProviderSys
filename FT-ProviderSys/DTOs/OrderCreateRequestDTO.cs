namespace FT_ProviderSys.DTOs
{
    public record OrderCreateRequestDTO
    {
        public string Code { get; set; }
        public IEnumerable<int> ProductIds { get; set; }
        public int ProviderId { get; set; }
        public float Amount { get; set; }
    }
}
