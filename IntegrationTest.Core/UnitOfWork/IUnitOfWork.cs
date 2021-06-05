using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();

        void RollBack();

        Task RoolbackAsync();
    }
}
