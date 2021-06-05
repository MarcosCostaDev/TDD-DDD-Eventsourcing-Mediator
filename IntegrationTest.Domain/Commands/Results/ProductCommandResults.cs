using IntegrationTest.Core.Command;
using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Commands.Results
{
    public class ProductCommandResults
    {
        public class CreateProductResult : ICommandResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
            public IEnumerable<InvoiceProduct> InvoiceProducts { get; set; }

        }

        public class UpdateProductResult : ICommandResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public double Price { get; set; }
            public IEnumerable<InvoiceProduct> InvoiceProducts { get; set; }

        }
    }
}
