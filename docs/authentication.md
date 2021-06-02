# Authentication

The app authenticates to access the Web API. The resulting Auth Token is then used when making requests to that Web API. 

The HTTP Authorization header should look like this:

```Authorization: Bearer <AuthToken>```

The Auth Token is also used when connecting through SignalR.

## Mobile app

In the mobile app, if the server upon receiving a HTTP request returns a response with the status code ```401 Unauthorized```, then the Delegate Request handler will catch that, and send the user to the login screen.