using Core.SharedKernel;
using System.Collections.Generic;

namespace Core.Kernel
{
    class BaseEntity
    {
        public int Id { get; set; }

        public List<BaseEvent> Events = new List<BaseEvent>();
    }
}