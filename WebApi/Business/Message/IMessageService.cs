using Base;
using Business.Generic;
using DataAccess.Domain;
using Schema;

namespace Business
{
    public interface IMessageService : IGenericService<Message, MessageRequest, MessageResponse>
    {
        ApiResponse UpdateMessage(int messageId, MessageRequest request);
    }
}
