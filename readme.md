# HotChocolate Subscription performance issue

## The issue

In HotChocolate 13.9(also tested this in 13.8) there is a performance issue when unsubscribing from a subscription topic. This happens when the connection to the websocket is closed. In a web application this can happen when you close the browser or refresh the page.

Unsubscribing from a websocket causes several issues:

- All other GraphQL calls seems blocked until you are unsubscribed from all topics. This also affects other users. The API is unresponsive for the user unsubscribing and all other users.
- It takes long before you are unsubscribed from all topics.
- There is a river of OperationCancelledExceptions happening in the background when unsubscribing.

This repo contains two projects: 

- SubscriptionDos, subscribed 500x to a topic. When the application closes you are unsubscribed and the application becomes unresponsive.
- WebApi, a simple GraphQL API with only one subscription topic.

Remarks:

- This issue also happens when subscribing to different topics! In the PoC one topic is used but it also happens.
- This also happens with other implementations of the subscription library(Redis etc...).
- In this example it takes more than 50 seconds to unsubscribe