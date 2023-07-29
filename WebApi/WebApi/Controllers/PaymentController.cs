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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository repository;
        private readonly IMapper mapper;

        public PaymentController(IPaymentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<PaymentResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<Payment>, List<PaymentResponse>>(entityList);
            return new ApiResponse<List<PaymentResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<PaymentResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<Payment, PaymentResponse>(entity);
            return new ApiResponse<PaymentResponse>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] PaymentRequest request)
        {
            var entity = mapper.Map<PaymentRequest, Payment>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] PaymentRequest request)
        {
            var entity = mapper.Map<PaymentRequest, Payment>(request);
            entity.PaymentId = id;

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
