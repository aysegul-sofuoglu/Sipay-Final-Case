using Business.Generic;
using DataAccess.Domain;
using Schema;

namespace Business
{
    public interface IApartmentService : IGenericService<Apartment, ApartmentRequest, ApartmentResponse>
    {
    }
}
