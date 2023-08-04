using Business.Generic;
using Schema;
using DataAccess.Domain;
using Base;

namespace Business
{
    public interface IBankCardInfoService : IGenericService<BankCardInfo, BankCardInfoRequest, BankCardInfoResponse>
    {
        ApiResponse Balance(int cardId, decimal amount, PaymentDirection direction);
    }
}
