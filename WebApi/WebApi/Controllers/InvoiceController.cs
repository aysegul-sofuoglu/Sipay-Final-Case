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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper, IInvoiceService service)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.service = service;
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ApiResponse<List<InvoiceResponse>> GetAll()
        {
            var response = service.GetAll("Genre", "Payments");
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ApiResponse<InvoiceResponse> Get(int id)
        {
            var response = service.GetById(id, "Genre", "Payments");
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ApiResponse Post([FromBody] InvoiceRequest request)
        {
            var response = service.Insert(request);
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] InvoiceRequest request)
        {
            var entity = mapper.Map<InvoiceRequest, Invoice>(request);
            entity.InvoiceId = id;

            unitOfWork.InvoiceRepository.Update(entity);
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
        public ApiResponse<List<InvoiceResponse>> GetUnpaidInvoice()
        {
            var response = service.GetAll("Payments", "Genre");

            var unpaidInvoiceList = new List<InvoiceResponse>();

            foreach (var invoiceResponse in response.Response)
            {
                var payments = invoiceResponse.Payments;

                if (payments == null || !payments.Any() && payments.Sum(p => p.Amount) != invoiceResponse.Amount)
                {


                    invoiceResponse.Payments = payments?.Select(payment => new PaymentResponse
                    {
                        PaymentId = payment.PaymentId,
                        CardId = payment.CardId,
                        UserName = payment.UserName,
                        DuesId = payment.DuesId,
                        InvoiceId = payment.InvoiceId,
                        Date = payment.Date,
                        Amount = payment.Amount,
                    }).ToList();

                    unpaidInvoiceList.Add(invoiceResponse);
                }

            }

           
            return new ApiResponse<List<InvoiceResponse>>(unpaidInvoiceList);
        }
    }
}
