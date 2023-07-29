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
    public class BankCardInfoController : ControllerBase
    {
        private readonly IBankCardInfoRepository repository;
        private readonly IMapper mapper;

        public BankCardInfoController(IBankCardInfoRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<BankCardInfoResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<BankCardInfo>, List<BankCardInfoResponse>>(entityList);
            return new ApiResponse<List<BankCardInfoResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<BankCardInfoResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<BankCardInfo, BankCardInfoResponse>(entity);
            return new ApiResponse<BankCardInfoResponse>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] BankCardInfoRequest request)
        {
            var entity = mapper.Map<BankCardInfoRequest, BankCardInfo>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] BankCardInfoRequest request)
        {
            var entity = mapper.Map<BankCardInfoRequest, BankCardInfo>(request);
            entity.CardId = id;

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
