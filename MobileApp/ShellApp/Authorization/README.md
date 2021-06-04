# Authorization

## AuthenticationStateProvider
Just like Blazor, there is an ```AuthenticationStateProvider``` that keeps track of the authentication state.

The class is abstract and you need to find a suitable implementation or write your own.

### Interface

Invoke the ```GetAuthenticationStateAsync``` method to retrieve the current authentication state.

```c#
public abstract Task<AuthenticationState> GetAuthenticationStateAsync();
```

Subscribe to the ```AuthenticationStateChanged```event to get a notification when the authentication state has changed:

```c#
public event AuthenticationStateChangedHandler AuthenticationStateChanged;
```

Implementeing classes call the method ```NotifyAuthenticationStateChanged``` to tell subscribers that the authentication state has changed.

```c#
protected void NotifyAuthenticationStateChanged(Task<AuthenticationState> task);
``` 

### JwtAuthenticationStateProvider

This is a Xamarin.Forms specific implementation that handles Jwt tokens.

It uses the ```SecureStorage``` class from Xamarin.Essentials as the storage provider for tokens.

You use these methods for setting and clearing the set Authentication Token:

```c#
public async Task SetAuthTokenAsync(string authToken)
```

```c#
 public async Task ClearAuthTokenAsync()
```

## AuthorizationView

The ```AuthorizationView``` lets you display certain content in your Xamarin.Forms forms depending on your authentication state.

To use the view, add the namespace in your XAML page, or view:

```xml
xmlns:auth="clr-namespace:ShellApp.Authorization"
```

Add the ```AuthorizationView``` to your page:

```xaml
<auth:AuthorizationView>
    <auth:AuthorizationView.AuthorizedTemplate>
        <DataTemplate>
             <Label HorizontalTextAlignment="Center" TextColor="Green">Authorized</Label>
        </DataTemplate>
    </auth:AuthorizationView.AuthorizedTemplate>

    <auth:AuthorizationView.UnauthorizedTemplate>
        <DataTemplate>
            <Label HorizontalTextAlignment="Center" TextColor="Red">Unauthorized</Label>
        </DataTemplate>
    </auth:AuthorizationView.UnauthorizedTemplate>
</auth:AuthorizationView>
```

You can also load the Data Templates from Static Resources, like so:

```xaml
<auth:AuthorizationView
    AuthorizedTemplate="{StaticResource AuthorizedTemplate}"
    UnauthorizedTemplate="{StaticResource UnauthorizedTemplate}" />
```

### Role-based authorization

You can also limit the view to a certain Role, using the ```Role``` property.

```xaml
<auth:AuthorizationView Role="Admin">
    <!-- Omitted -->
</auth:AuthorizationView>
```

Multiple roles can be specified by separating them with comma: ```Role="Developer, Tester"```