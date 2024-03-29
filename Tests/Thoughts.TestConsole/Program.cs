﻿
using Microsoft.Extensions.Logging;
using Thoughts.WebAPI.Clients.Identity;
using static WebStore.Interfaces.Services.WebAPIAddresses.Addresses.Identity;

var http = new HttpClient
{
    BaseAddress = new("https://localhost:5011")
};

var logger_factory = LoggerFactory.Create(builder => builder.AddConsole());


var account_client = new AccountClient(http, logger_factory.CreateLogger<AccountClient>());

Console.WriteLine("Ожидание сервера");
Console.ReadLine();

try
{
    await account_client.LoginAsync("Admin", "AdPAss_123");
}
catch (InvalidOperationException)
{

    Console.WriteLine("Не авторизован");
}

var roles = await account_client.GetAllRolessAsync();
var users = await account_client.GetAllUsersAsync();

Console.WriteLine("End.");

