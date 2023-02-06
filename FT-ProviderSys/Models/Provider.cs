namespace FT_ProviderSys.Models
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string CorporateName { get; set; }
        public string LegalEntityIdentifier { get; set; } 
        public string? State { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactName { get; set; }

        public Provider()
        {

        }
        public Provider(string corporateName, string legalEntityIdentifier, 
                        string State, string contactEmail, string contactName)
        {
            this.CorporateName = corporateName;
            this.LegalEntityIdentifier = legalEntityIdentifier;
            this.State = State;
            this.ContactEmail = contactEmail;
            this.ContactName = contactName;
        }
    }
}
