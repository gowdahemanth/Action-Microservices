﻿using Action.Services.Activities.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task AddAsync(Activity activity);
    }
}
