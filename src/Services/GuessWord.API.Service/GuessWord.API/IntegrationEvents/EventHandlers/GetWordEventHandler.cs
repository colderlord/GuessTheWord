using System.Threading.Tasks;
using EventBus.Events;
using GuessWord.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace GuessWord.API.IntegrationEvents.EventHandlers
{
    public class GetWordEventHandler : IEventHandler<SendWordEvent>
    {
        private readonly ILogger<SendWordEvent> _logger;

        public GetWordEventHandler(ILogger<SendWordEvent> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(SendWordEvent @event)
        {
            // Here you handle what happens when you receive an event of this type from the event bus.
            _logger.LogInformation(@event.ToString());
            return Task.CompletedTask;
        }
    }
}
