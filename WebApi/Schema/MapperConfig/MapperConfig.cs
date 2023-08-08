using AutoMapper;
using DataAccess.Domain;

namespace Schema
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<ApartmentRequest, Apartment>();
            CreateMap<Apartment, ApartmentResponse>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname))
                .ForMember(dest => dest.Block, opt => opt.MapFrom(src => src.Block.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ApartmentType.Type));

            CreateMap<BankCardInfoRequest, BankCardInfo>();
            CreateMap<BankCardInfo, BankCardInfoResponse>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname));

            CreateMap<DuesRequest, Dues>();
            CreateMap<Dues, DuesResponse>();

            CreateMap<InvoiceRequest, Invoice>();
            CreateMap<Invoice, InvoiceResponse>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<MessageRequest, Message>();
            CreateMap<Message, MessageResponse>().ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname));

            CreateMap<PaymentRequest, Payment>();
            CreateMap<Payment, PaymentResponse>()
     .ForMember(dest => dest.UserName, opt =>
     {
         opt.PreCondition(src =>
             (src.Invoice != null && src.Invoice.Apartment != null && src.Invoice.Apartment.User != null) ||
             (src.Dues != null && src.Dues.Apartment != null && src.Dues.Apartment.User != null) ||
             (src.BankCardInfo != null && src.BankCardInfo.User != null)
         );

         opt.MapFrom(src =>
             src.Invoice != null && src.Invoice.Apartment != null && src.Invoice.Apartment.User != null
                 ? src.Invoice.Apartment.User.Name + " " + src.Invoice.Apartment.User.Surname
                 : (src.Dues != null && src.Dues.Apartment != null && src.Dues.Apartment.User != null
                     ? src.Dues.Apartment.User.Name + " " + src.Dues.Apartment.User.Surname
                     : (src.BankCardInfo != null && src.BankCardInfo.User != null
                         ? src.BankCardInfo.User.Name + " " + src.BankCardInfo.User.Surname
                         : "")
                 ));
     });

            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();

            CreateMap<UserLogRequest, UserLog>();
            CreateMap<UserLog, UserLogResponse>();
        }
    }
}
