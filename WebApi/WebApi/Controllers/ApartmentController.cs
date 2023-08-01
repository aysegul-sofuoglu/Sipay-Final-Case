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
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ApartmentController(IApartmentService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet]
        public ApiResponse<List<ApartmentResponse>> GetAll()
        {
            var response = service.GetAll("Dueses", "Invoices");
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<ApartmentResponse> Get(int id)
        {
            var response = service.GetById(id, "Dueses", "Invoices");
            return response;
        }

        [HttpPost]
        public ApiResponse Post([FromBody] ApartmentRequest request)
        { 
        
            var response = service.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] ApartmentRequest request)
        {
            var entity = mapper.Map<ApartmentRequest, Apartment>(request);
            entity.ApartmentId = id;

            unitOfWork.ApartmentRepository.Update(entity);
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
