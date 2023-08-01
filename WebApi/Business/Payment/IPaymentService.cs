using Base;
using Business.Generic;
using DataAccess.Domain;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IPaymentService 
    {
        ApiResponse<List<PaymentResponse>> GetAll();
        ApiResponse<PaymentResponse> GetById(int id);

        ApiResponse<PayResponse> Pay(PayRequest request);
    }
}
