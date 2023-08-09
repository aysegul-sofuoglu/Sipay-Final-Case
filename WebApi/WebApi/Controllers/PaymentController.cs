
using Base;
using Business;
using Business.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schema;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService service;

        public PaymentController(IPaymentService service)
        {
            this.service = service;
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ApiResponse<List<PaymentResponse>> GetAll()
        {
            var response = service.GetAll("User");
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ApiResponse<PaymentResponse> Get(int id)
        {
            var response = service.GetById(id);
            return response;
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            var response = service.Delete(id);
            return response;
        }


        [Authorize(Roles = "user")]
        [HttpPost("Transfer")]
        public ApiResponse<PayResponse> Transfer([FromBody] PayRequest request)
        {
            
            int userId = JwtHelper.GetUserIdFromJwt(HttpContext);

            var response = service.Pay(userId, request);
            return response;



        }


    }
}
