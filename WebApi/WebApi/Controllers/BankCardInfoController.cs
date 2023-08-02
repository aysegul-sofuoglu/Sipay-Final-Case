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
    public class BankCardInfoController : ControllerBase
    {
        private readonly IBankCardInfoService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BankCardInfoController(IUnitOfWork unitOfWork, IMapper mapper, IBankCardInfoService service)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.service = service;
        }


        [HttpGet]
        public ApiResponse<List<BankCardInfoResponse>> GetAll()
        {
            var response = service.GetAll("Payments", "User");
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<BankCardInfoResponse> Get(int id)
        {
            var response = service.GetById(id, "Payments", "User");
            return response;
        }

        [HttpPost]
        public ApiResponse Post([FromBody] BankCardInfoRequest request)
        {
            var response = service.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] BankCardInfoRequest request)
        {
            var entity = mapper.Map<BankCardInfoRequest, BankCardInfo>(request);
            entity.CardId = id;

            unitOfWork.BankCardInfoRepository.Update(entity);
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
