# Synology API Client (.NET/C#) [![Build status](https://img.shields.io/appveyor/ci/plneto/synology-api-client.svg)](https://ci.appveyor.com/project/plneto/synology-api-client/branch/master) [![Test status](https://img.shields.io/appveyor/tests/plneto/synology-api-client.svg)](https://ci.appveyor.com/project/plneto/synology-api-client/branch/master) [![NuGet Version](http://img.shields.io/nuget/v/Synology.Api.Client.svg?style=flat)](https://www.nuget.org/packages/Synology.Api.Client/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Synology.Api.Client.svg)](https://www.nuget.org/packages/Synology.Api.Client/)

A client written in .NET/C# for the Synology API - https://www.synology.com/en-us/support/developer#tool

#### :warning: WORK IN PROGRESS

I've only implemented the endpoints that I'm currently using but please feel free to request changes or submit contributions.

## Usage:

```c#
const string dsmUrl = "http://192.168.0.1:5001/";

var client = new SynologyClient(dsmUrl);

// Retrieve all API description objects
var apiInfo = await client.InfoApi().QueryAsync();

// Authenticate
await client.LoginAsync("username", "password");

// List all shares
var shares = await client.FileStationApi().ListEndpoint().ListSharesAsync();

// Upload file
var uploadResult = await client.FileStationApi().UploadEndpoint().UploadAsync("path_to_file", "destination");

// End session
await client.LogoutAsync(session);
```

## APIs Implemented

| SYNO.API |     Methods      |
| -------- | :--------------: |
| Auth     | `login` `logout` |
| Info     |     `query`      |

| SYNO.FileStation |            Methods             |
| ---------------- | :----------------------------: |
| List             |       `list` `list_share`      |
| Search           |         `start` `list`         |
| Upload           |            `upload`            |
| CopyMove         |    `start` `status` `stop`     |
| CreateFolder     |            `create`            |
| Extract          | `start` `status` `stop` `list` |

| SYNO.DownloadStation |      Methods       |
|----------------------|:------------------:|
| Info                 | `info` `getconfig` |
| Task                 |       `list`       |

## Example on how to ignore certificate validation for e.g. self signed certificates:

You need to inject your own `HttpClient` with a modified `HttpClientHandler`.

```c#
const string dsmUrl = "http://192.168.0.1:5001/";

var handler = new HttpClientHandler();
//This disables all certificate validation. It is not recommended (especially in production)!
handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

var httpClient = new HttpClient(handler);

var client = new SynologyClient(dsmUrl, httpClient);
```
