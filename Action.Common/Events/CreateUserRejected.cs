using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Common.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }
        public string Email { get; }

        protected CreateUserRejected()
        {
        }

        public CreateUserRejected(string email, string reason, string code)
        {
            Email = email;
            Code = code;
            Reason = reason;
        }
    }
}
