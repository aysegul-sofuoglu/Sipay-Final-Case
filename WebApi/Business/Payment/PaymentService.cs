using AutoMapper;
using Base;
using Business.Generic;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IBankCardInfoService bankCardInfoService;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IBankCardInfoService bankCardInfoService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.bankCardInfoService = bankCardInfoService;
        }

        public ApiResponse<List<PaymentResponse>> GetAll()
        {
            try
            {
                var entityList = unitOfWork.PaymentRepository.GetAllWithInclude("BankCardInfo.User");
                var mapped = mapper.Map<List<Payment>, List<PaymentResponse>>(entityList);
                return new ApiResponse<List<PaymentResponse>>(mapped);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "PaymentService.GetAll");
                return new ApiResponse<List<PaymentResponse>>(ex.Message);
            }
        }

        public ApiResponse<PaymentResponse> GetById(int id)
        {
            try
            {
                var entity = unitOfWork.PaymentRepository.GetByIdWithInclude(id, "BankCardInfo.User");
                var mapped = mapper.Map<Payment, PaymentResponse>(entity);
                return new ApiResponse<PaymentResponse>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PaymentService.GetById");
                return new ApiResponse<PaymentResponse>(ex.Message);
            }
        }

        public ApiResponse<PayResponse> Pay(PayRequest request)
        {
            if (request == null)
            {
                return new ApiResponse<PayResponse>("Invalid request!");
            }

            if (request.FromCardId == request.ToCardId)
            {
                return new ApiResponse<PayResponse>("Invalid accounts!");
            }

            if (request.Amount <= 0)
            {
                return new ApiResponse<PayResponse>("Invalid amount!");
            }

            ApiResponse<BankCardInfoResponse> bankCardInfoResponseFrom = bankCardInfoService.GetById(request.FromCardId);
            if(!bankCardInfoResponseFrom.Success || bankCardInfoResponseFrom.Response is null)
            {
                return new ApiResponse<PayResponse>(bankCardInfoResponseFrom.Message);

            }

            BankCardInfoResponse bankCardFrom = bankCardInfoResponseFrom.Response;

            ApiResponse balanceResponseFrom = bankCardInfoService.Balance(bankCardFrom.CardId, request.Amount, PaymentDirection.Withdraw);
            if(!balanceResponseFrom.Success)
            {
                return new ApiResponse<PayResponse>(balanceResponseFrom.Message);
            }





            ApiResponse<BankCardInfoResponse> bankCardInfoResponseTo = bankCardInfoService.GetById(request.ToCardId);
            if (!bankCardInfoResponseTo.Success || bankCardInfoResponseTo.Response is null)
            {
                return new ApiResponse<PayResponse>(bankCardInfoResponseTo.Message);

            }

            BankCardInfoResponse bankCardTo = bankCardInfoResponseTo.Response;

            ApiResponse balanceResponseTo = bankCardInfoService.Balance(bankCardTo.CardId, request.Amount, PaymentDirection.Deposit);
            if (!balanceResponseTo.Success)
            {
                return new ApiResponse<PayResponse>(balanceResponseTo.Message);
            }



            Payment paymentTo = new Payment();
            paymentTo.CardId = bankCardTo.CardId;
            paymentTo.DuesId = request.DuesId;
            paymentTo.InvoiceId = request.InvoiceId;
            paymentTo.Amount = request.Amount;
            paymentTo.Date = request.Date;
            unitOfWork.PaymentRepository.Insert(paymentTo);

            Payment paymentFrom = new Payment();
            paymentFrom.CardId = bankCardFrom.CardId;
            paymentFrom.DuesId = request.DuesId;
            paymentFrom.InvoiceId = request.InvoiceId;
            paymentFrom.Amount = request.Amount;
            paymentFrom.Date = request.Date;
            unitOfWork.PaymentRepository.Insert(paymentFrom);
            unitOfWork.Complete();



            var response = new PayResponse()
            {
                CardId = bankCardTo.CardId
            };

            return new ApiResponse<PayResponse>(response);

        }
    }
}
