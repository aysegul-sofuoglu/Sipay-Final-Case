using AutoMapper;
using Base;
using Business.Generic;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;

namespace Business
{
    public class UserService : GenericService<User, UserRequest, UserResponse>, IUserService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        public override ApiResponse<List<UserResponse>> GetAll(params string[] includes)
        {
            return base.GetAll(includes);
        }

        public override ApiResponse<UserResponse> GetById(int id, params string[] includes)
        {
            return base.GetById(id, includes);
        }


    }
}
