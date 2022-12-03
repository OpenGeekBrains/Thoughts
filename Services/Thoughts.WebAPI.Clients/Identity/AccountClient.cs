using System.Net.Http.Json;
using System.Net;

using Microsoft.Extensions.Logging;

using Thoughts.DAL.Entities.Idetity;

using static WebStore.Interfaces.Services.WebAPIAddresses.Addresses.Identity;
using System.IdentityModel.Tokens.Jwt;
using static System.Net.WebRequestMethods;

namespace Thoughts.WebAPI.Clients.Identity;

public class AccountClient //: BaseClient //, UsersClient, IRolesClient
{
    private readonly ILogger<AccountClient> _Logger;
    private string _refreshToken;
    public HttpClient Http { get; }

    public AccountClient(HttpClient Http, ILogger<AccountClient> Logger)
    {
        this.Http = Http;
        _Logger = Logger;
    }

    public async Task<List<IdentUser>?> GetAllUsersAsync(CancellationToken Cancel = default)
    {
        await CheckToken(Cancel).ConfigureAwait(false);

        var response = await Http.GetAsync($"{Accounts}/GetAllUsers", Cancel).ConfigureAwait(false);

        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
            case HttpStatusCode.NotFound:
            case HttpStatusCode.Unauthorized:
                return default;
            default:
                var result = await response
                   .EnsureSuccessStatusCode()
                   .Content
                   .ReadFromJsonAsync<List<IdentUser>>(cancellationToken: Cancel);
                return result;
        }
    }

    public async Task<List<IdentRole>?> GetAllRolessAsync(CancellationToken Cancel = default)
    {
        await CheckToken(Cancel).ConfigureAwait(false);

        var response = await Http.GetAsync($"{Accounts}/GetAllRoles", Cancel).ConfigureAwait(false);

        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
            case HttpStatusCode.NotFound:
            case HttpStatusCode.Unauthorized:
                return default;
            default:
                var result = await response
                   .EnsureSuccessStatusCode()
                   .Content
                   .ReadFromJsonAsync<List<IdentRole>>(cancellationToken: Cancel);
                return result;
        }
    }

    private async Task CheckToken(CancellationToken Cancel)
    {
        var token = Http.DefaultRequestHeaders.Authorization.Parameter;

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var decodeJWT = jwtSecurityTokenHandler.ReadJwtToken(token);

        if (decodeJWT.ValidTo < DateTime.UtcNow)
        {
            var decodeRefreshJWT = jwtSecurityTokenHandler.ReadJwtToken(_refreshToken);
            if (decodeRefreshJWT.ValidTo > DateTime.UtcNow)
            {
                var responseRefreshToken = await Http.GetAsync($"{Accounts}/GetRefreshToken", Cancel).ConfigureAwait(false);

                if (responseRefreshToken.StatusCode == HttpStatusCode.OK)
                {
                    var bearer = responseRefreshToken.Headers.GetValues("Authorization").FirstOrDefault();
                    Http.DefaultRequestHeaders.Authorization = new("Bearer", bearer);
                    _refreshToken = responseRefreshToken.Headers.GetValues("RefreshToken").FirstOrDefault();
                }
                else
                    throw new InvalidOperationException("Не удалось обновить токен!");
            }
        }
    }

    public async Task LoginAsync(string login, string password, CancellationToken Cancel = default)
    {
        var response = await Http.PostAsJsonAsync($"{Accounts}/Login", new { login, password }).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var bearer = response.Headers.GetValues("Authorization").FirstOrDefault();
            Http.DefaultRequestHeaders.Authorization = new("Bearer", bearer);
            _refreshToken = response.Headers.GetValues("RefreshToken").FirstOrDefault();
        }
        else
            throw new InvalidOperationException("Не удалось получить токен от сервера! Авторизация не выполнена.");
    }

    public async Task LogoutAsync(CancellationToken Cancel = default)
    {
        var response = await Http.PostAsJsonAsync($"{Accounts}/Logout", Cancel).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Http.DefaultRequestHeaders.Authorization = null;
        }
    }
}
