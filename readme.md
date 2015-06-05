#KidoZen .NET Client Sample
Use this sample to test how to consume KidoZen APIs from your
.NET projects (ie: services, web services, web apis, web sites, etc...
 
*Notice*: For Mobile, Xamarin or Windows Phone projects please refer
to the KidoZen SDKs instead!

##Running the Sample
In order to run the sample, you will need to complete the four
values in the following lines of code:
```
			var tenant = "YOUR-TENANT";
			var app = "YOUR-APP";
			var clientID = "PASTE-CLIENT-ID-HERE";
			var clientSecret = "PASTE-CLIENT-SECRET-HERE";


In order to generate your clientId and clientSecret follow these steps:

* Go to the application center
* Admin > Select application > Security > Clients
* Click in "Create new client" and select "Service Account" from the options.
* Give your service account Core APIs access (ie: "allow all *")
* Enable all the data access APIs this client will be using
* Save it!

##Support
If you need help with this sample, please send us an email to support@kidozen.com
