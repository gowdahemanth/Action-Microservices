using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Common.Events
{
    public class ActivityCreated : IAuthenticatedEvent
    {
        public Guid UserId { get; }
        public Guid Id { get; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }

        protected ActivityCreated()
        {
        }

        public ActivityCreated(Guid id, Guid userId, string category, string name)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
        }
    }
}
