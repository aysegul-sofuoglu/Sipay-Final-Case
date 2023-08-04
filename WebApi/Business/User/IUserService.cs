using Business.Generic;
using DataAccess.Domain;
using Schema;

namespace Business
{
    public interface IUserService : IGenericService<User, UserRequest,UserResponse>
    {
    }
}
