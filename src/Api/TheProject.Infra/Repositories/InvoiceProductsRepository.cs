using System.Collections.Generic;
using System.Threading.Tasks;
using TheProject.Core.Repositories;
using TheProject.Domain.Entities;
using TheProject.Domain.Repositories;
using TheProject.Infra.Contexts;

namespace TheProject.Infra.Repositories
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
