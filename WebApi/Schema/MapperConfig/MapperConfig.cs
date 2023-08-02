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
            CreateMap<BankCardInfo, BankCardInfoResponse>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname));

            CreateMap<DuesRequest, Dues>();
            CreateMap<Dues, DuesResponse>();

            CreateMap<InvoiceRequest, Invoice>();
            CreateMap<Invoice, InvoiceResponse>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<MessageRequest, Message>();
            CreateMap<Message, MessageResponse>();

            CreateMap<PaymentRequest, Payment>();
            CreateMap<Payment, PaymentResponse>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.BankCardInfo.User.Name + " " + src.BankCardInfo.User.Surname));

            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
