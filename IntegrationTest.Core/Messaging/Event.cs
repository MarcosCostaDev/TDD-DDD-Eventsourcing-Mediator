using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Core.Messaging
{
    public abstract class Event : INotification
    {
        protected Event() {
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; set; }
    }
}
