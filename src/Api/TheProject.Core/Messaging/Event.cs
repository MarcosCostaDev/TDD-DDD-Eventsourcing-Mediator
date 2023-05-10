using MediatR;
using System;

namespace TheProject.Core.Messaging
{
    public abstract class Event : INotification
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; set; }
    }
}
