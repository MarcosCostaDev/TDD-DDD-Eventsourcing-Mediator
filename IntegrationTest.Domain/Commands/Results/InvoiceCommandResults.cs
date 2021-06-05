using IntegrationTest.Core.Command;
using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Commands.Results
{
    public class InvoiceCommandResults
    {
        public class InvoiceCommandResult 
        {
            public Guid Id { get; set; }
            public DateTime CreatedDate { get; set; }
            public Guid CustomerId { get;  set; }
            public double Discount { get;  set; }
            public double Total { get;  set; }
            public double TotalWithDiscount { get;  set; }
            public IEnumerable<InvoiceProduct> InvoiceProducts { get; set; }
        }
    }
}
