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
    public class DuesController : ControllerBase
    {
        private readonly IDuesRepository repository;
        private readonly IMapper mapper;

        public DuesController(IDuesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<DuesResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<Dues>, List<DuesResponse>>(entityList);
            return new ApiResponse<List<DuesResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<DuesResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<Dues, DuesResponse>(entity);
            return new ApiResponse<DuesResponse>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] DuesRequest request)
        {
            var entity = mapper.Map<DuesRequest, Dues>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] DuesRequest request)
        {
            var entity = mapper.Map<DuesRequest, Dues>(request);
            entity.DuesId = id;

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
