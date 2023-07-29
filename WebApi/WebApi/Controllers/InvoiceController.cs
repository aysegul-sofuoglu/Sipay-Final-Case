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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository repository;
        private readonly IMapper mapper;

        public InvoiceController(IInvoiceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<InvoiceResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<Invoice>, List<InvoiceResponse>>(entityList);
            return new ApiResponse<List<InvoiceResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<InvoiceResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<Invoice, InvoiceResponse>(entity);
            return new ApiResponse<InvoiceResponse>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] InvoiceRequest request)
        {
            var entity = mapper.Map<InvoiceRequest, Invoice>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] InvoiceRequest request)
        {
            var entity = mapper.Map<InvoiceRequest, Invoice>(request);
            entity.InvoiceId = id;

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
