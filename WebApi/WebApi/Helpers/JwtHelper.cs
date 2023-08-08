using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Helpers
{
    public class JwtHelper
    {
        public static int GetUserIdFromJwt(HttpContext httpContext)
        {
            try
            {
                string jwtToken = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
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
    }
}
