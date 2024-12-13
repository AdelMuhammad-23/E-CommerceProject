﻿using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Infrastructure.Helper;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IAuthenticationRepository : IGenericRepository<UserRefreshToken>
    {
        public Task<JwtAuthResult> GetJwtToken(User user);
        public Task<string> SendResetPasswordCodeAsync(string email);
        public Task<string> ConfirmResetPasswordAsync(string email, string code);
        public Task<string> ResetPasswordAsync(string email, string Password);
        public Task<JwtAuthResult> GetNewRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
    }
}
