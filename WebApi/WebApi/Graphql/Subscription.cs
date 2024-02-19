using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace WebApi.Graphql
{
    public class Subscription
    {
        TaskCanceledException exc;
        public ValueTask<ISourceStream<TestReturnClass>> TestReturnClass([Service] ITopicEventReceiver receiver, CancellationToken token)
        {
            var topic = $"{nameof(TestReturnClass)}";
            return receiver.SubscribeAsync<TestReturnClass>(topic, token);
        }

        [Subscribe(With = nameof(TestReturnClass))]
        public TestReturnClass Test([EventMessage] TestReturnClass message) => message;
    }

    public class TestReturnClass
    {
        public Guid Id { get; set; }
    }
}
