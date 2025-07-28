using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Abstractions.Dtos.AccountDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Service.Abstractions.IServices;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class AccountServices :IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountServices(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            _userManager=userManager;
            _configuration=configuration;
        }
        public async Task<AuthModel> Login(LoginDto loginDto)
        {
            var authModel = new AuthModel();
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User==null||!await _userManager.CheckPasswordAsync(User, loginDto.Password))
            {
                authModel.Message="Email or Password is incorrect";
                return authModel;
            }
            var JwtSecurityToken = CreateJwtToken(User);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(await JwtSecurityToken);
            authModel.Message=$"{User.UserName} login successfully\"";
            authModel.IsAuthenticated=true;
            authModel.Token = tokenString;
            
            if (User.refreshTokens.Any(t=>t.IsActive))
            {
                var activeRefreshToken = User.refreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken=activeRefreshToken.Token;
                authModel.RefreshTokenExpiration=activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken=refreshToken.Token;
                authModel.RefreshTokenExpiration=refreshToken.ExpiresOn;
                User.refreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(User);
            }
                return authModel;
        }

        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.refreshTokens.Any(t => t.Token==token));
            if(user is null)
            {
                authModel.Message="Invalid token";
                return authModel;
            }
            
            var refreshToken = user.refreshTokens.Single(t => t.Token==token);
            if(!refreshToken.IsActive)
            {
                authModel.Message="Inactive token";
                return authModel;
            }
            
            refreshToken.RevokedOn=DateTime.UtcNow;
            
            var newRefreshToken = GenerateRefreshToken();
            user.refreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
            
            var jwtToken = await CreateJwtToken(user);
            authModel.IsAuthenticated =true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email =user.Email;
            authModel.UserName = user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles=roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
            return authModel;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.refreshTokens.Any(t => t.Token == token));
            if (user == null)
                return false;

            var refreshToken = user.refreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var UserClaims = await _userManager.GetClaimsAsync(user);
            var Roles = await _userManager.GetRolesAsync(user);
            var RoleClaims = new List<Claim>();
            foreach (var Rolename in Roles)
            {
                RoleClaims.Add(new Claim(ClaimTypes.Role, Rolename));
            }
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)

            }.Union(UserClaims)
            .Union(RoleClaims);

            var SecretKeyString = _configuration.GetSection("SecratKey").Value;
            var SecreteKeyBytes = Encoding.ASCII.GetBytes(SecretKeyString);
            SecurityKey securityKey = new SymmetricSecurityKey(SecreteKeyBytes);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Expiredate = DateTime.Now.AddDays(2);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: Claims,
                signingCredentials: signingCredentials,
                expires: Expiredate
                );
            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn =DateTime.UtcNow
            };
        }
    }
}
