# Everyday Hero

## About this project
The Everyday Hero Registration API is a .Net wrapper that communicates with the Everyday Hero RESTful API.

## Quick Start
```
Dim clientId As String = "..."
Dim clientSecret As String = "..."
Try
	Dim client = New Client("https://edheroz.com", clientId, clientSecret)
	Dim charities As List(Of Charity) = client.Charity.GetCharities(New String() {"au-123"})
Catch ex As AuthenticationFailedException
	Dim [error] As String = ex.Result.Error
	Dim errorDescription As String = ex.Result.ErrorDescription
Catch ex As RequestFailedException
	'Check server error: ex.Result.Error ...
End Try
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
Dim clientId As String = "..."
Dim clientSecret As String = "..."
Dim client = New Client("https://edheroz.com", clientId, clientSecret)
```
**Example using an access token:**
```
Dim accessToken As String = "..."
Dim client = New Client("https://edheroz.com", accessToken)
```

**To create a page**
```
Dim newPageInfo = New PageCreationFields With {
	.birthday = "1980-01-01",
	.campaign_id = "au-122",
	.charity_id = "au-20",
	.user_email = "myEmail@MyAddress.com.au",
	.user_name = "username"
}
Dim pageCreatedResult = client.Pages.CreateSupporterPage(newPageInfo)
Dim activationUrl = pageCreatedResult.activation_url
```

**To update a page**
```
Dim pageInfo = New PageCreationFields With {
	.target = "500",
	.name = "Fred",
	.expires_at = DateTime.Now.AddDays(14).ToString("O"),
	.slug = "TheSlug",
	.birthday = "1980-01-01",
	.campaign_id = "...",
	.charity_id = "...",
	.user_email = "my@email.address",
	.user_name = "My User"
}
Dim result = client.Pages.UpdateSupporterPage(1, pageInfo)

```

**To get pages using pagination**
```
Dim campaign_id = "..."
Dim result = client.Pages.GetSupporterPages(1, 10, campaign_id)
```

**To get pages using known ids**
```
Dim pages = client.Pages.GetSupporterPages(New Integer(){1234, 5678})
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
