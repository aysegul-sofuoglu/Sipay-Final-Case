using AutoMapper;
using Base;
using Business;
using DataAccess.Domain;
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
    public class BankCardInfoController : ControllerBase
    {
        private readonly IBankCardInfoService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BankCardInfoController(IUnitOfWork unitOfWork, IMapper mapper, IBankCardInfoService service)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.service = service;
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ApiResponse<List<BankCardInfoResponse>> GetAll()
        {
            var response = service.GetAll("Payments", "User");
            return response;
        }


        [Authorize(Roles = "user")]
        [HttpGet("{id}")]
        public ApiResponse<BankCardInfoResponse> Get(int id)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userIdString = GetUserIdFromJwt(jwtToken);
            int userId = Convert.ToInt32(userIdString);
            var response = service.GetById(id, "Payments", "User");

            if (response.Response == null)
            {
                return new ApiResponse<BankCardInfoResponse>("Card data not found.");
            }

            if (response.Response.UserId == null)
            {
                return new ApiResponse<BankCardInfoResponse>("Card data not found for the user.");
            }


            if (response.Response.UserId == userId)
            {
                return response;
            }
            else
            {
                return new ApiResponse<BankCardInfoResponse>("This card does not have to you.");
            }

        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public ApiResponse Post([FromBody] BankCardInfoRequest request)
        {
            var entity = mapper.Map<BankCardInfoRequest, BankCardInfo>(request);

            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userIdString = GetUserIdFromJwt(jwtToken);
            int userId = Convert.ToInt32(userIdString);

            

            if (userId != request.UserId)
            {
                return new ApiResponse("Enter your own information! " + "Your userId: " + userId);
            }

            unitOfWork.BankCardInfoRepository.Insert(entity);
            unitOfWork.Complete();
            return new ApiResponse();
        }


        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] BankCardInfoRequest request)
        {
            var entity = mapper.Map<BankCardInfoRequest, BankCardInfo>(request);
            entity.CardId = id;

            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userIdString = GetUserIdFromJwt(jwtToken);
            int userId = Convert.ToInt32(userIdString);

            if(userId != request.UserId)
            {
                return new ApiResponse("This card does not have to you!" );
            }


            unitOfWork.BankCardInfoRepository.Update(entity);
            unitOfWork.Complete();
            return new ApiResponse();



        }

        


        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {


            var response = service.Delete(id);

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
