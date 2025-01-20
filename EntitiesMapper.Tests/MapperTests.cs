using System.Diagnostics;
using EntitiesMapper.Tests.Dto;
using EntitiesMapper.Tests.Entities;
using EntitiesMapper.Tests.Fakes;
using EntitiesMapper.Tests.Helpers;

namespace EntitiesMapper.Tests
{
    [TestClass]
    public class MapperTests
    {
        #region PersonEntity properties
        static readonly PersonEntity JohnDoePersonEntity = new()
        {
            Name = "John",
            Surname = "Doe",
            Age = 30,
            Rule = "Ingegnere",
            BirthdayYear = 1992,
            Address = "Via del Sole, 123"
        };

        static readonly PersonEntity MarioRossiPersonEntity = new()
        {
            Name = "Mario",
            Surname = "Rossi",
            Age = 18,
            Rule = "Developer",
            BirthdayYear = 2013,
            Address = "Via della Luna, 456"
        };

        static readonly List<PersonEntity> FriendsPersonsEntities = new()
        {
            JohnDoePersonEntity,
            MarioRossiPersonEntity
        };
        #endregion

        #region PersonEntity2 properties
        static readonly PersonEntity2 JohnDoePersonEntity2 = new()
        {
            NomeNascita = "John",
            SurnameNascita = "Doe",
            Et‡Attuale = 30,
            ProfessioneAttuale = "Ingegnere"
        };

        static readonly PersonEntity2 MarioRossiPersonEntity2 = new()
        {
            NomeNascita = "Mario",
            SurnameNascita = "Rossi",
            Et‡Attuale = 18,
            ProfessioneAttuale = "Developer"
        };

        static readonly List<PersonEntity2> FriendsPersonsEntities2 = new()
        {
            JohnDoePersonEntity2,
            MarioRossiPersonEntity2
        };
        #endregion

        [TestMethod("Copia di un oggetto in un altro dello stesso tipo")]
        public void Test1()
        {
            using var timer = new StopWatchOperationHelper("Test1");
            Mapper.CopyObject(JohnDoePersonEntity, out PersonEntity anotherJohnDoe);

            Assert.AreEqual(JohnDoePersonEntity.Name, anotherJohnDoe.Name);
            Assert.AreEqual(JohnDoePersonEntity.Surname, anotherJohnDoe.Surname);
            Assert.AreEqual(JohnDoePersonEntity.Age, anotherJohnDoe.Age);
            Assert.AreEqual(JohnDoePersonEntity.Rule, anotherJohnDoe.Rule);
            Assert.AreEqual(JohnDoePersonEntity.BirthdayYear, anotherJohnDoe.BirthdayYear);
            Assert.AreEqual(JohnDoePersonEntity.Address, anotherJohnDoe.Address);
        }

        [TestMethod("Copia di un oggetto in un altro di tipo diverso")]
        public void Test2()
        {
            using var timer = new StopWatchOperationHelper("Test2");
            Mapper.CopyObject(JohnDoePersonEntity, out PersonDto anotherJohnDoe);

            Assert.AreEqual(JohnDoePersonEntity.Name, anotherJohnDoe.Nome);
            Assert.AreEqual(JohnDoePersonEntity.Surname, anotherJohnDoe.Cognome);
            Assert.AreEqual(JohnDoePersonEntity.Age, anotherJohnDoe.Et‡);
            Assert.AreEqual(JohnDoePersonEntity.Rule, anotherJohnDoe.Professione);
            Assert.AreEqual(JohnDoePersonEntity.BirthdayYear, anotherJohnDoe.AnnoNascita);
            Assert.AreEqual(JohnDoePersonEntity.Address, anotherJohnDoe.ViaResidenza);
        }

