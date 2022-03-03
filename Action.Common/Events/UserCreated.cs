using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Common.Events
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string Name { get; }

        //creating a protected method because the serializer doesn't have any issues in serializing the message
        protected UserCreated()
        {
        }

        public UserCreated(string em, string nm)
        {
            Email = em;
            Name = nm;
        }

    }
}
