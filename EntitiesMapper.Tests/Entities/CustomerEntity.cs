namespace EntitiesMapper.Tests.Entities
{
    public class CustomerEntity
    {
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TaxCode { get; set; }
        public string VatNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyVatNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentCredit { get; set; }
        public string CustomerType { get; set; }
        public string CustomerCategory { get; set; }
        public string PaymentTerms { get; set; }
        public string ShippingPreference { get; set; }
        public string PreferredCurrency { get; set; }
        public string Language { get; set; }
        public string TimeZone { get; set; }
        public bool NewsletterSubscription { get; set; }
        public bool MarketingPreference { get; set; }
        public string Notes { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public decimal TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int LoyaltyPoints { get; set; }
        public string CustomerSegment { get; set; }
        public string ReferralSource { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
    }
}
