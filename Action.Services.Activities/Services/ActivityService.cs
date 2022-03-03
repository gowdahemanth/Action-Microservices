using Action.Common.Exceptions;
using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ActivityService(IActivityRepository activityRepo, ICategoryRepository categoryRepo)
        {
            _activityRepo = activityRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategory = await _categoryRepo.GetAsync(name);
            if(activityCategory == null)
            {
                throw new ActionException("category_not_found", $"Category: '{category} was not found.");
            }

            var activity = new Activity(id, activityCategory, userId, name, description, createdAt);
            await _activityRepo.AddAsync(activity);
        }
    }
}
