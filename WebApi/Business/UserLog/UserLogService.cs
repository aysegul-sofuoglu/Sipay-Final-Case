using AutoMapper;
using Base;
using Business.Generic;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;

namespace Business
{
    public class UserLogService : GenericService<UserLog, UserLogRequest, UserLogResponse>, IUserLogService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserLogService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public ApiResponse<List<UserLogResponse>> GetByUserSession(string email)
        {
           var list = unitOfWork.UserLogRepository.Where(x => x.Email == email).ToList();
            var mapped = mapper.Map<List<UserLogResponse>>(list);
            return new ApiResponse<List<UserLogResponse>>(mapped);
        }
    }
}
