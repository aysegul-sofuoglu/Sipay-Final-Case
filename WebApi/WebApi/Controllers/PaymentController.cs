
using Base;
using Business;
using DataAccess.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schema;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService service;
        private readonly IUnitOfWork unitOfWork;

        public PaymentController(IPaymentService service, IUnitOfWork unitOfWork)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ApiResponse<List<PaymentResponse>> GetAll()
        {
            var response = service.GetAll();
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ApiResponse<PaymentResponse> Get(int id)
        {
            var response = service.GetById(id);
            return response;
        }


        [Authorize(Roles = "user")]
        [HttpPost("Transfer")]
        public ApiResponse<PayResponse> Transfer([FromBody] PayRequest request)
        {
            var usercard = unitOfWork.BankCardInfoRepository.Where(card => card.CardId == request.FromCardId).FirstOrDefault();

            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userIdString = GetUserIdFromJwt(jwtToken);
            int userId = Convert.ToInt32(userIdString);

            if (usercard.CardId != request.FromCardId || userId != usercard.UserId)
            {
                return new ApiResponse<PayResponse>("Card not found or it does not belong to the user." + usercard.CardId + " " + request.FromCardId + " " + userId + " " + usercard.UserId);
            }

            if(request.FromCardId == request.ToCardId)
            {
                return new ApiResponse<PayResponse>("You can only pay to another account.");
            }


            var response = service.Pay(request);
            return response;



        }





        private string GetUserIdFromJwt(string jwtToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(jwtToken);

                var userIdClaim = decodedToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                return userIdClaim;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while decoding JWT");
                throw;
            }
        }


    }
}
