using Synology.Api.Client;
using Synology.Api.Client.Apis.FileStation.List.Models;

// Update these values with your own
const string dsmUrl = "http://192.168.0.1:5001/";
const string username = "username";
const string password = "password";

//The path to the shared directory
//Example: "/DataVault/Departments/IT"
const string sharedFolderPath = "/DataVault/Departments/IT";

/*
 * This is a simple example of how to use the Synology API Client .NET library.
 * 
 * This example will:
 * 1. Connect to the DSM
 * 2. Query the API info
 * 3. Authenticate
 * 4. List all shares
 * 5. Logout
 */

Console.WriteLine("Starting Synology API Client .NET 8 sample project");

var client = new SynologyClient(dsmUrl);

Console.WriteLine("Querying API info...");
// Retrieve all API description objects
var apiInfo = await client.InfoApi().QueryAsync();

Console.WriteLine("Auth API Path: {0}", apiInfo.AuthApi?.Path);
Console.WriteLine("Auth API Min Version: {0}", apiInfo.AuthApi?.MinVersion);
Console.WriteLine("Auth API Max Version: {0}", apiInfo.AuthApi?.MaxVersion);

// Authenticate
Console.WriteLine("Authenticating user {0}...", username);
await client.LoginAsync(username, password);
Console.WriteLine("Authenticated");

Console.WriteLine("Listing shares...");
// List all shares
var shares = await client.FileStationApi().ListEndpoint().ListSharesAsync();

foreach (var share in shares.Shares)
{
    Console.WriteLine("- Share: {0}", share.Name);
}

//List all files in the shares
var files = await client.FileStationApi().ListEndpoint().ListAsync(new FileStationListRequest(sharedFolderPath));
foreach (var item in files.Files)
{
    Console.WriteLine($"Name: {item.Name}");
    Console.WriteLine($"Path: {item.Path}");
    Console.WriteLine($"Additional:");
    Console.WriteLine($"    RealPath: {item.Additional?.RealPath}");
    Console.WriteLine($"    Owner: {item.Additional?.Owner?.User}");
    Console.WriteLine($"    Size: {item.Additional?.Size}");
}

Console.WriteLine("Logging out...");
await client.LogoutAsync();

Console.WriteLine("Done");