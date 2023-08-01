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
    public class BankCardInfoService : GenericService<BankCardInfo, BankCardInfoRequest, BankCardInfoResponse>, IBankCardInfoService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public BankCardInfoService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public override ApiResponse<List<BankCardInfoResponse>> GetAll(params string[] includes)
        {
            return base.GetAll(includes);
        }

        public override ApiResponse<BankCardInfoResponse> GetById(int id, params string[] includes)
        {
            return base.GetById(id, includes);
        }

        ApiResponse IBankCardInfoService.Balance(int cardId, decimal amount, PaymentDirection direction)
        {
            if(cardId == 0)
            {
                return new ApiResponse("Invalid card!");
            }

            var card = unitOfWork.BankCardInfoRepository.GetById(cardId);
            if(card == null)
            {
                return new ApiResponse("Invalid card!");
            }

            decimal balance = card.Balance;
            if(direction == PaymentDirection.Deposit)
            {
                balance += amount;
            }
            if(direction == PaymentDirection.Withdraw)
            {
                if(card.Balance < amount)
                {
                    return new ApiResponse("Insufficent balance");
                }
                balance -= amount;
            }

            card.Balance = balance;
            unitOfWork.BankCardInfoRepository.Update(card);
            unitOfWork.Complete();

            return new ApiResponse();
        }
    }
}