        [TestMethod("Copia di una lista in un'altra dello stesso tipo")]
        public void Test3()
        {
            using var timer = new StopWatchOperationHelper("Test3");
            Mapper.CopyList<PersonEntity, PersonEntity>(FriendsPersonsEntities, out var anotherFriendsPersonsEntities);

            for (int friendIndex = 0; friendIndex < anotherFriendsPersonsEntities.Count; friendIndex++)
            {
                var currentSourceFriend = FriendsPersonsEntities[friendIndex];
                var currentAnotherFriend = anotherFriendsPersonsEntities.ElementAt(friendIndex);

                Assert.AreEqual(currentSourceFriend.Name, currentAnotherFriend.Name);
                Assert.AreEqual(currentSourceFriend.Surname, currentAnotherFriend.Surname);
                Assert.AreEqual(currentSourceFriend.Age, currentAnotherFriend.Age);
                Assert.AreEqual(currentSourceFriend.Rule, currentAnotherFriend.Rule);
                Assert.AreEqual(currentSourceFriend.BirthdayYear, currentAnotherFriend.BirthdayYear);
                Assert.AreEqual(currentSourceFriend.Address, currentAnotherFriend.Address);
            }
        }
        [TestMethod("Copia di una lista in un'altra di tipo diverso")]
        public void Test4()
        {
            using var timer = new StopWatchOperationHelper("Test4");
            Mapper.CopyList<PersonEntity, PersonDto>(FriendsPersonsEntities, out var anotherFriendsPersonsEntities);

            for (int friendIndex = 0; friendIndex < anotherFriendsPersonsEntities.Count; friendIndex++)
            {
                var currentSourceFriend = FriendsPersonsEntities[friendIndex];
                var currentAnotherFriend = anotherFriendsPersonsEntities.ElementAt(friendIndex);

                Assert.AreEqual(currentSourceFriend.Name, currentAnotherFriend.Nome);
                Assert.AreEqual(currentSourceFriend.Surname, currentAnotherFriend.Cognome);
                Assert.AreEqual(currentSourceFriend.Age, currentAnotherFriend.Et‡);
                Assert.AreEqual(currentSourceFriend.Rule, currentAnotherFriend.Professione);
                Assert.AreEqual(currentSourceFriend.BirthdayYear, currentAnotherFriend.AnnoNascita);
                Assert.AreEqual(currentSourceFriend.Address, currentAnotherFriend.ViaResidenza);
            }
        }

        [TestMethod("Copia di una lista in un'altra di tipo diverso con una propriet‡ ignorata durante il mapping")]
        public void Test5()
        {
            using var timer = new StopWatchOperationHelper("Test5");
            Mapper.CopyList<PersonEntity2, PersonDto>(FriendsPersonsEntities2, out var anotherFriendsPersonsEntities2);

            for (int friendIndex = 0; friendIndex < anotherFriendsPersonsEntities2.Count; friendIndex++)
            {
                var currentSourceFriend = FriendsPersonsEntities2[friendIndex];
                var currentAnotherFriend = anotherFriendsPersonsEntities2.ElementAt(friendIndex);

                Assert.AreEqual(currentSourceFriend.NomeNascita, currentAnotherFriend.Nome);
                Assert.AreEqual(currentSourceFriend.SurnameNascita, currentAnotherFriend.Cognome);
                Assert.AreEqual(currentSourceFriend.Et‡Attuale, currentAnotherFriend.Et‡);

                //Non verr‡ copiata perchË contiene null nella definizione del MapToAttribute
                Assert.IsNull(currentAnotherFriend.Professione);

                Assert.IsTrue(string.IsNullOrEmpty(currentAnotherFriend.ViaResidenza));
                Assert.IsTrue(currentAnotherFriend.AnnoNascita is 0);
            }
        }

