using Base;
using Schema;

namespace Business.Token
{
    public interface ITokenService
    {
        ApiResponse<TokenResponse> Login(TokenRequest request);
    }
}
