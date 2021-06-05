using Flunt.Notifications;
using IntegrationTest.Core.Repositories;
using IntegrationTest.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Core.Command
{
    public abstract class CommandHandler : Notifiable<Notification>
    {
        protected async Task CommitAsync(IUnitOfWork unitOfWork) 
        {
            if(IsValid)
            {
                await unitOfWork.CommitAsync();
            }
            else
            {
                await unitOfWork.RoolbackAsync();
            }
        }

        protected void Commit(IUnitOfWork unitOfWork)
        {
            if (IsValid)
            {
                unitOfWork.Commit();
            }
            else
            {
                unitOfWork.RollBack();
            }
        }
    }
}