        [TestMethod("Copia di un oggetto creato al volo in un altro dello stesso tipo.")]
        public void Test6()
        {
            var customerEntity = CreateFake.ObjectOfType<CustomerEntity>();
            using var timer = new StopWatchOperationHelper("Test6");
            Mapper.CopyObject<CustomerEntity, CustomerEntity>(customerEntity, out var sameItemCopied);

            // Basic Info
            Assert.AreEqual(customerEntity.Id, sameItemCopied.Id);
            Assert.AreEqual(customerEntity.FirstName, sameItemCopied.FirstName);
            Assert.AreEqual(customerEntity.LastName, sameItemCopied.LastName);
            Assert.AreEqual(customerEntity.Email, sameItemCopied.Email);
            Assert.AreEqual(customerEntity.PhoneNumber, sameItemCopied.PhoneNumber);

            // Address
            Assert.AreEqual(customerEntity.Address, sameItemCopied.Address);
            Assert.AreEqual(customerEntity.City, sameItemCopied.City);
            Assert.AreEqual(customerEntity.Country, sameItemCopied.Country);
            Assert.AreEqual(customerEntity.PostalCode, sameItemCopied.PostalCode);
            Assert.AreEqual(customerEntity.DateOfBirth, sameItemCopied.DateOfBirth);

            // Tax Info
            Assert.AreEqual(customerEntity.TaxCode, sameItemCopied.TaxCode);
            Assert.AreEqual(customerEntity.VatNumber, sameItemCopied.VatNumber);

            // Company Info
            Assert.AreEqual(customerEntity.CompanyName, sameItemCopied.CompanyName);
            Assert.AreEqual(customerEntity.CompanyAddress, sameItemCopied.CompanyAddress);
            Assert.AreEqual(customerEntity.CompanyCity, sameItemCopied.CompanyCity);
            Assert.AreEqual(customerEntity.CompanyCountry, sameItemCopied.CompanyCountry);
            Assert.AreEqual(customerEntity.CompanyPostalCode, sameItemCopied.CompanyPostalCode);
            Assert.AreEqual(customerEntity.CompanyEmail, sameItemCopied.CompanyEmail);
            Assert.AreEqual(customerEntity.CompanyPhoneNumber, sameItemCopied.CompanyPhoneNumber);
            Assert.AreEqual(customerEntity.CompanyVatNumber, sameItemCopied.CompanyVatNumber);

            // Financial Info
            Assert.AreEqual(customerEntity.CreditLimit, sameItemCopied.CreditLimit);
            Assert.AreEqual(customerEntity.CurrentCredit, sameItemCopied.CurrentCredit);
            Assert.AreEqual(customerEntity.CustomerType, sameItemCopied.CustomerType);
            Assert.AreEqual(customerEntity.CustomerCategory, sameItemCopied.CustomerCategory);
            Assert.AreEqual(customerEntity.PaymentTerms, sameItemCopied.PaymentTerms);

            // Preferences
            Assert.AreEqual(customerEntity.ShippingPreference, sameItemCopied.ShippingPreference);
            Assert.AreEqual(customerEntity.PreferredCurrency, sameItemCopied.PreferredCurrency);
            Assert.AreEqual(customerEntity.Language, sameItemCopied.Language);
            Assert.AreEqual(customerEntity.TimeZone, sameItemCopied.TimeZone);
            Assert.AreEqual(customerEntity.NewsletterSubscription, sameItemCopied.NewsletterSubscription);
            Assert.AreEqual(customerEntity.MarketingPreference, sameItemCopied.MarketingPreference);

            // Notes and Timestamps
            Assert.AreEqual(customerEntity.Notes, sameItemCopied.Notes);
            Assert.AreEqual(customerEntity.CreatedAt, sameItemCopied.CreatedAt);
            Assert.AreEqual(customerEntity.UpdatedAt, sameItemCopied.UpdatedAt);
            Assert.AreEqual(customerEntity.CreatedBy, sameItemCopied.CreatedBy);
            Assert.AreEqual(customerEntity.UpdatedBy, sameItemCopied.UpdatedBy);
            Assert.AreEqual(customerEntity.IsActive, sameItemCopied.IsActive);

            // Orders Info
            Assert.AreEqual(customerEntity.LastOrderDate, sameItemCopied.LastOrderDate);
            Assert.AreEqual(customerEntity.TotalOrders, sameItemCopied.TotalOrders);
            Assert.AreEqual(customerEntity.AverageOrderValue, sameItemCopied.AverageOrderValue);
            Assert.AreEqual(customerEntity.LoyaltyPoints, sameItemCopied.LoyaltyPoints);

            // Additional Info
            Assert.AreEqual(customerEntity.CustomerSegment, sameItemCopied.CustomerSegment);
            Assert.AreEqual(customerEntity.ReferralSource, sameItemCopied.ReferralSource);

            // Collections
            CollectionAssert.AreEqual(customerEntity.Tags, sameItemCopied.Tags);
            CollectionAssert.AreEqual(customerEntity.CustomFields.Keys.ToList(), sameItemCopied.CustomFields.Keys.ToList());
            CollectionAssert.AreEqual(customerEntity.CustomFields.Values.ToList(), sameItemCopied.CustomFields.Values.ToList());
        }

