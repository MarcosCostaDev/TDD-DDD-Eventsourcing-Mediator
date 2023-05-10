using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheProject.Core.Repositories;
using TheProject.Domain.Entities;

namespace TheProject.Domain.Repositories
{
    public interface IInvoiceRepository : IRepository
    {
        Task<Invoice> GetAsync(Guid id);
        Task<IList<Invoice>> ListAsync();
        Task AddAsync(Invoice invoice);
    }
}
