﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Common.Events
{
    public class UserAuthenticated : IEvent
    {
        public string Email { get; }

        protected UserAuthenticated()
        { }

        public UserAuthenticated(string em)
        {
            Email = em;
        }
    }
}
