﻿using Base;
using Schema;

namespace Business
{
    public interface IPaymentService 
    {
        ApiResponse<List<PaymentResponse>> GetAll(params string[] includes);
        ApiResponse<PaymentResponse> GetById(int id);

        ApiResponse<PayResponse> Pay(int userId, PayRequest request);
        ApiResponse Delete(int Id);
    }
}
