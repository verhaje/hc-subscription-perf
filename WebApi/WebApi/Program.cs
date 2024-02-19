
using WebApi;
using WebApi.Graphql;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddInMemorySubscriptions()
    .AddDiagnosticEventListener<SubscriptionDiagnostics>()
    .AddSubscriptionType<Subscription>()
    .InitializeOnStartup();

var app = builder.Build();
app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();
app.Run();
