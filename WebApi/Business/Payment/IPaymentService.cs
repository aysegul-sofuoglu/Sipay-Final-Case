using Base;
using Schema;

namespace Business
{
    public interface IPaymentService 
    {
        ApiResponse<List<PaymentResponse>> GetAll();
        ApiResponse<PaymentResponse> GetById(int id);

        ApiResponse<PayResponse> Pay(PayRequest request);
    }
}
