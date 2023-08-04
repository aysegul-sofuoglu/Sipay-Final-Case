using Business.Generic;
using DataAccess.Domain;
using Schema;

namespace Business
{
    public interface IInvoiceService : IGenericService<Invoice, InvoiceRequest, InvoiceResponse>
    {
    }
}
