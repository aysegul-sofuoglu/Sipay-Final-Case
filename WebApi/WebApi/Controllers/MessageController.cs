﻿using AutoMapper;
using Base;
using Business;
using Business.Helpers;
using DataAccess.Domain;
using DataAccess.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schema;

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
            if(response.Success)
            {
                foreach (var message in response.Response)
                {
                    if(message.Seen == false)
                    {
                        message.Seen = true;
                        service.UpdateMessage(message.MessageId, new MessageRequest());
                    }
                    
                }
            }

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

            int userId = JwtHelper.GetUserIdFromJwt(HttpContext);



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

    }
}

