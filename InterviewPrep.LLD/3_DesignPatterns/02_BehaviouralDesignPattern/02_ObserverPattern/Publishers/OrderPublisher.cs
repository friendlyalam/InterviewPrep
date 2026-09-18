using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._02_ObserverPattern.Publishers
{
    public sealed class OrderPublisher : IOrderPublisher
    {
        private readonly List<IEventSubscriber> _subscribers = new();

        public void Subscribe(IEventSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IEventSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void Publish(OrderPlacedEvent orderEvent)
        {
            Console.WriteLine("===== Publishing Order Event =====");

            foreach (IEventSubscriber subscriber in _subscribers)
            {
                subscriber.Handle(orderEvent);
            }

            Console.WriteLine("===== Event Published Successfully =====");
        }
    }
}
