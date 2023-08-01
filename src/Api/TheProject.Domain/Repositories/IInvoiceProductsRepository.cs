using System.Collections.Generic;
using System.Threading.Tasks;
using TheProject.Core.Repositories;
using TheProject.Domain.Entities;

namespace TheProject.Domain.Repositories
{
    public interface IInvoiceProductsRepository : IRepository
    {
        Task AddAsync(InvoiceProduct productInvoice);
        Task AddAsync(IEnumerable<InvoiceProduct> invoiceProducts);
    }
}
