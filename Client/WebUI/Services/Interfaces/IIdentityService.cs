using IdentityModel.Client;
using Shared.Dtos;
using WebUI.Models;

namespace WebUI.Services.Interfaces;

public interface IIdentityService
{
    Task<Response<bool>> SignIn(SignInInput signInInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RemoveRefreshToken();
}