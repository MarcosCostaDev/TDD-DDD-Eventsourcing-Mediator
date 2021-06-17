using IntegrationTest.Core.Repositories;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repositories;
using IntegrationTest.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Infra.Repositories
{
    public class InvoiceProductsRepository : Repository<MyDbContext>, IInvoiceProductsRepository
    {
        public InvoiceProductsRepository(MyDbContext context) : base(context)
        {
        }

        public async Task AddAsync(InvoiceProduct productInvoice)
        {
            await Context.InvoiceProducts.AddAsync(productInvoice);
        }

        public async Task AddAsync(IEnumerable<InvoiceProduct> invoiceProducts)
        {
            await Context.InvoiceProducts.AddRangeAsync(invoiceProducts);
        }
    }
}
