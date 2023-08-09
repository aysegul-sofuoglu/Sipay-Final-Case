using AutoMapper;
using Base;
using DataAccess.Domain;
using DataAccess.Uow;
using Schema;
using Serilog;

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

        public ApiResponse<List<PaymentResponse>> GetAll(params string[] includes)
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


        public ApiResponse Delete(int Id)
        {
            try
            {
                var entity = unitOfWork.PaymentRepository.GetById(Id);
                if (entity == null)
                {
                    return new ApiResponse("Record not found!");
                }

                unitOfWork.PaymentRepository.DeleteById(Id);
                unitOfWork.Complete();
                return new ApiResponse();
            }

            catch (Exception ex)
            {
                Log.Error(ex, "PaymentService.Delete");
                return new ApiResponse(ex.Message);
            }
        }



        public ApiResponse<PayResponse> Pay(int userId, PayRequest request)
        {
            var usercard = unitOfWork.BankCardInfoRepository.Where(card => card.CardId == request.FromCardId).FirstOrDefault();

            if (usercard.CardId != request.FromCardId || userId != usercard.UserId)
            {
                return new ApiResponse<PayResponse>("Card not found or it does not belong to the user.");
            }

            if (request.FromCardId == request.ToCardId)
            {
                return new ApiResponse<PayResponse>("You can only pay to another account.");
            }

            if (request.Amount <= 0)
            {
                return new ApiResponse<PayResponse>("Invalid amount!");
            }




            ApiResponse<BankCardInfoResponse> bankCardInfoResponseFrom = bankCardInfoService.GetById(request.FromCardId);
            if (!bankCardInfoResponseFrom.Success || bankCardInfoResponseFrom.Response is null)
            {
                return new ApiResponse<PayResponse>(bankCardInfoResponseFrom.Message);
            }

            BankCardInfoResponse bankCardFrom = bankCardInfoResponseFrom.Response;

            ApiResponse balanceResponseFrom = bankCardInfoService.Balance(bankCardFrom.CardId, request.Amount, PaymentDirection.Withdraw);
            if (!balanceResponseFrom.Success)
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




            var dues = unitOfWork.DuesRepository.GetById(request.DuesId ?? 0);
            var invoice = unitOfWork.InvoiceRepository.GetById(request.InvoiceId ?? 0);

            if ((dues == null && invoice == null) || (dues != null && invoice != null))
            {
                return new ApiResponse<PayResponse>("Exactly one of duesId or invoiceId should be provided.");
            }

            if (dues != null)
            {
                var apartment = unitOfWork.ApartmentRepository.GetById(dues.ApartmentId);
                if (apartment == null)
                {
                    return new ApiResponse<PayResponse>("Apartment information not found.");
                }

                if (apartment.UserId != userId)
                {
                    return new ApiResponse<PayResponse>("You are not authorized to pay for this due.");
                }
            }
            else if (invoice != null)
            {
                var apart = unitOfWork.ApartmentRepository.GetById(invoice.ApartmentId);
                if (apart == null)
                {
                    return new ApiResponse<PayResponse>("Apartment info not found.");
                }

                if (apart.UserId != userId)
                {
                    return new ApiResponse<PayResponse>("You are not authorized to pay for this invoice.");
                }
            }

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
