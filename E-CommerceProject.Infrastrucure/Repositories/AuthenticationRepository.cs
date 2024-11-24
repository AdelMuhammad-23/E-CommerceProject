using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_CommerceProject.Core.Interfaces;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;

        public AuthenticationRepository(JwtSettings jwtSettings, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }
        public async Task<JwtAuthResult> GetJwtToken(User user)
        {
            var (jwtToken, accessToken) = await GetJWTToken(user);




            var response = new JwtAuthResult();
            response.AccessToken = accessToken;
            return response;
        }


        #region Claims Functions
        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        #endregion


        #region JWT Token Functions  For Help
        // using table to return more than one of types like string and JwtSecurityToken
        private async Task<(JwtSecurityToken, string)> GetJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
              _jwtSettings.Issuer,
              _jwtSettings.Audience,
                claims,
              expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            //token
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }

        #endregion

    }
}
