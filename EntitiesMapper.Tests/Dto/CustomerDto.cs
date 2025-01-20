using EntitiesMapper.CustomAttribute;
using EntitiesMapper.Tests.Entities;

namespace EntitiesMapper.Tests.Dto
{
    public class CustomerDTO
    {
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CreatedAt))]
        public DateTime Created { get; set; }
        public int CustomerId { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.FirstName))]
        public string Name { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.LastName))]
        public string Surname { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.Email))]
        public string EmailAddress { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.PhoneNumber))]
        public string Phone { get; set; }
        public string FullAddress { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CompanyName))]
        public string Business { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CompanyVatNumber))]
        public string BusinessVat { get; set; }
        public string BusinessFullAddress { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CreditLimit))]
        public decimal MaxCredit { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CurrentCredit))]
        public decimal AvailableCredit { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CustomerType))]
        public string Type { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CustomerCategory))]
        public string Category { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.PaymentTerms))]
        public string PaymentConditions { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.ShippingPreference))]
        public string PreferredShipping { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.PreferredCurrency))]
        public string Currency { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.Language))]
        public string PreferredLanguage { get; set; }
        public string Zone { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.NewsletterSubscription))]
        public bool NewsletterEnabled { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.MarketingPreference))]
        public bool MarketingEnabled { get; set; }
        public string Comments { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.UpdatedAt))]
        public DateTime? Modified { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CreatedBy))]
        public string Creator { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.UpdatedBy))]
        public string Modifier { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.IsActive))]
        public bool Active { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.LastOrderDate))]
        public DateTime? LastPurchase { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.TotalOrders))]
        public decimal TotalPurchases { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.AverageOrderValue))]
        public decimal AveragePurchase { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.LoyaltyPoints))]
        public int Points { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CustomerSegment))]
        public string Segment { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.ReferralSource))]
        public string ReferredBy { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.Tags))]
        public List<string> Labels { get; set; }
        [MapTo<CustomerEntity>(nameof(CustomerEntity.CustomFields))]
        public Dictionary<string, string> AdditionalFields { get; set; }
    }
}
