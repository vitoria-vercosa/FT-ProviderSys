namespace FT_ProviderSys.DTOs
{
    public record ProviderCreateRequestDTO
    {
        public string CorporateName { get; set; }
        public string LegalEntityIdentifier { get; set; }
        public string State { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
    }
}