        [TestMethod("Copia di un oggetto creato al volo in un altro di tipo diverso.")]
        public void Test7()
        {
            var customerEntity = CreateFake.ObjectOfType<CustomerEntity>();
            using var timer = new StopWatchOperationHelper("Test7");
            Mapper.CopyObject<CustomerEntity, CustomerDTO>(customerEntity, out var customerDto);

            // Basic Info
            Assert.AreEqual(customerEntity.FirstName, customerDto.Name);
            Assert.AreEqual(customerEntity.LastName, customerDto.Surname);
            Assert.AreEqual(customerEntity.Email, customerDto.EmailAddress);
            Assert.AreEqual(customerEntity.PhoneNumber, customerDto.Phone);

            // Business Info
            Assert.AreEqual(customerEntity.CompanyName, customerDto.Business);
            Assert.AreEqual(customerEntity.CompanyVatNumber, customerDto.BusinessVat);

            // Financial Info
            Assert.AreEqual(customerEntity.CreditLimit, customerDto.MaxCredit);
            Assert.AreEqual(customerEntity.CurrentCredit, customerDto.AvailableCredit);
            Assert.AreEqual(customerEntity.CustomerType, customerDto.Type);
            Assert.AreEqual(customerEntity.CustomerCategory, customerDto.Category);
            Assert.AreEqual(customerEntity.PaymentTerms, customerDto.PaymentConditions);

            // Preferences
            Assert.AreEqual(customerEntity.ShippingPreference, customerDto.PreferredShipping);
            Assert.AreEqual(customerEntity.PreferredCurrency, customerDto.Currency);
            Assert.AreEqual(customerEntity.Language, customerDto.PreferredLanguage);
            Assert.AreEqual(customerEntity.NewsletterSubscription, customerDto.NewsletterEnabled);
            Assert.AreEqual(customerEntity.MarketingPreference, customerDto.MarketingEnabled);

            // Time Info
            Assert.AreEqual(customerEntity.CreatedAt, customerDto.Created);
            Assert.AreEqual(customerEntity.UpdatedAt, customerDto.Modified);
            Assert.AreEqual(customerEntity.CreatedBy, customerDto.Creator);
            Assert.AreEqual(customerEntity.UpdatedBy, customerDto.Modifier);

            // Status and Metrics
            Assert.AreEqual(customerEntity.IsActive, customerDto.Active);
            Assert.AreEqual(customerEntity.LastOrderDate, customerDto.LastPurchase);
            Assert.AreEqual(customerEntity.TotalOrders, customerDto.TotalPurchases);
            Assert.AreEqual(customerEntity.AverageOrderValue, customerDto.AveragePurchase);
            Assert.AreEqual(customerEntity.LoyaltyPoints, customerDto.Points);

            // Additional Info
            Assert.AreEqual(customerEntity.CustomerSegment, customerDto.Segment);
            Assert.AreEqual(customerEntity.ReferralSource, customerDto.ReferredBy);

            // Collections
            CollectionAssert.AreEqual(customerEntity.Tags, customerDto.Labels);
            CollectionAssert.AreEqual(customerEntity.CustomFields.Keys.ToList(), customerDto.AdditionalFields.Keys.ToList());
            CollectionAssert.AreEqual(customerEntity.CustomFields.Values.ToList(), customerDto.AdditionalFields.Values.ToList());
        }

