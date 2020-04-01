using System;

namespace Core.SharedKernel
{
    class BaseEvent
    {
        public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;
    }
}
