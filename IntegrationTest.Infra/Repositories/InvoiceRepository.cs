using IntegrationTest.Core.Repositories;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Domain.Repositories;
using IntegrationTest.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Infra.Repositories
{
    public class InvoiceRepository : Repository<MyDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(MyDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Invoice invoice)
        {
            await Context.Invoices.AddAsync(invoice);
        }

        public async Task<Invoice> GetAsync(Guid id)
        {
            return await Context.Invoices.Include(p => p.InvoiceProducts).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }

        public async Task<IList<Invoice>> ListAsync()
        {
            return await Context.Invoices.AsNoTracking().ToListAsync();
        }
    }
}
