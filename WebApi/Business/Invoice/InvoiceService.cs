using AutoMapper;
using Base;
using Business.Generic;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;

namespace Business
{
    public class InvoiceService : GenericService<Invoice, InvoiceRequest, InvoiceResponse>, IInvoiceService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public InvoiceService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public override ApiResponse<List<InvoiceResponse>> GetAll(params string[] includes)
        {
            return base.GetAll(includes);
        }

        public override ApiResponse<InvoiceResponse> GetById(int id, params string[] includes)
        {
            return base.GetById(id, includes);
        }
    }
}
