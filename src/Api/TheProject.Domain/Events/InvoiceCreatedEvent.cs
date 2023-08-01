using System;
using TheProject.Core.Command;

namespace TheProject.Domain.Events;

public class InvoiceCreatedEvent : CommandEvent
{
    public Guid InvoiceId { get; set; }
    public Guid CustomerId { get; set; }
}
