using System.Threading.Tasks;

namespace TheProject.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();

        void RollBack();

        Task RoolbackAsync();
    }
}
