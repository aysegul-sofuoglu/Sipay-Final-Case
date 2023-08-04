using AutoMapper;
using Base;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;

namespace Business.Generic
{
    public class ApartmentService : GenericService<Apartment, ApartmentRequest, ApartmentResponse>, IApartmentService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ApartmentService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public override ApiResponse<List<ApartmentResponse>> GetAll(params string[] includes)
        {
            return base.GetAll(includes);
        }

        public override ApiResponse<ApartmentResponse> GetById(int id, params string[] includes)
        {
            return base.GetById(id, includes);
        }
    }
}


