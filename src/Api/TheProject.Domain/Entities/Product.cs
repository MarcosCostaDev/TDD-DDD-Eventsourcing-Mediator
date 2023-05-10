using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using TheProject.Shared.Resources;

namespace TheProject.Domain.Entities
{
    public class Product : Notifiable<Notification>
    {
        protected Product() { }
        public Product(string name, string brand, double price)
        {
            var contract = new Contract<Notification>();
            contract.IsNotNullOrEmpty(name, nameof(Name), AppResource.NameMustBeInformed)
                    .IsNotNullOrEmpty(brand, nameof(Brand), AppResource.BrandMustBeInformed)
                    .IsGreaterOrEqualsThan(price, 0, nameof(Price), AppResource.PriceMustBeGreaterOrEqualsZero);

            AddNotifications(contract);
            Id = Guid.NewGuid();
            Name = name;
            Brand = brand;
            Price = price;
        }

        public void Update(string name, string brand, double price)
        {
            var contract = new Contract<Notification>();
            contract.IsNotNullOrEmpty(name, nameof(Name), AppResource.NameMustBeInformed)
                    .IsNotNullOrEmpty(brand, nameof(Brand), AppResource.BrandMustBeInformed)
                    .IsGreaterOrEqualsThan(price, 0, nameof(Price), AppResource.PriceMustBeGreaterOrEqualsZero);

            AddNotifications(contract);
            Name = name;
            Brand = brand;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public IReadOnlyCollection<InvoiceProduct> InvoiceProducts { get; set; }

    }
}
