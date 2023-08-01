using AutoMapper;
using Base;
using Business.Generic;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class DuesService : GenericService<Dues, DuesRequest, DuesResponse>, IDuesService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public DuesService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public override ApiResponse<List<DuesResponse>> GetAll(params string[] includes)
        {
            return base.GetAll(includes);
        }

        public override ApiResponse<DuesResponse> GetById(int id, params string[] includes)
        {
            return base.GetById(id, includes);
        }
    }
}
