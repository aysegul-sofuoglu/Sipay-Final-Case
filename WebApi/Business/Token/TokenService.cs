using Base;
using Base.Jwt;
using Base.LogType;
using DataAccess.Domain;
using DataAccess.Uow;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Token
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserLogService userLogService;
        private readonly JwtConfig jwtConfig;

        public TokenService(IUnitOfWork unitOfWork, IUserLogService userLogService, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.unitOfWork = unitOfWork;
            this.userLogService = userLogService;
            this.jwtConfig = jwtConfig.CurrentValue;
        }

        public ApiResponse<TokenResponse> Login(TokenRequest request)
        {
            if(request is null)
            {
                return new ApiResponse<TokenResponse>("Request was null!");
            }
            if(string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return new ApiResponse<TokenResponse>("Request was null!");
            }

            request.Email = request.Email.Trim();
            request.Password = request.Password.Trim();

            var user = unitOfWork.UserRepository.Where(x => x.Email.Equals(request.Email)).FirstOrDefault();
            if(user is null)
            {
                Log(request.Email, LogType.InValidEmail);
                return new ApiResponse<TokenResponse>("Invalid user informations");
            }

            if (user.Password.ToLower() != CreateMD5(request.Password))
            {

                unitOfWork.UserRepository.Update(user);
                unitOfWork.Complete();

                Log(request.Email, LogType.WrongPassword);
                return new ApiResponse<TokenResponse>("Invalid user informations");
            }


                unitOfWork.UserRepository.Update(user);
                unitOfWork.Complete();


                string token = Token(user);

                Log(request.Email, LogType.LogedIn);

                TokenResponse response = new()
                {
                    AccessToken = token,
                    ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                    Email = user.Email
                };

                return new ApiResponse<TokenResponse>(response);
            }



         


        private string Token(User user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }


        private Claim[] GetClaims(User user)
        {
            var claims = new[]
            {
            new Claim("Email",user.Email),
            new Claim("UserId",user.UserId.ToString()),
            new Claim("Role",user.Role),
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.Name,$"{user.Name} {user.Surname}")
        };

            return claims;
        }



        private void Log(string email, string logType)
        {
            UserLogRequest request = new()
            {
                LogType = logType,
                Email = email,
                Date = DateTime.UtcNow
            };
            userLogService.Insert(request);

        }

        private string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();

            }
        }
    }
}
