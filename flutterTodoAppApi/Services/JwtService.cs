using flutterTodoAppApi.Data.SettingsModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace flutterTodoAppApi.Services
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IEnumerable<Claim>? _claims;

        public JwtService(IHttpContextAccessor httpContextAccessor,
            IOptions<JwtSettings> jwtSettings)
        {
            _claims = httpContextAccessor.HttpContext?.User.Claims;
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(DateTime expiers, params Claim[] claims)
        {
            var symmetricSecurityIssuerSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SignKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = expiers,
                SigningCredentials = new SigningCredentials(symmetricSecurityIssuerSignKey, SecurityAlgorithms.HmacSha512),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var tokenStr = $"Bearer {tokenHandler.WriteToken(token)}";

            return tokenStr;
        }

        public string? GetTokenClaim(string key) => _claims?.FirstOrDefault(claim => claim.Type == key)?.Value;
    }
}
