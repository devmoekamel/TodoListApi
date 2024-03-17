using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using todlistApi.Model;
using TodolistApi.DTO;
using TodolistApi.Helper;
using TodolistApi.Model;

namespace TodolistApi.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthModel { Message = "Email Or Password is Wrong" };



            var authmodel = new AuthModel();

            var JwtSecurityToken = await CreateJwtToken(user);
            var Roles = await _userManager.GetRolesAsync(user);



            return new AuthModel
            {
                ExpiredOn = JwtSecurityToken.ValidTo,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                UserName = user.UserName,
                Roles = Roles.ToList(),
                IsAuthenticated = true,
                Id = user.Id

            };
        }

        public async Task<AuthModel> RegisterAsync(RegisterDTO model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "This Email already Exist" };
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "This Username Already Exist" };

            var newuser = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            var result = await _userManager.CreateAsync(newuser, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description}\n";
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(newuser, "User");

            var jwtsecuretoken = await CreateJwtToken(newuser);

            return new AuthModel
            {
                Id = newuser.Id,
                IsAuthenticated = true,
                UserName = newuser.UserName,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtsecuretoken),
                ExpiredOn = jwtsecuretoken.ValidTo
            };
        }

        public async Task<string> AddRoleAsync(AddRoleDTO model)
        {

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role)) return "Invalid User or ROle ";
            if (await _userManager.IsInRoleAsync(user, model.Role)) return $"User already In this {model.Role}";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "somthing went Wrong";
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.Duration),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user, string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole)
            };
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddDays(_jwt.Duration),
                signingCredentials: credentials
                );
            return token;
        }
    }
}
