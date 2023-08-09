using Microsoft.AspNetCore.Http;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace Business.Helpers
{
    public class JwtHelper
    {

        public static int GetUserIdFromJwt(string jwtToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(jwtToken);

                var userIdClaim = decodedToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                return Convert.ToInt32(userIdClaim);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while decoding JWT");
                throw;
            }
        }

        public static int GetUserIdFromJwt(HttpContext httpContext)
        {
            string jwtToken = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return GetUserIdFromJwt(jwtToken);
        }
    }
}
