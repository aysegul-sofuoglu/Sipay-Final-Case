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


        [HttpGet]
        public ApiResponse<List<InvoiceResponse>> GetAll()
        {
            var response = service.GetAll("Genre", "Payments");
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<InvoiceResponse> Get(int id)
        {
            var response = service.GetById(id, "Genre", "Payments");
            return response;
        }

        [HttpPost]
        public ApiResponse Post([FromBody] InvoiceRequest request)
        {
            var response = service.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] InvoiceRequest request)
        {
            var entity = mapper.Map<InvoiceRequest, Invoice>(request);
            entity.InvoiceId = id;

            unitOfWork.InvoiceRepository.Update(entity);
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
