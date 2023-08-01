using AutoMapper;
using Base;
using Business;
using DataAccess.Domain;
using DataAccess.Repository;
using DataAccess.Uow;
using Microsoft.AspNetCore.Mvc;
using Schema;

namespace WebApi.Controllers
{

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


        [HttpGet]
        public ApiResponse<List<MessageResponse>> GetAll()
        {
            var response = service.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<MessageResponse> Get(int id)
        {
            var response = service.GetById(id);
            return response;
        }

        [HttpPost]
        public ApiResponse Post([FromBody] MessageRequest request)
        {
            var response = service.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] MessageRequest request)
        {
            var entity = mapper.Map<MessageRequest, Message>(request);
            entity.MessageId = id;

            unitOfWork.MessageRepository.Update(entity);
            unitOfWork.Complete();
            return new ApiResponse();
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {

            var response = service.Delete(id);
            return response;
        }
    }
}

