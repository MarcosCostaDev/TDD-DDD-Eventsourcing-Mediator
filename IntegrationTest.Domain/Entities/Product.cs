using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Entities
{
    public class Product : Notifiable<Notification>
    {
        protected Product() { }
        public Product(string name, string brand)
        {
            var contract = new Contract<Notification>();
            contract.IsNotNullOrEmpty(name, "Name", "Name must be informed.")
                .IsNotNullOrEmpty(brand, "Brand", "Brand must be informed.");

            AddNotifications(contract);
            Id = Guid.NewGuid();
            Name = name;
            Brand = brand;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }

        public IReadOnlyCollection<InvoiceProduct> InvoiceProducts { get; set; }

    }
}
