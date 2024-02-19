using HotChocolate.Subscriptions.Diagnostics;

namespace WebApi
{
    public class SubscriptionDiagnostics : SubscriptionDiagnosticEventsListener
    {
        public override void SubscribeSuccess(string topicName)
        {
            Console.WriteLine($"{DateTime.UtcNow} - Subscribed: {topicName}");
            base.SubscribeSuccess(topicName);
        }

        public override void Unsubscribe(string topicName, int shard, int subscribers)
        {
            Console.WriteLine($"{DateTime.UtcNow} - Unsubscribed: {topicName}:{shard}:{subscribers}");
            base.Unsubscribe(topicName, shard, subscribers);
        }

        public override void Close(string topicName)
        {
            Console.WriteLine($"Close: {topicName}");
            base.Close(topicName);
        }
    }
}
