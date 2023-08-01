using AutoMapper;
using Base;
using Business;
using DataAccess;
using DataAccess.Domain;
using DataAccess.Repository;
using DataAccess.Uow;
using Microsoft.AspNetCore.Mvc;
using Schema;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService service;

        public PaymentController(IPaymentService service)
        {
            this.service = service;
          
        }

        [HttpGet]
        public ApiResponse<List<PaymentResponse>> GetAll()
        {
            var response = service.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<PaymentResponse> Get(int id)
        {
            var response = service.GetById(id);
            return response;
        }

        [HttpPost("Transfer")]
        public ApiResponse<PayResponse> Transfer([FromBody] PayRequest request)
        {
            var response = service.Pay(request);
            return response;
        }
    

    }
}
