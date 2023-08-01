using Business.Generic;
using Schema;
using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace Business
{
    public interface IBankCardInfoService : IGenericService<BankCardInfo, BankCardInfoRequest, BankCardInfoResponse>
    {
        ApiResponse Balance(int cardId, decimal amount, PaymentDirection direction);
    }
}
