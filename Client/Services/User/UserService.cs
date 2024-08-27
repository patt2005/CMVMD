using System.Net;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using CMVMD.Client.Providers;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace CMVMD.Client.Services.User;

public class UserService : IUserService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;

    public UserService(ILocalStorageService localStorageService, HttpClient httpClient, AuthenticationStateProvider stateProvider)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
        _authStateProvider = stateProvider;
    }

    public string APIErrorMessages { get; private set; }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/user/login", loginDto);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return false;
        }
        var tokenResponse = await response.Content.ReadFromJsonAsync<JWTTokenResponseDto>();
        await _localStorageService.SetItemAsync("jwt-access-token", tokenResponse?.AccessToken);
        (_authStateProvider as CustomAuthProvider)?.NotifyAuthState();

        return true;
    }

    public async Task<bool> Register(UserRegistrationDto userRegistrationDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/user/register", userRegistrationDto);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return false;
        }
        var tokenResponse = await response.Content.ReadFromJsonAsync<JWTTokenResponseDto>();
        await _localStorageService.SetItemAsync("jwt-access-token", tokenResponse?.AccessToken);
        (_authStateProvider as CustomAuthProvider)?.NotifyAuthState();

        return true;
    }

    public async Task<bool> Logout()
    {
        await _localStorageService.RemoveItemAsync("jwt-access-token");
        (_authStateProvider as CustomAuthProvider)?.NotifyAuthState();
        return true;
    }
}
