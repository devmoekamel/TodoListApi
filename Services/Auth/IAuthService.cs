using TodolistApi.DTO;
using TodolistApi.Model;

namespace TodolistApi.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDTO model);
        Task<AuthModel> LoginAsync(LoginDTO model);
        Task<string> AddRoleAsync(AddRoleDTO model);
    }
}
