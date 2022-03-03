using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost webHost;

        public ServiceHost(IWebHost webHost)
        {
            this.webHost = webHost;
        }

        public void Run() => webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }        
    }

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost webHost;

        private IBusClient _bus;

        public HostBuilder(IWebHost webHost)
        {
            this.webHost = webHost;
        }

        public BusBuilder UseRabbitMq()
        {
            _bus = (IBusClient)webHost.Services.GetService(typeof(IBusClient));
            return new BusBuilder(webHost, _bus);
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(webHost);
        }
    }

    public class BusBuilder : BuilderBase
    {

        private readonly IWebHost webHost;

        private IBusClient _bus;
        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            this.webHost = webHost;
            _bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var serviceScopeFactory = webHost.Services.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var handler = (ICommandHandler<TCommand>)scope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));
                _bus.WithCommandHandlerAsync(handler);
            }
                //var handler = (ICommandHandler<TCommand>)webHost.Services
                //.GetService(typeof(ICommandHandler<TCommand>));
            //_bus.WithCommandHandlerAsync(handler);

            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var serviceScopeFactory = webHost.Services.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var handler = (IEventHandler<TEvent>)scope.ServiceProvider.GetService(typeof(IEventHandler<TEvent>));
                _bus.WithEventHandlerAsync(handler);
            }
            //var handler = (IEventHandler<TEvent>)webHost.Services
            //    .GetService(typeof(IEventHandler<TEvent>));
            //_bus.WithEventHandlerAsync(handler);

            return this;
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(webHost);
        }
    }
}
