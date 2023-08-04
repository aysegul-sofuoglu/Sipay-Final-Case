using Base;
using Business.Generic;
using DataAccess.Domain;
using Schema;

namespace Business
{
    public interface IUserLogService : IGenericService<UserLog, UserLogRequest, UserLogResponse>
    {
        ApiResponse<List<UserLogResponse>> GetByUserSession(string email);
    }
}
