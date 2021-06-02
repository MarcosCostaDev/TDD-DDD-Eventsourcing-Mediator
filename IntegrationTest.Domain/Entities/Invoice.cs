using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Entities
{
    public class Invoice : Notifiable<Notification>
    {
        public Invoice()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public IReadOnlyCollection<InvoiceProduct> InvoiceProducts { get; set; }
    }
}
