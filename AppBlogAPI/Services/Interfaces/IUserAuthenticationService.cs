using AppBlogAPI.Data;
using AppBlogAPI.DTOs;
using AppBlogAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AppBlogAPI.Services.Interfaces
{
    public interface IUserAuthenticationService
    {       
        Task<string> LoginAsync(LoginModel model);
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<ApplicationUser> GetIdUserByEmailAsync(string emailUser);
    }
}
