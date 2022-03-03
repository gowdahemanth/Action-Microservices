using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.Exceptions;
using Action.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private ILogger _logger;

        public CreateActivityHandler(IBusClient busClient, ILogger<CreateActivityHandler> logger,
            IActivityService activityService)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating activity: {command.Category} {command.Name}");

            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category,
                    command.Name, command.Description, command.CreatedAt);
                await _busClient.PublishAsync(new ActivityCreated(command.Id, command.UserId, command.Category, command.Name));
                return;
            }
            catch(ActionException ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Code, ex.Message));
                _logger.LogError(ex.Message);
            }
            catch(Exception ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, "error", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}
