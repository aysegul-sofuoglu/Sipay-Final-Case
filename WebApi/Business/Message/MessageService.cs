using AutoMapper;
using Base;
using Business.Generic;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;

namespace Business
{
    public class MessageService : GenericService<Message, MessageRequest, MessageResponse>, IMessageService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public MessageService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        public ApiResponse UpdateMessage(int messageId, MessageRequest request)
        {
      
            var message = unitOfWork.MessageRepository.GetById(messageId);

            message.Seen = true;
            unitOfWork.MessageRepository.Update(message);
            unitOfWork.Complete();

            return new ApiResponse();
        }
    }
}

