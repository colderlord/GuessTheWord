using EventBus.Events;

namespace GuessWord.Abstractions.Events
{
    public class SendWordEvent : Event
    {
        public string[] Words { get; set; }
    }
}