using EventBus.Bus;
using EventBus.Events;
using GuessWord.Abstractions.Events;

namespace GuessWord.Dictionary.IntegrationEvents.EventHandlers
{
    public class GetWordEventHandler : IEventHandler<GetWordEvent>
    {
        private readonly ILogger<GetWordEvent> _logger;
        private readonly IEventBus eventBus;

        public GetWordEventHandler(ILogger<GetWordEvent> logger, IEventBus eventBus)
        {
            _logger = logger;
            this.eventBus = eventBus;
        }

        public Task HandleAsync(GetWordEvent @event)
        {
            // Here you handle what happens when you receive an event of this type from the event bus.
            _logger.LogInformation(@event.ToString());
            eventBus.Publish(new SendWordEvent
            {
                Words = new []{ "111", "222" }
            });
            return Task.CompletedTask;
        }
    }
}
