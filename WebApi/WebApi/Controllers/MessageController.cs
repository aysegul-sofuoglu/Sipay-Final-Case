using AutoMapper;
using Base;
using DataAccess.Domain;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Schema;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public MessageController(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<MessageResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
            return new ApiResponse<List<MessageResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<MessageResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<Message, MessageResponse>(entity);
            return new ApiResponse<MessageResponse>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] MessageRequest request)
        {
            var entity = mapper.Map<MessageRequest, Message>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] MessageRequest request)
        {
            var entity = mapper.Map<MessageRequest, Message>(request);
            entity.MessageId = id;

            repository.Update(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {

            repository.DeleteById(id);
            repository.Save();
            return new ApiResponse();
        }
    }
}

