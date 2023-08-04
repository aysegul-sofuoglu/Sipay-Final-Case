using AutoMapper;
using Base;
using Business;
using DataAccess.Domain;
using DataAccess.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schema;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class DuesController : ControllerBase
    {
        private readonly IDuesService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DuesController(IUnitOfWork unitOfWork, IMapper mapper, IDuesService service)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.service = service;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ApiResponse<List<DuesResponse>> GetAll()
        {
            var response = service.GetAll();
            return response;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ApiResponse<DuesResponse> Get(int id)
        {
            var response = service.GetById(id);
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ApiResponse Post([FromBody] DuesRequest request)
        {
            var response = service.Insert(request);
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] DuesRequest request)
        {
            var entity = mapper.Map<DuesRequest, Dues>(request);
            entity.DuesId = id;

            unitOfWork.DuesRepository.Update(entity);
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
