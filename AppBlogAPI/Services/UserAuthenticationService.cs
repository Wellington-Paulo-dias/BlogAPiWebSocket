using AppBlogAPI.Data;
using AppBlogAPI.DTOs;
using AppBlogAPI.Extensions;
using AppBlogAPI.Helpers;
using AppBlogAPI.Models;
using AppBlogAPI.Repositories.Interfaces;
using AppBlogAPI.Services.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppBlogAPI.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            var userModel = new User(model.UserName, model.Password);

            var user = await _userManager.FindByNameAsync(userModel.Username!);

            if (user is not null && await _userManager.CheckPasswordAsync(user, userModel.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    // claim com valor nome do usuario
                    new Claim("id",user.Id!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

                await _userManager.UpdateAsync(user);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return string.Empty;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var userModel = new User(model.Username, model.Password, model.Email);

            var userExists = await _userManager.FindByNameAsync(userModel.Username!);

            if (userExists != null)
            {
                IdentityResult.Failed(new IdentityError
                {
                    Description = "Já existe um usuário com este estes dados no sistema!",
                    Code = "UserExists"
                }); ;
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            return await _userManager.CreateAsync(user, userModel.Password!);
        }

        public async Task<ApplicationUser> GetIdUserByEmailAsync(string emailUser)
        {
            var result = await _userManager.FindByEmailAsync(emailUser);

            return result;
        }
    }
}