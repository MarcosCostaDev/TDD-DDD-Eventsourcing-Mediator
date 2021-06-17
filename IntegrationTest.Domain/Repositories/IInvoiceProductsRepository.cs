using IntegrationTest.Core.Repositories;
using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Repositories
{
    public interface IInvoiceProductsRepository : IRepository
    {
        Task AddAsync(InvoiceProduct productInvoice);
        Task AddAsync(IEnumerable<InvoiceProduct> invoiceProducts);
    }
}
