using Business.Generic;
using DataAccess.Domain;
using Schema;

namespace Business
{
    public interface IMessageService : IGenericService<Message, MessageRequest, MessageResponse>
    {
    }
}
