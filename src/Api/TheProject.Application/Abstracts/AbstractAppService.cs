

using Flunt.Notifications;
using TheProject.Application.AppServices.Interfaces;
using TheProject.Core.UnitOfWork;

namespace TheProject.Application.Abstracts;

public abstract class AbstractAppService : Notifiable<Notification>, IAppService
{
    protected Task CommitAsync(IUnitOfWork unitOfWork)
    {
        if (IsValid)
        {
            return unitOfWork.CommitAsync();
        }
        return unitOfWork.RoolbackAsync();

    }

    protected void Commit(IUnitOfWork unitOfWork)
    {
        if (IsValid)
        {
            unitOfWork.Commit();
            return;
        }
        unitOfWork.RollBack();
    }
}
