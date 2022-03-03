﻿using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Action.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Activity> GetAsync(Guid id) =>
            await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Activity activity) =>
            await Collection.InsertOneAsync(activity);

        private IMongoCollection<Activity> Collection =>
            _database.GetCollection<Activity>("Activities");
    }
}
