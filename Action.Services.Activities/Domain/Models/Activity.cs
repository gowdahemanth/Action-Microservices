﻿using Action.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Activity()
        {

        }
        public Activity(Guid id, Category category, Guid userId, string name, string desc, DateTime createAt)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ActionException("empty_activity_name", $"Activity name cannot be empty.");
            }

            Id = id;
            Category = category.Name;
            UserId = userId;
            Name = name;
            Description = desc;
            CreatedAt = createAt;
        }
    }
}
