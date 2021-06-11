using IntegrationTest.Core.Command;
using IntegrationTest.Core.Repositories;
using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Domain.Repositories
{
    public interface IInvoiceRepository : IRepository
    {
        Task<Invoice> GetAsync(Guid id);
        Task<IList<Invoice>> ListAsync();
        Task AddAsync(Invoice invoice);
    }
}
