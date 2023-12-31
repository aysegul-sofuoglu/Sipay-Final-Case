﻿using AutoMapper;
using Base;
using Business;
using Business.Helpers;
using DataAccess.Domain;
using DataAccess.Uow;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ApiResponse<List<ApartmentResponse>> GetAll()
        {
            var response = service.GetAll("Dueses.Payments", "Invoices.Payments", "User", "ApartmentType", "Block");
            return response;
        }


        [Authorize(Roles = "user")]
        [HttpGet("{id}")]
        public ApiResponse<ApartmentResponse> Get(int id)
        {

            int userId = JwtHelper.GetUserIdFromJwt(HttpContext);



            var response = service.GetById(id, "Dueses.Payments", "Invoices.Payments", "User", "ApartmentType", "Block");

            if (response.Response == null)
            {
                return new ApiResponse<ApartmentResponse>("Apartment data not found.");
            }

            if (response.Response.UserId == null)
            {
                return new ApiResponse<ApartmentResponse>("User data not found for the apartment.");
            }

  
            if (response.Response.UserId == userId)
            {
           
                return response;
            }
            else
            {
     
                return new ApiResponse<ApartmentResponse>("This apartment does not have to you.");
            }
        }

       


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ApiResponse Post([FromBody] ApartmentRequest request)
        { 
        
            var response = service.Insert(request);
            return response;
        }


        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] ApartmentRequest request)
        {
            var entity = mapper.Map<ApartmentRequest, Apartment>(request);
            entity.ApartmentId = id;

            unitOfWork.ApartmentRepository.Update(entity);
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


    }
}
