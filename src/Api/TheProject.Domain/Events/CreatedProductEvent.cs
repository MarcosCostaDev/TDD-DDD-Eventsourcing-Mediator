using System;
using TheProject.Core.Command;

namespace TheProject.Domain.Events;

public class CreatedProductEvent : CommandEvent
{
    public Guid ProductId { get; set; }
}
