namespace FT_ProviderSys.DTOs
{
    public record ProviderUpdateRequestDTO
    {
        public int ProviderId { get; set; }
        public string CorporateName { get; set; }
        public string LegalEntityIdentifier { get; set; }
        public string State { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
    }
}
