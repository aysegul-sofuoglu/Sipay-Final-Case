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
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MessageController(IUnitOfWork unitOfWork, IMapper mapper, IMessageService service)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.service = service;
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ApiResponse<List<MessageResponse>> GetAll()
        {
            var response = service.GetAll("User");
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ApiResponse<MessageResponse> Get(int id)
        {
            var response = service.GetById(id, "User");
            return response;
        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public ApiResponse Post([FromBody] MessageRequest request)
        {
            var entity = mapper.Map<MessageRequest, Message>(request);

            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userIdString = GetUserIdFromJwt(jwtToken);
            int userId = Convert.ToInt32(userIdString);

      

            if (userId != request.SenderId)
            {
                return new ApiResponse("Enter your own information! " + "Your senderId: " + userId);
            }

            unitOfWork.MessageRepository.Insert(entity);
            unitOfWork.Complete();
            return new ApiResponse();
        }


        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] MessageRequest request)
        {
            var entity = mapper.Map<MessageRequest, Message>(request);
            entity.MessageId = id;

            unitOfWork.MessageRepository.Update(entity);
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

