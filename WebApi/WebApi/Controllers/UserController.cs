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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<UserResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<User>, List<UserResponse>>(entityList);
            return new ApiResponse<List<UserResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<UserResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<User, UserResponse>(entity);
            return new ApiResponse<UserResponse>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] UserRequest request)
        {
            var entity = mapper.Map<UserRequest, User>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] UserRequest request)
        {
            var entity = mapper.Map<UserRequest, User>(request);
            entity.UserId = id;

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
