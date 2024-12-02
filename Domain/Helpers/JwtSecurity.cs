using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Helpers
{
    public static class JwtSecurity
    {
        public static string GenerateJwtToken(int userId, string secretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", userId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "user_mng",
                audience: "user_mng",
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool ValidateJwt(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                return true; // Token is valid
            }
            catch
            {
                return false; // Token is invalid
            }
        }

        public static string? GetClaimFromDecodedJwt(string claim, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                Console.WriteLine("Invalid JWT token format.");
                return null;
            }

            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Extract the needed claim from the payload
            var neededClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == claim);
            return neededClaim?.Value; // Return the value of the claim
        }
    }
}
