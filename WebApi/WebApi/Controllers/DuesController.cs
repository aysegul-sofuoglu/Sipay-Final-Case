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
            var response = service.GetAll("Payments");


            return response;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ApiResponse<DuesResponse> Get(int id)
        {
            var response = service.GetById(id, "Payments");

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

        [Authorize(Roles = "admin")]
        [HttpGet("unpaid")]
        public ApiResponse<List<DuesResponse>> GetUnpaidDues()
        {
            var response = service.GetAll("Payments");

            var unpaidDuesList = new List<DuesResponse>();

            foreach (var duesResponse in response.Response)
            {
                var payments = duesResponse.Payments;

                if (payments == null || !payments.Any() && payments.Sum(p => p.Amount) != duesResponse.Amount)
                {
                    

                    duesResponse.Payments = payments?.Select(payment => new PaymentResponse
                    {
                        PaymentId = payment.PaymentId,
                        CardId = payment.CardId,
                        UserName = payment.UserName,
                        DuesId = payment.DuesId,
                        InvoiceId = payment.InvoiceId,
                        Date = payment.Date,
                        Amount = payment.Amount,
                    }).ToList();

                    unpaidDuesList.Add(duesResponse);
                }
                
            }

            return new ApiResponse<List<DuesResponse>>(unpaidDuesList);
        }



        
    }
}
