using AutoMapper;
using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<ApartmentRequest, Apartment>();
            CreateMap<Apartment, ApartmentResponse>();

            CreateMap<BankCardInfoRequest, BankCardInfo>();
            CreateMap<BankCardInfo, BankCardInfoResponse>();

            CreateMap<DuesRequest, Dues>();
            CreateMap<Dues, DuesResponse>();

            CreateMap<InvoiceRequest, Invoice>();
            CreateMap<Invoice, InvoiceResponse>();

            CreateMap<MessageRequest, Message>();
            CreateMap<Message, MessageResponse>();

            CreateMap<PaymentRequest, Payment>();
            CreateMap<Payment, PaymentResponse>();

            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
