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
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserController(IUserService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<UserResponse>> GetAll()
        {
            var response = service.GetAll("Apartments.Dueses", "Apartments.Invoices", "Messages" , "BankCards");
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<UserResponse> Get(int id)
        {
            var response = service.GetById(id,"Apartments.Dueses", "Apartments.Invoices", "Messages", "BankCards");
            return response;
        }

        [HttpPost]
        public ApiResponse Post([FromBody] UserRequest request)
        {
            var response = service.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] UserRequest request)
        {

            var entity = mapper.Map<UserRequest, User>(request);
            entity.UserId = id;

            unitOfWork.UserRepository.Update(entity);
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
