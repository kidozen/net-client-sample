/*
 * KidoZen .NET Client Sample -
 * 
 * Use this sample to test how to consume KidoZen APIs from your
 * .NET projects (ie: services, web services, web apis, web sites, etc...
 * 
 * Notice: For Mobile, Xamarin or Windows Phone projects please refer
 * to the KidoZen SDKs instead!
 */

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ClientTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Run().Wait();
		}

		/*
		 * This method will retrieve a KidoZen token using a Service Account
		 * clientID and clientSecret, and then invoke a KidoZen API (ie: query a datasource).
		 * 
		 * In order to generate your clientId and clientSecret follow these steps:
		 * - Go to the application center
		 * - Admin > Select application > Security > Clients
		 * - Click in "Create new client" and select "Service Account" from the options.
		 * - Give your service account Core APIs access (ie: "allow all *")
		 * - Enable all the data access APIs this client will be using
		 * - Save it!
		 * 
		 * Notice: make sure to complete your own "tenant", "app", "clientID" and "clientSecret"
		 * parameters, as well as double check the API you want to consume.
		 */
		public static async Task Run()
		{
			var tenant = "YOUR-TENANT";
			var app = "YOUR-APP";
			var clientID = "PASTE-CLIENT-ID-HERE";
			var clientSecret = "PASTE-CLIENT-SECRET-HERE";
			var url = String.Format("https://{0}-{1}.kidocloud.com", app, tenant);

			//
			// Before we can make an API call, we need a KidoZen token.
			//
			Console.WriteLine ("Retrieving token");
			var authServiceURL = "https://auth.kidozen.com/v1/" + tenant + "/oauth/token";
			var client = new HttpClient ();
			var tokenResponse = await client.PostAsJsonAsync (authServiceURL, new {
				client_id = clientID,
				client_secret = clientSecret,
				scope = app,
				grant_type = "client_credentials",
			});
			if (!tokenResponse.IsSuccessStatusCode) {
				Console.WriteLine ("Error getting auth token");
				Console.WriteLine (tokenResponse.Content.ReadAsStringAsync ().Result);
				return;
			}
			var token = await tokenResponse.Content.ReadAsAsync<Token> ();
			Console.WriteLine ("Retrieved auth token:" + token.access_token);

			//
			// In this case we are querying a data access API (aka DataSource), but
			// you can use the same token to access any of the other APIs that KidoZen
			// exposes.
			//
			var dsName = "NAME-OF-YOUR-DATA-SOURCE";
			Console.WriteLine ("Querying Data Source: " + dsName);
			client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse ("Bearer " + token.access_token);
			var dsUrl = url + "/api/v2/datasources/" + dsName;
			var dsResponse = await client.GetAsync(dsUrl);
			Console.WriteLine ("Success: " + dsResponse.IsSuccessStatusCode);
			Console.WriteLine (dsResponse.Content.ReadAsStringAsync ().Result);
		}

		/*
		 * Token
		 * DTO class used to deserialize the token returned by the KidoZen authentication
		 * service.
		 */
		public class Token
		{
			public string access_token { get; set; }
		}
	}
}
