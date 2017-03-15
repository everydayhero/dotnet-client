# Everyday Hero

## About this project
The Everyday Hero Registration API is a .Net wrapper that communicates with the Everyday Hero RESTful API.

## Quick Start
```
string clientId = "...";
string clientSecret = "...";
try
{
    var client = new Client("https://edheroz.com", clientId, clientSecret);
    List<Charity> charities = client.Charity.GetCharities(new[] {"au-123"});
}
catch (AuthenticationFailedException ex)
{
    string error = ex.Result.Error;
    string errorDescription = ex.Result.ErrorDescription;
}
catch (RequestFailedException ex)
{
    //Check server error: ex.Result.Error ...
}
```

## Current Capabilities
- Authentication using access token or Client Id and Client Secret
- Pages Client
	- Get Supporter Pages
	- Create Supporter Page
	- Update Supporter Page
- Charities Client
    - Get Charities
    - Get Charity

## Minimum requirements
-.Net 4.0+

## About authentication
You will need to create an account and register an application by following instructions on https://gist.github.com/bradparker/b5074aae4b9a13e5dc7032f703ab4410#step-52-creating-a-supporter-page
This wrapper supports the OAuth2.  You can authenticate with one of the following methods:
1. Client Id and Client Secret
2. Access Token
Authentication is completed with the server on the first request.  Afterwards, the access token is used on each subsequent request and added into the request headers.

If the authentication fails, you will receive an `AuthenticationFailedException` exception.

**Example using a client id and secret:**
```
string clientId = "...";
string clientSecret = "...";
var client = new Client("https://edheroz.com", clientId, clientSecret);
```
**Example using an access token:**
```
string accessToken = "...";
var client = new Client("https://edheroz.com", accessToken);
```

**To create a page**
```
var newPageInfo = new PageCreationFields<string>
{
    birthday = "1980-01-01",
    campaign_id = "au-122",
    charity_id = "au-20",
    user_email = "myEmail@MyAddress.com.au",
    user_name = "username",
    user_phone = "55556666",
    user_address = "Test address"
};
var pageCreatedResult = client.Pages.CreateSupporterPage(newPageInfo);
var activationUrl = pageCreatedResult.activation_url;
```
**To create a page - with a detailed user address**
```
var newPageInfo = new PageCreationFields<UserAddress>
{
    birthday = "1980-01-01",
    campaign_id = "au-122",
    charity_id = "au-20",
    user_email = "myEmail@MyAddress.com.au",
    user_name = "username",
    user_phone = "55556666",
    user_address = new UserAddress
    {
        user_street_address = "Test street address",
        user_extended_address = "Test extended address",
        user_locality = "Test locality",
        user_region = "Test region",
        user_postal_code = "Test postal code",
        user_country_name = "Test country name"
    }
};
var pageCreatedResult = client.Pages.CreateSupporterPage(newPageInfo);
var activationUrl = pageCreatedResult.activation_url;
```

**To update a page**
```
var pageInfo = new PageCreationFields<string>
{
    target = "500",
    name = "Fred",
    expires_at = DateTime.Now.AddDays(14).ToString("O"),
    slug = "TheSlug" ,
    birthday = "1980-01-01",
    campaign_id = "...",
    charity_id = "...",
    user_email = "my@email.address",
    user_name = "My User",
    user_phone = "55556666",
    user_address = "Test address"
};
var result = client.Pages.UpdateSupporterPage(1, pageInfo);
```
**To update a page - with a detailed user address**
```
var pageInfo = new PageCreationFields<UserAddress>
{
    target = "500",
    name = "Fred",
    expires_at = DateTime.Now.AddDays(14).ToString("O"),
    slug = "TheSlug" ,
    birthday = "1980-01-01",
    campaign_id = "...",
    charity_id = "...",
    user_email = "my@email.address",
    user_name = "My User",
    user_phone = "55556666",
    user_address = new UserAddress
    {
        user_street_address = "Test street address",
        user_extended_address = "Test extended address",
        user_locality = "Test locality",
        user_region = "Test region",
        user_postal_code = "Test postal code",
        user_country_name = "Test country name"
    }
};
var result = client.Pages.UpdateSupporterPage(1, pageInfo);
```

**To get pages using pagination**
```
var campaign_id = "...";
var result = client.Pages.GetSupporterPages(1, 10, campaign_id);
```

**To get pages using known ids**
```
var pages = client.Pages.GetSupporterPages(new[] {1234,5678});
```

## Error handling
Authentication errors will throw AuthenticationFailedException
Request failures will throw RequestFailedException
These exception objects hold additional information from the server.

# Naming conventions and structure
The solution uses a primary Client class (everydayhero.Api.v3.Client).  
This client creates an EverydayHero IAuthenticator for use with the RESTSharp library and initialises other clients responsible for interacting with the services.
These are:
- Client.Pages
- Client.Charities

Class names, protected members and constants are using Pascal Case
Parameters use Caml Case

## Naming convention exceptions
The property names for the entities (everydayhero.Api.v3.Entities) are using the same naming conventions as the server to simplify
maintenance.

#NuGet Packages
To make it easer for you to develop with the everydayhero API this client library has been released as a nuget package. The libraries published by Everyday Hero are owned by [everydayhero](https://www.nuget.org/profiles/everydayhero).
