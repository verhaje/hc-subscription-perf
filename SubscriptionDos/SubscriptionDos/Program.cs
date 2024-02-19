using System.Reactive.Disposables;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Http.Websocket;
using GraphQL.Client.Serializer.Newtonsoft;

const int SubscriptionCount = 500;

var client = new GraphQLHttpClient("https://localhost:8080/graphql/", new NewtonsoftJsonSerializer());
client.Options.WebSocketProtocol = WebSocketProtocols.GRAPHQL_WS;
var subscriptions = new CompositeDisposable();

for (var i = 0; i < SubscriptionCount; i++)
{
    subscriptions.Add(CreateSubscription(Guid.NewGuid().ToString(), client));
}

static IDisposable CreateSubscription(string id, GraphQLHttpClient client)
{
    var stream = client.CreateSubscriptionStream<string>(new GraphQLRequest(@"
					subscription {
						test{
						    id
						}
					}"
        )
    { Variables = new { id } });

    return stream.Subscribe(
        response => Console.WriteLine($"{id}: new message"),
        exception => Console.WriteLine($"{id}: message subscription stream failed: {exception}"),
        () => Console.WriteLine($"{id}: message subscription stream completed"));
}

Console.WriteLine("Press any key to disconnect");
Console.ReadLine();
