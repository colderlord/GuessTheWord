using System;
using EventBus.Bus;
using EventBus.RabbitMQ.Bus;
using EventBus.RabbitMQ.Connection;
using EventBus.Subscriptions;
using EventBus.Subscriptions.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EventBus.RabbitMQ.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds an event bus that uses RabbitMQ to deliver messages.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="connectionUrl">URL to connect to RabbitMQ.</param>
        /// <param name="brokerName">Broker name. This represents the exchange name.</param>
        /// <param name="queueName">Messa queue name, to track on RabbitMQ.</param>
        /// <param name="timeoutBeforeReconnecting">The amount of time in seconds the application will wait after trying to reconnect to RabbitMQ.</param>
        public static void AddRabbitMQEventBus(this IServiceCollection services, string connectionUrl, string brokerName, string queueName, int timeoutBeforeReconnecting = 15)
        {
            services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();
            services.AddSingleton<IPersistentConnection, RabbitMQPersistentConnection>(factory =>
            {
                var connectionFactory = new ConnectionFactory
                {
                    Uri = new Uri(connectionUrl),
                    DispatchConsumersAsync = true,
                };

                var logger = factory.GetService<ILogger<RabbitMQPersistentConnection>>();
                return new RabbitMQPersistentConnection(connectionFactory, logger, timeoutBeforeReconnecting);
            });

            services.AddSingleton<IEventBus, RabbitMQEventBus>(factory =>
            {
                var persistentConnection = factory.GetService<IPersistentConnection>();
                var subscriptionManager = factory.GetService<IEventBusSubscriptionManager>();
                var logger = factory.GetService<ILogger<RabbitMQEventBus>>();

                return new RabbitMQEventBus(persistentConnection, subscriptionManager, factory, logger, brokerName, queueName);
            });
        }
    }
}
