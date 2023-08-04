using Base;
using Business.Token;
using Microsoft.AspNetCore.Mvc;
using Schema;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class TokenController : ControllerBase
    {

        private readonly ITokenService service;
        public TokenController(ITokenService service)
        {
            this.service = service;
        }

        [HttpPost("Login")]
        public ApiResponse<TokenResponse> Post([FromBody] TokenRequest request)
        {
            var response = service.Login(request);
            return response;
        }
    }
}
