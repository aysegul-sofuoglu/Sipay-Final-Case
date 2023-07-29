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
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository repository;
        private readonly IMapper mapper;

        public ApartmentController(IApartmentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<ApartmentResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<Apartment>, List<ApartmentResponse>>(entityList);
            return new ApiResponse<List<ApartmentResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<ApartmentResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<Apartment, ApartmentResponse>(entity);
            return new ApiResponse<ApartmentResponse>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] ApartmentRequest request)
        {
            var entity = mapper.Map<ApartmentRequest, Apartment>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] ApartmentRequest request)
        {
            var entity = mapper.Map<ApartmentRequest, Apartment>(request);
            entity.ApartmentId = id;

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