        [TestMethod("Copia di una lista in modo asincrono.")]
        public async Task Test8()
        {
            var fakeListOf100Items = CreateFake.CollectionOfType<CustomerEntity>(100);
            var listToCopy = new List<CustomerEntity>();
            using var timer = new StopWatchOperationHelper("Test8");
            await Mapper.CopyListAsync<CustomerEntity, CustomerEntity>(fakeListOf100Items, listToCopy, 20);

            foreach (var record in fakeListOf100Items.Index())
            {
                var sameItemCopied = listToCopy[record.Index];
                // Basic Info
                Assert.AreEqual(record.Item.Id, sameItemCopied.Id);
                Assert.AreEqual(record.Item.FirstName, sameItemCopied.FirstName);
                Assert.AreEqual(record.Item.LastName, sameItemCopied.LastName);
                Assert.AreEqual(record.Item.Email, sameItemCopied.Email);
                Assert.AreEqual(record.Item.PhoneNumber, sameItemCopied.PhoneNumber);

                // Address
                Assert.AreEqual(record.Item.Address, sameItemCopied.Address);
                Assert.AreEqual(record.Item.City, sameItemCopied.City);
                Assert.AreEqual(record.Item.Country, sameItemCopied.Country);
                Assert.AreEqual(record.Item.PostalCode, sameItemCopied.PostalCode);
                Assert.AreEqual(record.Item.DateOfBirth, sameItemCopied.DateOfBirth);

                // Tax Info
                Assert.AreEqual(record.Item.TaxCode, sameItemCopied.TaxCode);
                Assert.AreEqual(record.Item.VatNumber, sameItemCopied.VatNumber);

                // Company Info
                Assert.AreEqual(record.Item.CompanyName, sameItemCopied.CompanyName);
                Assert.AreEqual(record.Item.CompanyAddress, sameItemCopied.CompanyAddress);
                Assert.AreEqual(record.Item.CompanyCity, sameItemCopied.CompanyCity);
                Assert.AreEqual(record.Item.CompanyCountry, sameItemCopied.CompanyCountry);
                Assert.AreEqual(record.Item.CompanyPostalCode, sameItemCopied.CompanyPostalCode);
                Assert.AreEqual(record.Item.CompanyEmail, sameItemCopied.CompanyEmail);
                Assert.AreEqual(record.Item.CompanyPhoneNumber, sameItemCopied.CompanyPhoneNumber);
                Assert.AreEqual(record.Item.CompanyVatNumber, sameItemCopied.CompanyVatNumber);

                // Financial Info
                Assert.AreEqual(record.Item.CreditLimit, sameItemCopied.CreditLimit);
                Assert.AreEqual(record.Item.CurrentCredit, sameItemCopied.CurrentCredit);
                Assert.AreEqual(record.Item.CustomerType, sameItemCopied.CustomerType);
                Assert.AreEqual(record.Item.CustomerCategory, sameItemCopied.CustomerCategory);
                Assert.AreEqual(record.Item.PaymentTerms, sameItemCopied.PaymentTerms);

                // Preferences
                Assert.AreEqual(record.Item.ShippingPreference, sameItemCopied.ShippingPreference);
                Assert.AreEqual(record.Item.PreferredCurrency, sameItemCopied.PreferredCurrency);
                Assert.AreEqual(record.Item.Language, sameItemCopied.Language);
                Assert.AreEqual(record.Item.TimeZone, sameItemCopied.TimeZone);
                Assert.AreEqual(record.Item.NewsletterSubscription, sameItemCopied.NewsletterSubscription);
                Assert.AreEqual(record.Item.MarketingPreference, sameItemCopied.MarketingPreference);

                // Notes and Timestamps
                Assert.AreEqual(record.Item.Notes, sameItemCopied.Notes);
                Assert.AreEqual(record.Item.CreatedAt, sameItemCopied.CreatedAt);
                Assert.AreEqual(record.Item.UpdatedAt, sameItemCopied.UpdatedAt);
                Assert.AreEqual(record.Item.CreatedBy, sameItemCopied.CreatedBy);
                Assert.AreEqual(record.Item.UpdatedBy, sameItemCopied.UpdatedBy);
                Assert.AreEqual(record.Item.IsActive, sameItemCopied.IsActive);

                // Orders Info
                Assert.AreEqual(record.Item.LastOrderDate, sameItemCopied.LastOrderDate);
                Assert.AreEqual(record.Item.TotalOrders, sameItemCopied.TotalOrders);
                Assert.AreEqual(record.Item.AverageOrderValue, sameItemCopied.AverageOrderValue);
                Assert.AreEqual(record.Item.LoyaltyPoints, sameItemCopied.LoyaltyPoints);

                // Additional Info
                Assert.AreEqual(record.Item.CustomerSegment, sameItemCopied.CustomerSegment);
                Assert.AreEqual(record.Item.ReferralSource, sameItemCopied.ReferralSource);

                // Collections
                CollectionAssert.AreEqual(record.Item.Tags, sameItemCopied.Tags);
                CollectionAssert.AreEqual(record.Item.CustomFields.Keys.ToList(), sameItemCopied.CustomFields.Keys.ToList());
                CollectionAssert.AreEqual(record.Item.CustomFields.Values.ToList(), sameItemCopied.CustomFields.Values.ToList());
            }
        }
    }
}